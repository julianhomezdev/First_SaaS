using MediatR;
using SaaS.src.Application.Common;
using SaaS.src.Application.DTOs.Product;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Application.UseCases.Product.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileRepository _fileRepository; 

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IFileRepository fileRepository) 
    {
        _productRepository = productRepository;
        _fileRepository = fileRepository; 
    }

    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.ProductId);
            if (existingProduct == null)
            {
                return Result<bool>.Failure($"Producto con ID {request.ProductId} no encontrado.");
            }

            // CORRECCIÓN: Verificar solo si la referencia cambió Y si existe en otro producto
            if (existingProduct.ProductReference != request.ProductReference)
            {
                // Solo validar si la nueva referencia ya existe en OTRO producto
                if (await _productRepository.ExistsByReferenceAsync(request.ProductReference, request.ProductId))
                {
                    return Result<bool>.Failure($"Ya existe otro producto con la referencia: {request.ProductReference}");
                }
            }

            // Actualizar propiedades básicas
            existingProduct.ProductName = request.ProductName;
            existingProduct.ProductReference = request.ProductReference;
            existingProduct.ProductPrice = request.ProductPrice;
            existingProduct.ProductTypeId = request.ProductTypeId;

            // Actualizar imagen si se proporciona una nueva
            if (request.ImageFile != null)
            {
                var imageUrl = await _fileRepository.SaveImageAsync(request.ImageFile);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    existingProduct.ImageUrl = imageUrl;
                }
            }

            // Actualizar tallas
            await UpdateProductSizesAsync(existingProduct, request.Sizes);
            await _productRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error actualizando el producto: {ex.Message}");
        }
    }

    private async Task UpdateProductSizesAsync(SaaS.src.Domain.Entities.Product product, List<ProductSizeRequest> newSizes)
    {
        if (!product.ProductSizes.Any())
        {
            
        }

        var existingSizes = product.ProductSizes.ToList();
        foreach (var existingSize in existingSizes)
        {
            product.ProductSizes.Remove(existingSize);
        }

        foreach (var sizeRequest in newSizes)
        {
            if (sizeRequest.Stock > 0)
            {
                product.ProductSizes.Add(new SaaS.src.Domain.Entities.ProductsSizes
                {
                    ProductId = product.Id,
                    SizeId = sizeRequest.SizeId,
                    SizeStock = sizeRequest.Stock
                });
            }
        }
    }
}