using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using ReviewService.Blazor.Client.Components;

namespace ReviewService.Blazor.Client.Pages.EvaluationPoints
{
    public partial class Create
    {
        private EvaluationPointsTemplateApiModel _evaluationPointsTemplate;

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Create()
        {
            _evaluationPointsTemplate = new EvaluationPointsTemplateApiModel();
            _evaluationPointsTemplate.EvaluationPoints = new List<EvaluationPointApiModel>();
        }

    }
}
