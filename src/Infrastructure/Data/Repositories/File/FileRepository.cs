using Microsoft.Extensions.Options;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Infrastructure.Configuration;

namespace SaaS.src.Infrastructure.Data.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly FileStorageSettings _settings;
        private static readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);

        public FileRepository(IWebHostEnvironment environment, IOptions<FileStorageSettings> settings)
        {
            _settings = settings.Value;
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            await _fileLock.WaitAsync();
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                    return null;

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var projectRoot = Directory.GetCurrentDirectory();
                var uploadsFolder = Path.Combine(projectRoot, "wwwroot", "images", "products");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                // Cambia esta parte - usa useAsync: true
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                {
                    await imageFile.CopyToAsync(stream);
                }

                Console.WriteLine($"✅ Imagen guardada en: {filePath}");
                return $"images/products/{fileName}";
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}