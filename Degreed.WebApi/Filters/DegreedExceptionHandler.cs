using Microsoft.AspNetCore.Builder;
using Degreed.Logger;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Degreed.WebApi.Model;
using Microsoft.AspNetCore.Http;

namespace Degreed.WebApi.Filters
{
    public static class DegreedExceptionHandler
    {
        public static void GlobalExceptionHandler(this IApplicationBuilder app, IDegreedLogger loggers)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        loggers.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new JsonResponse()
                        {                           
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",                            
                            Result = null,
                        }.ToString());
                    }
                });
            });
        }
    }
}
