using Microsoft.EntityFrameworkCore;
using Medicalcare_API.Models;
using Medicalcare_API.Helpers;

var builder = WebApplication.CreateBuilder(args);
var APICors = "ApiCORS";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var mongoSetting = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();


// Console.WriteLine("mongoSetting?.AtlasURI: "+mongoSetting?.AtlasURI);
// Console.WriteLine("mongoSetting?.DatabaseName: "+mongoSetting?.DatabaseName);


// builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));



// builder.Services.AddDbContext<DataContext>(options=>
// options.UseMongoDB(mongoSetting?.AtlasURI??"", mongoSetting?.DatabaseName??""));

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(APICors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
