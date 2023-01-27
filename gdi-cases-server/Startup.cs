using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.MongoDb;
using gdi_cases_server.XmlSupport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddControllers().AddXmlSerializerFormatters();
        services.AddControllers().AddXmlSerializerFormatters();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddCasesAuthentication();
        services.AddCasesXmlSupport();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.EnableAnnotations());

        services.AddControllers().AddXmlDataContractSerializerFormatters();

        /*
        services.AddMvc(options =>
        {
            options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
            options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
        });
        */
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        app.UseSwagger(c => c.RouteTemplate = "api/v1/cases/swagger/{documentName}/swagger.json");
        //app.UseSwagger(c => c.v)
        app.UseSwaggerUI(c =>
        {
            
            c.RoutePrefix = "api/v1/cases/swagger";
        });

        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        //app.MapControllers();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        // Redirect to swagger
        app.UseEndpoints(endpoints => endpoints.MapGet("/", async (http) => http.Response.Redirect("/api/v1/cases/swagger")));
    }
}