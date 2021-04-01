using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.State;

namespace ReviewService.Blazor.Client.Layout.Footer
{
    public partial class FooterModel: ComponentBase, IDisposable
    {
        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            ApplicationState.OnChange += StateHasChanged;
            base.OnInitialized();
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
           //ToDo clean up buttons
        }

        public void Dispose()
        {
            ApplicationState.OnChange -= StateHasChanged;
        }




    }
}
