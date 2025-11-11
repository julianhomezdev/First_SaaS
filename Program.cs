using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SaaS.src.Application;
using SaaS.src.Application.Behaviors;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Application.UseCases.Report;
using SaaS.src.Infrastructure.Configuration;
using SaaS.src.Infrastructure.Data.Repositories;
using SaaS.src.Infrastructure.Data.Repositories.File;
using SaaS.src.Infrastructure.Data.Repositories.Invoice;
using SaaS.src.Infrastructure.Data.Repositories.Size;
using SaaS.src.Infrastructure.Persistence;
using SaaS.src.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


// AL PRINCIPIO de Program.cs, ANTES de builder.Build()
builder.WebHost.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
});


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 104857600; // 100MB
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => { 
    cfg.AddMaps(typeof(AssemblyReference).Assembly);
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions =>
        {
            sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
            sqlServerOptions.CommandTimeout(60); 
        });
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IExcelRepository, ExcelService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ExportSalesReportUseCase>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200",
                 "https://8tl2sjbh-4200.use2.devtunnels.ms",
                 "https://rs28j11l-5174.use2.devtunnels.ms",
                 "https://7gpwbpz3-4200.use2.devtunnels.ms",
                 "https://front-saas-production.up.railway.app",
                 "https://front-saas-production.up.railway.app"
                )

                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithExposedHeaders("*");

            if (builder.Environment.IsDevelopment())
            {
                policy.SetIsOriginAllowed(origin =>
                {
                    // Permite cualquier túnel de devtunnels.ms
                    if (origin.Contains("devtunnels.ms") ||
                        origin.Contains("localhost") ||
                        origin.Contains("railway.app"))
                        return true;
                    return false;
                });
            }
        });
});
builder.Services.Configure<FileStorageSettings>(
    builder.Configuration.GetSection("FileStorage"));

var app = builder.Build();

// Crear estructura de carpetas
var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
var imagesPath = Path.Combine(wwwrootPath, "images", "products");

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
    Console.WriteLine($"Carpeta creada: {imagesPath}");
}





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/SaaS/wwwroot"
});



//app.Urls.Add("http://localhost:5174");
app.UseCors();
app.UseAuthorization();
app.MapControllers();


app.Run();