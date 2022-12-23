using System;
using DefaultDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using DefaultDatabase.Services;
using DefaultDatabase.DbContexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var dBConnStringConfig = new StringBuilder(Environment.GetEnvironmentVariable("ConnectionStringsDockerConStr"));
// var dBConnString = dBConnStringConfig.Replace("ENVDBU", Environment.GetEnvironmentVariable("DB_U"))
//                     .Replace("ENVDBPW", Environment.GetEnvironmentVariable("DB_PW"))
//                     .ToString();
// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DefaultContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.UseHttps();

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseForwardedHeaders();
    // app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    using (var scope = app.Services.CreateScope())
    {
        var defaultContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        // defaultContext.Database.EnsureDeleted();
        defaultContext.Database.EnsureCreated();
    }
}
else
{
    app.UseForwardedHeaders();
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    using (var scope = app.Services.CreateScope())
    {
        var defaultContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        // defaultContext.Database.EnsureDeleted();
        defaultContext.Database.EnsureCreated();
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();