
using ChatApi.Context;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var bleh = builder.Configuration.GetConnectionString("postgreSql");//Environment.GetEnvironmentVariable("ConnectionStrings__postgreSql");
builder.Services.AddDbContext<ChatApiDbContext>((DbContextOptionsBuilder ob) =>
    ob.UseNpgsql(builder.Configuration.GetConnectionString("postgreSql")) //("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
