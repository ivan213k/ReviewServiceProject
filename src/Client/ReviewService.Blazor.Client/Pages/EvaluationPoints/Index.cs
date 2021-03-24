using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewService.Shared.ApiModels;
using ReviewService.Blazor.Client.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace ReviewService.Blazor.Client.Pages.EvaluationPoints
{
    public partial class Index
    {
        private List<EvaluationPointsTemplateApiModel> _evaluationpoints;

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _evaluationpoints = await HttpClient.GetFromJsonAsync<List<EvaluationPointsTemplateApiModel>>("api/EvaluationPoint");
        }

        private void OnAddEvaluationClicked()
        {
            NavigationManager.NavigateTo("/evaluationpoints/add");
        }

    }
}
