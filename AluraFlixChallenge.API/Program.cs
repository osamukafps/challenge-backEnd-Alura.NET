using AluraFlixChallenge.API.Config;
using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Repository;
using AluraFlixChallenge.API.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Database Configuration
MongoDbContext.ConnectionString = builder.Configuration.GetSection("DatabaseConnection:ConnectionString").Value;
MongoDbContext.Database = builder.Configuration.GetSection("DatabaseConnection:Database").Value;

//Auto Mapper Inicialization
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Service Registry
builder.Services.AddSingleton<IVideoService, VideoService>();

//Repository
builder.Services.AddTransient<MongoDbContext>();
builder.Services.AddTransient<IVideoRepository, VideoRepository>();

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
