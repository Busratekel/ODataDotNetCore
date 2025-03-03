
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbContext>((sp, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

var serviceUrl = "https://your-sap-server/sap/opu/odata/sap/YOUR_SERVICE_NAME";
var username = "your-username";
var password = "your-password";
var entitySet = "YourEntitySet";

var sapODataService = app.Services.GetRequiredService<ISapODataService>();

// Get data
var data = await sapODataService.GetDataAsync(serviceUrl, username, password, entitySet);
Console.WriteLine(data);

// Create data
var jsonData = "{ \"Property1\": \"Value1\", \"Property2\": \"Value2\" }";
var createResponse = await sapODataService.CreateDataAsync(serviceUrl, username, password, entitySet, jsonData);
Console.WriteLine(createResponse);

// Update data
var key = "1"; // Replace with the actual key of the entity to update
var updateResponse = await sapODataService.UpdateDataAsync(serviceUrl, username, password, entitySet, key, jsonData);
Console.WriteLine(updateResponse);

