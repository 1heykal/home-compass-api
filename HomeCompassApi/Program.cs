using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<IRepository<Post>, PostRepository>();
builder.Services.AddScoped<IRepository<Comment>, CommentRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
