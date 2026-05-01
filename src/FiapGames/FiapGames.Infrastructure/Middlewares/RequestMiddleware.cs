using Microsoft.AspNetCore.Http;

namespace FiapGames.Infrastructure.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            await _next(context);
        }
    }
}
