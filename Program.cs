using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaaS.src.Application;
using SaaS.src.Application.Behaviors;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Infrastructure.Data.Repositories;
using SaaS.src.Infrastructure.Data.Repositories.Size;
using SaaS.src.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => {
    cfg.AddMaps(typeof(AssemblyReference).Assembly);
}); builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Urls.Add("http://localhost:5000");
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();