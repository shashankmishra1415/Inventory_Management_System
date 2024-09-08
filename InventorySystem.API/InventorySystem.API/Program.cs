using InventorySystem.API.Extensions;
using InventorySystem.API.Injectable;
using InventorySystem.Application;
using InventorySystem.Application.Helpers;
using InventorySystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    	setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
 { jwtSecurityScheme, Array.Empty<string>() }
 });
});

#region AmazonS3
builder.Services.Configure<AmazonS3>(builder.Configuration.GetSection("AmazonS3"));
#endregion

#region JWT
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JwtConfig"));
#endregion JWT

#region Dependancy Injection
InjectableServices.Services(builder);
builder.Services.AddInfrastructureServices();
#endregion Dependancy Injection end

#region Fluent Validation
builder.Services.ConfigureValidators();
#endregion Fluent Validation


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1 == 1)

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader());


app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
