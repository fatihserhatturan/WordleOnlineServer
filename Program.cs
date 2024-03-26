using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
