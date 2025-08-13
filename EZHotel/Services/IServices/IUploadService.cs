using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Components.Forms;

namespace EZHotel.Services.IServices
{
    public interface IUploadService
    {
        public Task<List<ImageUploadResult>> AddMultipleImageAsync(List<IBrowserFile> files, string folderPath);
        public Task<DeletionResult> DeleteImageAsync(string publicId);
        public Task DeleteMultipleImageAsync(string publicId);
    }
}
