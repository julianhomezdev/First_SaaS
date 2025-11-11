using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.Interfaces.Repositories;
using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.UseCases.Product.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductResponseDto>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IFileRepository _fileRepository;


        //Constructor: inyect repository to save the bd
        public CreateProductCommandHandler(IProductRepository productRepository, IFileRepository fileRepository)
        {

            _productRepository = productRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Result<ProductResponseDto>> Handle(
    CreateProductCommand request,
    CancellationToken cancellationToken)
        {
            string imageUrl = null;

            try
            {
                Console.WriteLine("🔴 INICIANDO CREACIÓN DE PRODUCTO...");

                // 1. Procesar imagen de forma ultra simple
                if (request.ImageFile != null)
                {
                    Console.WriteLine($"📁 Procesando imagen: {request.ImageFile.FileName}");

                    var fileName = Guid.NewGuid() + Path.GetExtension(request.ImageFile.FileName);
                    var uploadsPath = Path.Combine("wwwroot", "images", "products");
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), uploadsPath, fileName);

                    // Crear directorio si no existe
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                    // Guardar SINCRONO y simple
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await request.ImageFile.CopyToAsync(stream);

                    imageUrl = $"images/products/{fileName}";
                    Console.WriteLine($"✅ Imagen guardada: {imageUrl}");
                }

                Console.WriteLine("🗄️ Creando entidad producto...");

                // 2. Crear producto
                var product = new Domain.Entities.Product
                {
                    ProductName = request.ProductName,
                    ProductReference = request.ProductReference,
                    ProductPrice = request.ProductPrice,
                    ProductTypeId = request.ProductTypeId,
                    ImageUrl = imageUrl
                };

                // 3. Agregar sizes
                if (request.Sizes != null)
                {
                    product.ProductSizes = request.Sizes.Select(s => new ProductsSizes
                    {
                        SizeId = s.SizeId,
                        SizeStock = s.Stock
                    }).ToList();
                }

                Console.WriteLine("💾 Guardando en BD...");

                // 4. Guardar en BD
                var createdProduct = await _productRepository.CreateProductAsync(product);

                Console.WriteLine($"✅ Producto creado ID: {createdProduct.Id}");

                var response = new ProductResponseDto
                {
                    Id = createdProduct.Id,
                    ProductName = createdProduct.ProductName,
                    ProductReference = createdProduct.ProductReference,
                    ProductPrice = createdProduct.ProductPrice,
                    ImageUrl = createdProduct.ImageUrl
                };

                return Result<ProductResponseDto>.Success(response, "Producto creado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 ERROR CRÍTICO EN HANDLER:");
                Console.WriteLine($"📍 Mensaje: {ex.Message}");
                Console.WriteLine($"📍 Tipo: {ex.GetType().Name}");
                Console.WriteLine($"📍 StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"📍 Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"📍 Inner StackTrace: {ex.InnerException.StackTrace}");
                }

                return Result<ProductResponseDto>.Failure($"Error: {ex.Message}");
            }
        }

    }
}
