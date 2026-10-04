using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityModel.Data;
using UniversityModel.Memory;
using UniversityModel.Service;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<MemoryDataStore>();
builder.Services.AddScoped<UserService>();

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters                       //4.1 adding packages and orders in program section
        {
            ValidateIssuer = true,        // to check who has sent the token    
            ValidateAudience = true,           // to check whom the token is sent to
            ValidateLifetime = true,        //to check if the token is expired or not
            ValidateIssuerSigningKey = true          //to check if the token signature is espired or not
        };      //these are called validates
    });
var app = builder.Build();
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