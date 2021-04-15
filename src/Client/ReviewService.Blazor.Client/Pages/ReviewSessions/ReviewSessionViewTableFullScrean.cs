using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using System;
using System.Collections.Generic;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionViewTableFullScrean
    {
        [Parameter]
        public int SessionId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        protected override void OnInitialized()
        {
            ApplicationState.SetState("", CreateFooterButtons());
        }

        private List<FooterButton> CreateFooterButtons()
        {
            return new List<FooterButton>()
            {
                new FooterButton("Back to review session", BackToSessionClicked)
            };
        }

        private void BackToSessionClicked() 
        {
            NavigationManager.NavigateTo($"/reviewSessionEdit/{SessionId}");
        }
    }
}
