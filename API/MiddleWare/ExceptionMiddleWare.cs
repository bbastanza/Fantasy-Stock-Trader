// using System;
// using System.Threading.Tasks;
// using Infrastructure.Exceptions;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
//
// namespace API
// {
//     public class ExceptionMiddleWare
//     {
//         private readonly RequestDelegate _next;
//
//         public ExceptionMiddleWare(RequestDelegate next)
//         {
//             _next = next;
//         }
//
//         public async Task<IActionResult> Invoke(HttpContext context)
//         {
//
//             try
//             {    
//                 await _next(context);
//             }
//             catch (DreamTraderException ex)
//             {
//                 return await ex.Method;
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 throw;
//             }
//         }
//     }
// }