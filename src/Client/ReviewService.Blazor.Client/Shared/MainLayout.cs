using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReviewService.Blazor.Client.Shared
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;

        

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
