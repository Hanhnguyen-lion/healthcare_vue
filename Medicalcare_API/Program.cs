using Microsoft.EntityFrameworkCore;
using Medicalcare_API.Helpers;
using Medicalcare_API.Models;

var builder = WebApplication.CreateBuilder(args);
var APICors = "ApiCORS";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// var mongoSetting = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

// builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// builder.Services.AddDbContext<DbContext>();

builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("MedicalCareConnectionDb")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddCors(options =>
    options.AddPolicy(APICors, policy=>
    {
        policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(APICors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
