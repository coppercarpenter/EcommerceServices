using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.Models.Common;
using Ecommerce.EF;
using Ecommerce.Services.Interfaces.Unit;
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
            var _service = scope.ServiceProvider.GetRequiredService<IServiceUnit>();
            db.Database.Migrate();
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                        using var scope = app.ApplicationServices.CreateScope();

                        if (contextFeature.Error is NotFoundException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        }
                        else if (contextFeature.Error is BadRequestException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else if (contextFeature.Error is AlreadyExistException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }

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