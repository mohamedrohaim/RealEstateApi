using BusinessLayer.Iservices;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utilities;
using Utilities.EmailService;
using DataAccessLayer.SeedingData;
using RealEstate.Helpers.ImageUploader;
using BusinessLayer.Services.OTP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Bind the JWT section from the configuration to your JWT class
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connectionString)
                );
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        ValidIssuer = JWTStatic.Issuer,
        ValidAudience = JWTStatic.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTStatic.Key))
    };
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOtpSender, EmailOtpSender>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitCategoryService, UnitCategoryService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IScheduleAppointmentSercice, ScheduleAppointmentSercice>();
builder.Services.AddScoped<IImageUploader, ImageUploader>();
builder.Services.AddScoped<ISMSTwilio, SMSTwilio>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddIdentity<User, IdentityRole>(
    options => {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;

    }
    ).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();




//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title = "RealEstateApp", Version = "v1" })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors(
                c =>
                c.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()

                );
SeedingData.Initialize(app.Services);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
