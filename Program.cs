using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.Interfaces.TenantInterfaces;
using SaaS.src.Application.Interfaces.TenantUseCases;
using SaaS.src.Application.UseCases.TenantUseCases;
using SaaS.src.Infrastructure.Data.Repositories;
using SaaS.src.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<ICreateTenantUseCase, CreateTenantUseCase>();

// Register dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Urls.Add("http://localhost:5000");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();