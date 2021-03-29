using System;
using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.State;

namespace ReviewService.Blazor.Client.Layout
{
    public partial class HeaderModel: ComponentBase, IDisposable
    {
        public string Title { get; set; }

        [Inject]
        private ApplicationState ApplicationState { get; set; }

        public void TitleChanged()
        {
            Title = ApplicationState.HeaderTitle;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            ApplicationState.OnChange += TitleChanged;
            base.OnInitialized();
        }

        public void Dispose()
        {
            ApplicationState.OnChange -= TitleChanged;
        }



    }
}
