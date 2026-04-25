using Microsoft.AspNetCore.Http;
using System.Net;
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var objErro = new ErroResponseDto();

            switch (ex)
            {
                case UnauthorizedAccessException:
                    objErro.TituloErro = "Acesso não autorizado";
                    objErro.Status = HttpStatusCode.Unauthorized;
                    break;

                case ArgumentException:
                    objErro.TituloErro = $"Requisição inválida: {ex.Message}";
                    objErro.Status = HttpStatusCode.BadRequest;
                    break;

                default:
                    objErro.TituloErro = "Erro interno do servidor";
                    objErro.Status = HttpStatusCode.InternalServerError;
                    break;

            }
            //logica de log{}
            context.Response.StatusCode = (int)objErro.Status;
            await context.Response.WriteAsync(JsonSerializer.Serialize(objErro));
        }

        public class ErroResponseDto()
        {
            public string TituloErro { get; set; }
            public HttpStatusCode Status { get; set; }
        }
    }
}
