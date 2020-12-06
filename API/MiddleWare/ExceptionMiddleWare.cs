using System;
using System.Threading.Tasks;
using API.Models;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace API.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (DreamTraderException ex)
            {
                Console.WriteLine($"{ex.GetType()}\n{ex.Message}\nPath {ex.Path}.{ex.Method}");
                context.Response.StatusCode = 409;
                context.Response.Headers.Add("content-type", "application/json");
                await context.Response.WriteAsync(new DreamTraderExceptionModel(ex).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()}\n{ex.Message}\n");
                context.Response.StatusCode = 500;
                context.Response.Headers.Add("content-type", "application/json");
                await context.Response.WriteAsync(new DefaultExceptionModel(ex).ToString());
            }
        }
    }
}