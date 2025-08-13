using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EZHotel.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;

namespace EZHotel.Services
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary _cloudinary;
        public UploadService(IConfiguration config)
        {
            var acc = new Account
            (
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(acc);
        }

        #region Add

        public async Task<List<ImageUploadResult>> AddMultipleImageAsync(List<IBrowserFile> files, string folderPath)
        {
            var uploadResults = new List<ImageUploadResult>();

            foreach (var file in files)
            {
                using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10MB limit
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Folder = folderPath
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                uploadResults.Add(uploadResult);
            }

            return uploadResults;
        }

        #endregion

        #region Delete

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public async Task DeleteMultipleImageAsync(string publicId)
        {
            if (publicId.Length == 0) return;
            try
            {
                var delParams = new DelResParams
                {
                    PublicIds = publicId.Split(",").ToList(),
                    ResourceType = ResourceType.Image
                };

                await _cloudinary.DeleteResourcesAsync(delParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
