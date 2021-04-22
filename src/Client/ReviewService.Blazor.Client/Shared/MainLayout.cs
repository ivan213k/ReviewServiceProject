using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReviewService.Blazor.Client.Shared
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
