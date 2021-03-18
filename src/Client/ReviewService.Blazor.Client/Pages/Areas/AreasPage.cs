using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    public partial class AreasPage
    {
        private List<AreaApiModel> areas;

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
    }
}
