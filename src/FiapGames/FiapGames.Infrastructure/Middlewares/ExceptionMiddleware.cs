using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces.LogRepo;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using System.Text.Json;
namespace FiapGames.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogRepositorio _logRepository)
        {
            string requestBody = await ReadRequestBody(context);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logRepository, requestBody);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogRepositorio _logRepositorio, string request)
        {
            context.Response.ContentType = "application/json";

            var objErro = new ErroResponseDto();

            switch (ex)
            {
                case UnauthorizedAccessException:
                    objErro.Erro = "Acesso não autorizado";
                    objErro.Status = HttpStatusCode.Unauthorized;
                    break;

                case ArgumentException:
                    objErro.Erro = $"Requisição inválida: {ex.Message}";
                    objErro.Status = HttpStatusCode.BadRequest;
                    break;

                default:
                    objErro.Erro = "Erro interno do servidor";
                    objErro.Status = HttpStatusCode.InternalServerError;
                    break;
            }

            try
            {
                // opcional: proteger dados sensíveis
                if (context.Request.Path.ToString().ToLower().Contains("login"))
                {
                    request = "[PROTEGIDO]";
                }

                // opcional: limitar tamanho
                if (!string.IsNullOrEmpty(request) && request.Length > 2000)
                {
                    request = request.Substring(0, 2000);
                }

                var log = new LogError
                {
                    Mensagem = objErro.Erro,
                    Exception = ex.ToString(),
                    Request = request,
                    Response = objErro.Erro,
                    Status = (int)objErro.Status,
                    Url = context.Request.Path,
                    TraceId = context.TraceIdentifier,
                    Data = DateTime.UtcNow
                };

                await _logRepositorio.SalvarLogErro(log);
            }
            catch
            {
                // nunca quebra a API por causa do log
            }

            context.Response.StatusCode = (int)objErro.Status;

            await context.Response.WriteAsync(JsonSerializer.Serialize(objErro));
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                leaveOpen: true
            );

            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            return body;
        }

        public class ErroResponseDto
        {
            public string Erro { get; set; }
            public HttpStatusCode Status { get; set; }
        }
    }
}