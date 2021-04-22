using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.Services;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    partial class EditAreaPage : IDisposable
    {
        private AreaApiModel area;
        private EditForm editForm;
        private AreaItemApiModel areaItem;

        [Parameter]
        public int AreaId { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Edit Area",CreateFooterButtons());
            area = await HttpClient.GetFromJsonAsync<AreaApiModel>($"api/Area/{AreaId}");
            areaItem = new AreaItemApiModel();
        }
        private List<FooterButton> CreateFooterButtons()
        {
            List<FooterButton> buttons = new List<FooterButton>()
            {
                new FooterButton("Cancel", OnCancelClicked),
                new FooterButton("Save", OnSaveClicked)
            };
            return buttons;
        }
        private void AddRowClicked()
        {
            if (area.AreaItems.Any(a => a.Name == areaItem.Name))
            {
                return;
            }
            area.AreaItems.Add(new AreaItemApiModel
            {
                Name = areaItem.Name,
                Description = areaItem.Description
            });
        }
        private void DeleteRow(AreaItemApiModel areaItem) => area.AreaItems.Remove(areaItem);
        private void OnCancelClicked() => NavigationManager.NavigateTo("/areas");
        private async void OnSaveClicked()
        {
            if (editForm.EditContext.Validate())
            {
                var responseMessage =  await HttpClient.PutAsJsonAsync("api/Area", area);
                if (responseMessage.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/areas");
                }
            }
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
