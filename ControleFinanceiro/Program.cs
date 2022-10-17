using ControleFinanceiro;
using ControleFinanceiro.Data;
using ControleFinanceiro.Interfaces;
using ControleFinanceiro.Repositories;
//using ControleFinanceiro.Interfaces;
//using ControleFinanceiro.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

//Trabalhar com controllers
builder.Services.AddControllers();

//Banco de Dados
builder.Services.AddDbContext<AppDbContext>(
     opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

//interfaces e repositorios
builder.Services.AddScoped<IBancoRepository, BancoRepository>();

builder.Services.AddCors();
builder.Services.AddControllersWithViews();

//------------| Autentica��o e autoriza��o (in�cio) |-----------------

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
//------------| Autentica��o e autoriza��o (fim) |-----------------


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
