using EZHotel.DTOs.Rooms;
using EZHotel.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace EZHotel.Components.Pages.Rooms
{
    public class RoomCrudBase : ComponentBase
    {
        public string titleForm = "";
        public bool showModal = false;
        public List<RoomDTO> rooms = null;
        public RoomUpsertDTO roomUpserDTO = new();
        private Guid roomUpdateId;
        [Inject] public IRoomService RoomService { get; set; }

        #region Init / Load
        protected override async Task OnInitializedAsync()
        {
            await LoadRooms();
        }

        public async Task LoadRooms()
        {
            rooms = (await RoomService.GetAllAsync()).ToList();
        }

        #endregion

        #region CRUD

        public async Task HandleSubmit()
        {
            try
            {
                if (titleForm == "Create") await HandleCreate();
                else if (titleForm == "Update") await HandleUpdate();

                HideModal();
            }
            catch (Exception ex)
            {
                // Handle validation errors
               throw new Exception($"Validation error: {ex.Message}");
            }
        }

        private async Task HandleCreate()
        {
            // Create and Update DB new Room
            await RoomService.CreateAsync(roomUpserDTO);

            // Update UI
            rooms.Add(new RoomDTO
            {
                Id = Guid.NewGuid(),
                Name = roomUpserDTO.Name,
                Description = roomUpserDTO.Description,
                Price = roomUpserDTO.Price,
                Capacity = roomUpserDTO.Capacity,
                ImageUrl = roomUpserDTO.ImageUrl,
                PublicId = roomUpserDTO.PublicId,
                IsAvailable = roomUpserDTO.IsAvailable,
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        private async Task HandleUpdate()
        {
            // Update DB Room
            await RoomService.UpdateAsync(roomUpdateId, roomUpserDTO);

            // Update UI
            var room = rooms.FirstOrDefault(r => r.Id == roomUpdateId);

            if (room != null)
            {
                room.Name = roomUpserDTO.Name;
                room.Description = roomUpserDTO.Description;
                room.Price = roomUpserDTO.Price;
                room.Capacity = roomUpserDTO.Capacity;
                room.ImageUrl = roomUpserDTO.ImageUrl;
                room.PublicId = roomUpserDTO.PublicId;
                room.IsAvailable = roomUpserDTO.IsAvailable;
                room.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #endregion

        #region Modal Handlers

        public void ShowCreateModal()
        {
            roomUpserDTO = new();
            showModal = true;
            titleForm = "Create";
        }

        public async void ShowUpdateModal(Guid roomId)
        {
            var existedRoom = await RoomService.GetByIdAsync(roomId);

            roomUpdateId = roomId;
            // bind data to form
            roomUpserDTO = new RoomUpsertDTO
            {
                Name = existedRoom.Name,
                Description = existedRoom.Description,
                Price = existedRoom.Price,
                Capacity = existedRoom.Capacity,
                ImageUrl = existedRoom.ImageUrl,
                PublicId = existedRoom.PublicId,
                IsAvailable = existedRoom.IsAvailable
            };
            showModal = true;
            titleForm = "Update";
            StateHasChanged();
        }

        public void HideModal()
        {
            showModal = false;
        }

        #endregion
    }
}
