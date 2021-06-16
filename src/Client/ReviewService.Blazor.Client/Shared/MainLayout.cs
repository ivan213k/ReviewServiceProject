using Microsoft.AspNetCore.Components;


namespace ReviewService.Blazor.Client.Shared
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
        
        private void ClickLogOut()
        {
            NavigationManager.NavigateTo("/logout");
        }
    }
}
