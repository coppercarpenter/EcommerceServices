using Ecommerce.DTO.Models.Common;
using Ecommerce.EF;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Extensions
{
    public static class ExtensionMethods
    {
        #region Methods

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EContext>();
            db.Database.Migrate();
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseWrapper<object>()
                        {
                            Success = false,
                            Message = contextFeature.Error.Message,
                            Data = null
                        }));
                    }
                });
            });
        }

        #endregion Methods
    }
}