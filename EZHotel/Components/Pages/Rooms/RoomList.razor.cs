using EZHotel.DTOs.Rooms;
using EZHotel.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace EZHotel.Components.Pages.Rooms
{
    public class RoomCrudBase : ComponentBase, IAsyncDisposable
    {
        public bool showModal = false;
        public List<RoomDTO> rooms = null;
        public Guid RoomIdSelected;
        [Inject] public IRoomService RoomService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public HubConnection? hubConnection;

        #region Init / Load
        protected override async Task OnInitializedAsync()
        {
            await LoadRooms();

            #region Connect SignalR

            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/roomHub"))
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<RoomDTO>("RoomSaveChanged", (room) =>
            {
                var existing = rooms.FirstOrDefault(r => r.Id == room.Id);
                if (existing != null)
                {
                    existing.Id = room.Id;
                    existing.Name = room.Name;
                    existing.Description = room.Description;
                    existing.Capacity = room.Capacity;
                    existing.RoomType = room.RoomType;
                    existing.ImageUrl = room.ImageUrl;
                    existing.PublicId = room.PublicId;
                    existing.Price = room.Price;
                    existing.IsAvailable = room.IsAvailable;
                    existing.UpdatedAt = room.UpdatedAt;
                }
                else 
                {
                    rooms.Add(room);
                    rooms.OrderByDescending(r => r.UpdatedAt).ToList();
                }

                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

            #endregion
        }

        public async Task LoadRooms()
        {
            rooms = (await RoomService.GetAllAsync()).ToList();
        }

        #endregion

        #region CRUD

        public async Task HandleSubmit()
        {
            HideModal();

            // Handle Update UI
            await LoadRooms();
        }

        #endregion

        #region Modal Handlers

        public void ShowCreateModal()
        {
            RoomIdSelected = Guid.Empty;
            showModal = true;
        }

        public void ShowUpdateModal(Guid roomId)
        {
            RoomIdSelected = roomId;
            showModal = true;
        }

        public void HideModal()
        {
            showModal = false;
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
                await hubConnection.DisposeAsync();
        }

        #endregion
    }
}
