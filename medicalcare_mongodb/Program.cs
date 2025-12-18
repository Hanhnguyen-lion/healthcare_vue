
using medicalcare_mongodb;
using medicalcare_mongodb.models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var APICors = "ApiCORS";

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddDbContext<MedicalcareDbContext>(options=>
    options.UseMongoDB(mongoDBSettings?.AtlasURI??"", mongoDBSettings?.DatabaseName??"")
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(APICors);
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
