using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    public partial class AreasPage
    {
        private List<AreaApiModel> areas;
        private AreaApiModel areaForDeletion;
        
        [Inject]
        public HttpClient HttpClient { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
         
        protected override async Task OnInitializedAsync()
        {
            areas = await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
        }

        private void OnAddNewAreaClicked() 
        {
            NavigationManager.NavigateTo("/addArea");
        }

        private void OnEditClicked(int areaId)
        {
            NavigationManager.NavigateTo($"/editArea/{areaId}");
        }
        private void OnDeleteClicked(AreaApiModel area)
        {
            areaForDeletion = area;
            var message = $"Actually delete\"{area.Name}\" area ?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        }
        private async void DeleteArea()
        {
            await HttpClient.DeleteAsync($"api/Area/{areaForDeletion.Id}");
            areas = await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
            StateHasChanged();
        }
    }
}
