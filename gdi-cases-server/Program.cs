using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using dotenv.net;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.XmlSupport;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCasesDatabase();
builder.Services.AddCasesAuthentication();

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
//builder.Services.AddControllers().AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(CasesApiKey.ConfigureSwaggerGen);
builder.Services.AddSwaggerGen(XmlSupport.ConfigureSwaggerGen);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
