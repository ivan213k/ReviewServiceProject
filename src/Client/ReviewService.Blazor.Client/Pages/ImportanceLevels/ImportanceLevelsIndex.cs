using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.Layout;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ReviewService.Blazor.Client.Pages.ImportanceLevels
{
    public partial class ImportanceLevelsIndex
    {
        private ImportanceLevelApiModel _importanceLevel;
        private ImportanceLevelApiModel _deleteImportanceLevel;
        private DeleteConfirmation _deleteConfirmation;
        private List<ImportanceLevelApiModel> _importanceLevels;
        private List<ImportanceLevelApiModel> _previousLevels;

        [Inject]
        public ApplicationState ApplicationState{ get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public ImportanceLevelsIndex()
        {
            _importanceLevel = new ImportanceLevelApiModel();
            _previousLevels = new List<ImportanceLevelApiModel>();
            _importanceLevels = new List<ImportanceLevelApiModel>();
            _deleteImportanceLevel = new ImportanceLevelApiModel();
        }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetHeaderTitle("Importance Levels");
            _previousLevels = await HttpClient.GetFromJsonAsync<List<ImportanceLevelApiModel>>("api/ImportanceLevel");
            _importanceLevels.AddRange(_previousLevels);
        }

        private void AddRowClicked()
        {
            _importanceLevels.Add(_importanceLevel);
            _importanceLevel = new ImportanceLevelApiModel();
            StateHasChanged();
        }

        private void OnDeleteClicked(ImportanceLevelApiModel level)
        {
            _deleteImportanceLevel = level;
            _deleteConfirmation.Show($"Actually delete\"{level.Name}\" importance level?");
        }

        public void DeleteLevel()
        {
            _importanceLevels.Remove(_deleteImportanceLevel);
            StateHasChanged();
        }

        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/");
        }

        private async void OnSaveClicked()
        {
            foreach(var level in _importanceLevels)
            {
                if(!_previousLevels.Contains(level))
                {
                    await HttpClient.PostAsJsonAsync("api/ImportanceLevel", level);
                }
            }
            foreach(var level in _previousLevels)
            {
                if(!_importanceLevels.Contains(level))
                {
                    await HttpClient.DeleteAsync($"api/ImportanceLevel/{level.Id}");
                }
            }
            NavigationManager.NavigateTo("/importancelevels");
        }
    }
}
