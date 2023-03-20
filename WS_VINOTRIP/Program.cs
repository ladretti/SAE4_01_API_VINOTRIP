using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using WS_VINOTRIP.Models;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

/*builder.Services.AddDbContext<SeriesDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("SeriesDbContext")));*/
builder.Services.AddDbContext<VinotripDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("VinotripDbContextRemote")));

builder.Services.AddScoped<IDataRepository<Sejour>, SejourManager>();
builder.Services.AddScoped<IDataRepository<Comporte>, ComporteManager>();
builder.Services.AddScoped<IDataRepository<Avis>, AvisManager>();
builder.Services.AddScoped<IDataRepository<RouteDesVins>, RouteDesVinsManager>();
builder.Services.AddScoped<IDataRepository<User>, UserManager>();
builder.Services.AddScoped<IDataRepository<Vignoble>, VignobleManager>();
builder.Services.AddScoped<IDataRepository<CatParticipant>, CatParticipantManager>();
builder.Services.AddScoped<IDataRepository<CatVignoble>, CatVignobleManager>();
builder.Services.AddScoped<IDataRepository<CatSejour>, CatSejourManager>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
    config.AddPolicy(Policies.User, Policies.UserPolicy());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors(
        options => options.WithOrigins("http://51.83.36.122:6980").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );



app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



