using System;
using DefaultDatabase.Models;
using Microsoft.EntityFrameworkCore;
using DefaultDatabase.Services;
using DefaultDatabase.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DefaultContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DockerConStr")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IItemService, ItemService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    using(var scope = app.Services.CreateScope())
    {
        var defaultContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        defaultContext.Database.EnsureDeleted();
        defaultContext.Database.EnsureCreated();
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();