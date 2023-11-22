using Microsoft.OpenApi.Models;
using properties.Application.Interfaces;
using properties.Application.Services;
using properties.Domain.Interfaces;
using properties.Infrastructure.ExternalService;
using properties.Infrastructure.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

var corsSecurity = "_policyCors";
var builder = WebApplication.CreateBuilder(args);


// Deppendencies

builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("LocalDataBase")));

builder.Services.AddTransient<IOwnerRepository, OwnerRepository>();
builder.Services.AddTransient<IOwnerService, OwnerService>();

builder.Services.AddTransient<IPropertyRepository, PropertyRepository>();
builder.Services.AddTransient<IPropertyServices, PropertyServices>();

builder.Services.AddTransient<IFakeCDNExternalService, FakeCDNExternalService>();
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "RealState api service",
            Version = "v1",
            Description = "Service manage and cosume property and owner services",
            Contact = new OpenApiContact
            {
                Name = "Jhon Velasco",
                Email = "frediv0406@gmail.com",
                Url = new Uri("https://jhonvelasco.co")
            }
        });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsSecurity,
                      policy =>
                      {
                          //policy.WithOrigins("https://www.millionandup.com");
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
//public docs
app.UseReDoc(options =>
{
    options.DocumentTitle = "RealState api service";
    options.SpecUrl = "/swagger/v1/swagger.json";
});
app.UseHttpsRedirection();

app.UseCors(corsSecurity);

app.UseAuthorization();

app.MapControllers();

app.Run();
