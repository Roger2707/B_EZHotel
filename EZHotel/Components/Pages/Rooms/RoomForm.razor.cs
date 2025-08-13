using EZHotel.DTOs.Rooms;
using EZHotel.Helpers;
using EZHotel.Hubs;
using EZHotel.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR;

namespace EZHotel.Components.Pages.Rooms
{
    public class RoomFormBase : ComponentBase
    {
        [Parameter] public Guid RoomId { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        public string? errorMessage;
        public List<string> ImagePreviews = new();
        public List<IBrowserFile> SelectedFiles = new();
        public RoomUpsertDTO Model { get; set; } = new RoomUpsertDTO();
        [Inject] public IRoomService RoomService { get; set; }
        [Inject] public IUploadService UploadService { get; set; }
        [Inject] public IHubContext<RoomHub> RoomHubContext { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Model = await GetRoomUpsertDTO();
            ImagePreviews = Model.ImageUrl != null ? Model.ImageUrl.Split(',').ToList() : new List<string>();
        }

        private async Task<RoomUpsertDTO> GetRoomUpsertDTO()
        {
            var existedRoomDTO = await RoomService.GetByIdAsync(RoomId);
            if (existedRoomDTO == null) return new RoomUpsertDTO();
            return new RoomUpsertDTO
            {
                Name = existedRoomDTO.Name,
                Description = existedRoomDTO.Description,
                Capacity = existedRoomDTO.Capacity,
                RoomType = existedRoomDTO.RoomType,
                Price = existedRoomDTO.Price,
                ImageUrl = existedRoomDTO.ImageUrl,
                PublicId = existedRoomDTO.PublicId,
                IsAvailable = existedRoomDTO.IsAvailable
            };
        }

        #region Column Value Changed

        public void OnCapacityChanged(int capacity)
        {
            Model.Capacity = capacity;
            Model.Price = CF.GetDecimal(500000 * capacity * BF.GetRateRoom(Model.RoomType));
        }

        public void OnRoomTypeChanged(RoomType roomType)
        {
            Model.RoomType = roomType;
            Model.Price = CF.GetDecimal(500000 * Model.Capacity * BF.GetRateRoom(roomType));
        }

        #endregion

        #region Images Upload (UI handle)

        public async Task OnImagesSelected(InputFileChangeEventArgs e)
        {
            ImagePreviews.Clear();
            SelectedFiles.Clear();

            foreach (var file in e.GetMultipleFiles())
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).ReadAsync(buffer);

                var base64 = Convert.ToBase64String(buffer);
                var imageDataUrl = $"data:{file.ContentType};base64,{base64}";

                ImagePreviews.Add(imageDataUrl);

                // Update the Model's ImageUrl property
                Model.ImageUrl = string.Join(',', ImagePreviews);

                // Add the file to the SelectedFiles list for further processing
                SelectedFiles.Add(file);
            }
        }

        #endregion

        #region Save

        protected async Task HandleValidSubmit()
        {
            errorMessage = null;
            try
            {
                if (OnValidSubmit.HasDelegate)
                {
                    await HandleSaveImages();

                    if (RoomId == Guid.Empty) await HandleCreate();
                    else await HandleUpdate();

                    // Notify all clients about the room update
                    RoomDTO propRoomDTO = new RoomDTO
                    {
                        Id = RoomId,
                        Name = Model.Name,
                        Description = Model.Description,
                        Capacity = Model.Capacity,
                        RoomType = Model.RoomType,
                        Price = Model.Price,
                        ImageUrl = Model.ImageUrl,
                        PublicId = Model.PublicId,
                        IsAvailable = Model.IsAvailable,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await RoomHubContext.Clients.All.SendAsync("RoomSaveChanged", propRoomDTO);

                    await OnValidSubmit.InvokeAsync();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task HandleSaveImages()
        {
            string folder = $"rooms/{Model.Name}";
            if (SelectedFiles == null || SelectedFiles.Count == 0) return;

            var result = await UploadService.AddMultipleImageAsync(SelectedFiles, folder);
            if (result == null) throw new Exception("Failed to upload images.");

            // Delete previous images if PublicId is not empty
            if (!string.IsNullOrEmpty(Model.PublicId))
                await UploadService.DeleteMultipleImageAsync(Model.PublicId);

            Model.ImageUrl = string.Join(',', result.Select(x => x.SecureUrl.ToString()));
            Model.PublicId = string.Join(',', result.Select(x => x.PublicId));
        }

        private async Task HandleCreate()
        {
            RoomId = await RoomService.CreateAsync(Model);
        }

        private async Task HandleUpdate()
        {
            await RoomService.UpdateAsync(RoomId, Model);
        }

        #endregion
    }
}
