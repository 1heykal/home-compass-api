using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Cases;
using HomeCompassApi.BLL.Facilities;
using HomeCompassApi.Helpers;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();


// JWT
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };

    });

string ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

// User
builder.Services.AddScoped<UserRepository, UserRepository>();

// Feed
builder.Services.AddScoped<PostRepository, PostRepository>();
builder.Services.AddScoped<CommentRepository, CommentRepository>();
builder.Services.AddScoped<LikeRepository, LikeRepository>();

// Cases
builder.Services.AddScoped<HomelessRepository, HomelessRepository>();
builder.Services.AddScoped<MissingRepository, MissingRepository>();

// Facility
builder.Services.AddScoped<FacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IRepository<Resource>, ResourceRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();

builder.Services.AddScoped<IRepository<Info>, InfoRepository>();



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

app.MapSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
