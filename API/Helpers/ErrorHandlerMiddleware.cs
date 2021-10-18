using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    KeyNotFoundException _ => (int)HttpStatusCode.NotFound,
                    BadHttpRequestException _ => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var result = JsonSerializer.Serialize(new { message = error?.Message });


                await context.Response.WriteAsync(result);
                //await context.Response.WriteAsync(new ErrorDetails()
                //{
                //    StatusCode = context.Response.StatusCode,
                //    Message = error?.Message
                //}.ToString());

              //  await response.WriteAsync(result);
            }
        }
    }
}