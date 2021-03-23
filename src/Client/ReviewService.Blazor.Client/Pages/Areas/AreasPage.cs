using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Components;
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
        private DeleteConfirmation deleteConfirmationDialog;
        private AreaApiModel areaForDeletion;
        
        [Inject]
        public HttpClient HttpClient { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            areas = await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
        }

        private void OnAddNewAreaClicked() 
        {
            NavigationManager.NavigateTo("/addArea");
        }
        private void OnDeleteClicked(AreaApiModel area)
        {
            areaForDeletion = area;
            deleteConfirmationDialog.Show($"Actually delete\"{area.Name}\" area ?");
        }
        private async void DeleteArea()
        {
            await HttpClient.DeleteAsync($"api/Area/{areaForDeletion.Id}");
            areas = await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
            StateHasChanged();
        }
    }
}
