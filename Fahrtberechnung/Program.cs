using Fahrtberechnung.DbContexts;
using Fahrtberechnung.Helpers;
using Fahrtberechnung.Interfaces;
using Fahrtberechnung.IRepostories;
using Fahrtberechnung.Models.Roles;
using Fahrtberechnung.Repositories;
using Fahrtberechnung.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers().AddNewtonsoftJson(options =>
 //   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.User)));

    options.AddPolicy("UserPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.User)));

    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin)));
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", config =>
    {
        config.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(actionModelConvention: new RouteTokenTransformerConvention(
                                 new ConfigureApiUrlName()));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
