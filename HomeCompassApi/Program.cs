using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Cases;
using HomeCompassApi.BLL.Facilities;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

// Feed
builder.Services.AddScoped<IRepository<Post>, PostRepository>();
builder.Services.AddScoped<IRepository<Comment>, CommentRepository>();

// Cases
builder.Services.AddScoped<IRepository<Homeless>, HomelessRepository>();
builder.Services.AddScoped<IRepository<Missing>, MissingRepository>();

// Facility
builder.Services.AddScoped<IRepository<Facility>, FacilityRepository>();
builder.Services.AddScoped<IRepository<Resource>, ResourceRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();



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
