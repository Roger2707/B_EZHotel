using EZHotel.DTOs.Rooms;
using Microsoft.AspNetCore.Components;

namespace EZHotel.Components.Pages.Rooms
{
    public class RoomFormBase : ComponentBase
    {
        [Parameter] public string TitleForm { get; set; }
        [Parameter] public RoomUpsertDTO Model { get; set; } = new();
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        public string? errorMessage;

        protected async Task HandleValidSubmit()
        {
            errorMessage = null;
            try
            {
                if (OnValidSubmit.HasDelegate)
                {
                    await OnValidSubmit.InvokeAsync();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
