using Microsoft.AspNetCore.OData;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddOData(options =>
        {
            options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100);
            options.AddRouteComponents("odata", GetEdmModel());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static Microsoft.OData.Edm.IEdmModel GetEdmModel()
    {
        var builder = new Microsoft.OData.ModelBuilder.ODataConventionModelBuilder();
        builder.EntitySet<MyModel>("MyModels");
        return builder.GetEdmModel();
    }
}