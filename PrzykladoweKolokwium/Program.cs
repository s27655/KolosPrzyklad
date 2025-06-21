using Microsoft.EntityFrameworkCore;
using PrzykładoweKolokwium.Data;
using PrzykładoweKolokwium.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddDbContext<S27655Context>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Deafult"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();