using dotenv.net;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;

DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCasesDatabase();
builder.Services.AddCasesAuthentication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(CasesApiKey.ConfigureSwaggerGen);

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

