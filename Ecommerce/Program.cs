using Ecommerce.Common.KeysAndValues;
using Ecommerce.Converters;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Extensions;
using Ecommerce.Filters;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.Reflection;

static BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
{
    var res = new ResponseWrapper<object>
    {
        Success = false,
        Message = MessageHelper.InvalidBody,
        Error = new ErrorResponse()
    };
    foreach (var item in actionContext.ModelState)
    {
        if (item.Value.ValidationState == ModelValidationState.Invalid)
        {
            res.Error.ErrorMessage += $"{item.Value.Errors.First().ErrorMessage}\n";
        }
    }
    return new BadRequestObjectResult(res);
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        return CustomErrorResponse(actionContext);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{{
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
    // XML Doc
    string xmlDocFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlDocFilePath = Path.Combine(AppContext.BaseDirectory, xmlDocFileName);
    c.IncludeXmlComments(xmlDocFilePath);
});

builder.Services.AddService(builder.Configuration);

builder.Services.AddScoped<Converter>();
builder.Services.AddScoped<CheckJwtFilter>();

builder.Services.AddResponseCompression(option => option.Providers.Add<GzipCompressionProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(option =>
{
    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(option.RoutePrefix) ? "." : "..";
    option.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Ecommerce");
});

app.UseCors();

app.InitializeDatabase();

app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();