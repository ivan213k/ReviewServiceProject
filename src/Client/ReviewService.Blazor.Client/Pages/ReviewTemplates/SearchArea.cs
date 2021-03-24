using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewTemplates
{
    public partial class SearchArea
    {
        private List<AreaApiModel> areas;
        private Timer timer;
        public SearchArea()
        {
            areas = new List<AreaApiModel>();
        }
        public string SearchTerm { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Parameter]
        public Action<AreaApiModel> OnAddAreaClicked { get; set; }

        private async Task<List<AreaApiModel>> LoadAreas() 
        {
            return await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
        }
        private void SearchChanged()
        {
            if (timer != null)
                timer.Dispose();
            timer = new Timer(OnTimerElapsed, null, 500, 0);
        }
        private async void OnTimerElapsed(object sender)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                areas.Clear();
            }
            else
            {
                areas = (await LoadAreas()).Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower())).ToList();
            }
            StateHasChanged();
            timer.Dispose();
        }
        private void AddAreaClicked(AreaApiModel area)
        {
            SearchTerm = "";
            areas.Clear();
            OnAddAreaClicked?.Invoke(area);
        }
    }
}
