using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewService.Shared.ApiModels;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Components;

namespace ReviewService.Blazor.Client.Pages.ImportanceLevels
{
    public partial class Index
    {
        private ImportanceLevelApiModel _importanceLevel;
        private ImportanceLevelApiModel _deleteImportanceLevel;
        private DeleteConfirmation _deleteConfirmation;
        private List<ImportanceLevelApiModel> _importanceLevels;
        private List<ImportanceLevelApiModel> _newLevels;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public Index()
        {
            _importanceLevel = new ImportanceLevelApiModel();
            _newLevels = new List<ImportanceLevelApiModel>();
            _deleteImportanceLevel = new ImportanceLevelApiModel();
        }

        protected override async Task OnInitializedAsync()
        {
            _importanceLevels = await HttpClient.GetFromJsonAsync<List<ImportanceLevelApiModel>>("api/ImportanceLevel");
        }

        private void AddRowClicked()
        {
            _newLevels.Add(_importanceLevel);
            _importanceLevels.Add(_importanceLevel);
            _importanceLevel = new ImportanceLevelApiModel();
            StateHasChanged();
        }

        private void OnDeleteClicked(ImportanceLevelApiModel level)
        {
            _deleteImportanceLevel = level;
            _deleteConfirmation.Show($"Actually delete\"{level.Name}\" importance level?");
        }

        public async void DeleteLevel()
        {
            await HttpClient.DeleteAsync($"api/ImportanceLevel/{_deleteImportanceLevel.Id}");
            _importanceLevels = await HttpClient.GetFromJsonAsync<List<ImportanceLevelApiModel>>("api/ImportanceLevel");
            StateHasChanged();
        }

        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/");
        }

        private async void OnSaveClicked()
        {
            foreach(var level in _newLevels)
            {
                await HttpClient.PostAsJsonAsync("api/ImportanceLevel", level);
            }
            NavigationManager.NavigateTo("/importancelevels");
        }
    }
}
