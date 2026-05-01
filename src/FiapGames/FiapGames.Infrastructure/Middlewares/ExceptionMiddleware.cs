using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces.Log;
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

        public async Task Invoke(HttpContext context, ILogRepositorio logRepository)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logRepository);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogRepositorio logRepositorio)
        {
            context.Response.ContentType = "application/json";

            var objErro = ex switch
            {
                UnauthorizedAccessException => new ErroResponseDto("Acesso não autorizado", HttpStatusCode.Unauthorized),

                ArgumentException => new ErroResponseDto($"Requisição inválida: {ex.Message}", HttpStatusCode.BadRequest),

                _ => new ErroResponseDto("Erro interno do servidor", HttpStatusCode.InternalServerError)
            };

            context.Response.StatusCode = (int)objErro.Status;

            try 
            {
                string requestBody = await ReadRequestBody(context);
                
                var log = new LogError
                {
                    Mensagem = objErro.Erro,
                    Exception = ex.ToString(),
                    Request = TratarDadosSensiveis(context.Request.Path, requestBody),
                    Response = JsonSerializer.Serialize(objErro),
                    Status = context.Response.StatusCode,
                    Url = context.Request.Path,
                    TraceId = context.TraceIdentifier,
                    Data = DateTime.UtcNow
                };

                await logRepositorio.SalvarLogErro(log);
            }
            catch { /* Silencioso para garantir a resposta ao usuário */ }

            await context.Response.WriteAsync(JsonSerializer.Serialize(objErro));
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            return body;
        }

        private string TratarDadosSensiveis(string path, string body)
        {
            if (string.IsNullOrEmpty(body)) return body;
            if (path.Contains("/login", StringComparison.OrdinalIgnoreCase)) return "[PROTEGIDO]";
            return body.Length > 2000 ? body.Substring(0, 2000) : body;
        }

        public record ErroResponseDto(string Erro, HttpStatusCode Status);
    }
}