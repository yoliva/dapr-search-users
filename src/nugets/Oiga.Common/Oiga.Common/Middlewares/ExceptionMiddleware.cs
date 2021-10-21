using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Oiga.Common.Exceptions;
using Oiga.Common.Models;
using System.Text;

namespace Oiga.Common.Middlewares
{
    public static class ExceptionMiddleware
    {
        private const string GENERIC_ERROR_MSG = "Something went wrong";
        private const string APPLICATION_JSON_CONTENT_TYPE = "application/json";

        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder => appBuilder.Use(async (context, next) =>
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>();

                context.Response.ContentType = APPLICATION_JSON_CONTENT_TYPE;

                if (ex.Error is OigaException exception)
                {
                    context.Response.StatusCode = (int)exception.StatusCode;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                    {
                        Error = new ErrorResponseItem
                        {
                            Code = exception.ErrorCode,
                            Message = exception.Message
                        }
                    }), Encoding.UTF8).ConfigureAwait(false);
                }
                else
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        new ErrorResponse
                        {
                            Error = new ErrorResponseItem
                            {
                                Code = (int)ErrorCode.UnexpectedError,
                                Message = GENERIC_ERROR_MSG
                            }
                        }), Encoding.UTF8).ConfigureAwait(false);
                }
            }));
        }
    }
}
