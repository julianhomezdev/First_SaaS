using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.Behaviors;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Application.UseCases.Product.Commands.Create;
using SaaS.src.Infrastructure.Data.Repositories;
using SaaS.src.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly);
});

// Global config pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Validators register
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();


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