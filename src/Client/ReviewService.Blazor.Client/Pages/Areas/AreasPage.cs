using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.State;
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
        
        [Inject]
        public ApplicationState ApplicationState { get; set; }
        
        [Inject]
        public HttpClient HttpClient { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
         
        protected override async Task OnInitializedAsync()
        {
            //ApplicationState.SetHeaderTitle("Areas");
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
        private async void OnDeleteClicked(AreaApiModel area)
        {
            var message = $"Actually delete\"{area.Name}\" area ?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteArea(area);
            }
        }
        private async void DeleteArea(AreaApiModel area)
        {
            await HttpClient.DeleteAsync($"api/Area/{area.Id}");
            areas = await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
            StateHasChanged();
        }
    }
}
