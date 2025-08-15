using EZHotel.DTOs.Users;
using EZHotel.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace EZHotel.Components.Pages.Users
{
    public class UsersListBase : ComponentBase
    {
        public bool showModal = false;
        public Guid UserIdSelected = Guid.Empty;
        public List<UserDTO> usersDTO = null;
        [Inject] public IUserService UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            usersDTO = (await UserService.GetAll()).ToList();
        }

        public void ShowCreateModal()
        {
            showModal = true;
        }

        public void ShowUpdateModal(Guid userId)
        {
            showModal = true;
            UserIdSelected = userId;
        }

        public void HideModal()
        {
            showModal = false;
            UserIdSelected = Guid.Empty;
        }
    }
}
