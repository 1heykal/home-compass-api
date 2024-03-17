using HomeCompassApi.Repositories;
using HomeCompassApi.Repositories.Cases;
using HomeCompassApi.Repositories.Facilities;
using HomeCompassApi.Helpers;
using HomeCompassApi.Models;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HomeCompassApi.Services.EmailService;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;

})
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();



// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOptions<EmailSettings>().BindConfiguration(nameof(EmailSettings));
builder.Services.AddSingleton<EmailService>();



// JWT
builder.Services.AddScoped<AuthService, AuthService>();
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
builder.Services.AddScoped<ReportRepository, ReportRepository>();


// Cases
builder.Services.AddScoped<HomelessRepository, HomelessRepository>();
builder.Services.AddScoped<MissingRepository, MissingRepository>();

// Facility
builder.Services.AddScoped<FacilityRepository, FacilityRepository>();
builder.Services.AddScoped<ResourceRepository, ResourceRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
builder.Services.AddScoped<JobRepository, JobRepository>();


builder.Services.AddScoped<InfoRepository, InfoRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the access token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id  = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
