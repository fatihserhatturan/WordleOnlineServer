using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WordleOnlineServer.Models.Context;
using WordleOnlineServer.Models.MsSqlModels;
using WordleOnlineServer.Options.Config;
using WordleOnlineServer.Options.OptionsSetup;
using WordleOnlineServer.Services;

var builder = WebApplication.CreateBuilder(args);
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings");
var connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");

// Add services to the container.
builder.Services.AddDbContext<ProjectDbContext>(options =>
options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ProjectDbContext>();

builder.Services.AddSingleton<JwtService>();

builder.Services.AddSingleton<MongoService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MongoDBConnection");
    var mongoDBSettings = configuration.GetSection("MongoDBSettings");
    var databaseName = mongoDBSettings.GetValue<string>("DatabaseName");
    var fourLetterCollection = mongoDBSettings.GetValue<string>("FourLetterLobby");
    var fiveLetterCollection = mongoDBSettings.GetValue<string>("FiveLetterLobby");
    var sixLetterCollection = mongoDBSettings.GetValue<string>("SixLetterLobby");
    var sevenLetterCollection = mongoDBSettings.GetValue<string>("SevenLetterLobby");
    var enableUsersCollection = mongoDBSettings.GetValue<string>("EnableUsers");
    var matchCollection = mongoDBSettings.GetValue<string>("Match");
    var matchRequestCollection = mongoDBSettings.GetValue<string>("MatchRequest");
    var userCollection = mongoDBSettings.GetValue<string>("User");

    return new MongoService(connectionString, databaseName, fourLetterCollection, fiveLetterCollection, sixLetterCollection, sevenLetterCollection, enableUsersCollection, matchCollection, matchRequestCollection, userCollection);
});

builder.Services.AddControllers();
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
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
