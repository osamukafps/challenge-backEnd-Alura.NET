using AluraFlixChallenge.API.ApiSettings;
using AluraFlixChallenge.API.Config;
using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Repository;
using AluraFlixChallenge.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(Settings.Secret);


builder.Services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


//Database Configuration
MongoDbContext.ConnectionString = builder.Configuration.GetSection("DatabaseConnection:ConnectionString").Value;
MongoDbContext.Database = builder.Configuration.GetSection("DatabaseConnection:Database").Value;

//Auto Mapper Inicialization
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Service Registry
builder.Services.AddSingleton<IVideoService, VideoService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();

//Repository
builder.Services.AddTransient<MongoDbContext>();
builder.Services.AddTransient<IVideoRepository, VideoRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
