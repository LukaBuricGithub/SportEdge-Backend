using Microsoft.Extensions.Options;
using SportEdge.API.Configuration;
using SportEdge.API.Models.Domain;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SportEdge.API.Services.Implementation
{
    /// <summary>
    /// Provides implementation for product image-related service operations.
    /// </summary>
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository productImageRepository;
        private readonly ImageSettings imageSettings;


        private readonly int targetWidth = 800;
        private readonly int targetHeight = 800;


        public ProductImageService(IProductImageRepository productImageRepository, IOptions<ImageSettings> imageSettings)
        { 
            this.productImageRepository = productImageRepository;
            this.imageSettings = imageSettings.Value;
        }


        /// <inheritdoc/>
        public async Task DeleteImagesAsync(int productId)
        {
            var existingImages = await productImageRepository.GetByProductIdAsync(productId);

            if (!existingImages.Any())
            {
                throw new KeyNotFoundException($"No images found for product with ID {productId}.");
            }

            foreach (var image in existingImages)
            {
                var path = Path.Combine(imageSettings.ImageFolderPath, image.Filename);
                if (File.Exists(path))
                    File.Delete(path);
            }

            await productImageRepository.DeleteByProductIdAsync(productId);
        }

        /// <inheritdoc/>
        public async Task UploadImagesAsync(int productId, IFormFileCollection images)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var imageList = new List<ProductImage>();

            foreach (var image in images)
            {
                var fileExtension = Path.GetExtension(image.FileName).ToLower();
                
                // Check if the file extension is allowed
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new InvalidOperationException($"Invalid file extension: {fileExtension}");
                }



                var fileName = $"product_{productId}_{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(imageSettings.ImageFolderPath, fileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }



                using (var imageStream = image.OpenReadStream())
                using (var original = Image.Load(imageStream))
                {
                    original.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Crop,
                        Size = new Size(targetWidth, targetHeight)
                    }));

                    await original.SaveAsync(filePath);
                }

                imageList.Add(new ProductImage
                {
                    ProductId = productId,
                    Filename = fileName
                });
            }

            await productImageRepository.AddImagesAsync(imageList);

        }


        /// <inheritdoc/>
        public async Task<List<string>> GetImageUrlsAsync(int productId)
        {
            var images = await productImageRepository.GetByProductIdAsync(productId);

            if (!images.Any())
            {
                throw new KeyNotFoundException($"No images found for product with ID {productId}.");
            }

            return images.Select(img => Path.Combine(imageSettings.ImageRequestPath, img.Filename).Replace("\\", "/")).ToList();
        }


        /// <inheritdoc/>
        public async Task UpdateImagesAsync(int productId, IFormFileCollection newImages)
        {
            // Delete old images from disk and database
            await DeleteImagesAsync(productId);

            // Upload new images
            await UploadImagesAsync(productId, newImages);
        }

    }
}
