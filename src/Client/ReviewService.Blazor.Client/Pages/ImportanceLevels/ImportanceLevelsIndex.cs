using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ReviewService.Blazor.Client.Pages.ImportanceLevels
{
    public partial class ImportanceLevelsIndex
    {
        private ImportanceLevelApiModel _importanceLevel;
        private List<ImportanceLevelApiModel> _importanceLevels;
        private List<ImportanceLevelApiModel> _previousLevels;

        [Inject]
        public IDialogService DialogService { get; set; }

        public bool IsOpened { get; set; } = false;
        //public string ColorField { get; set; }

        [Inject]
        public ApplicationState ApplicationState{ get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public ImportanceLevelsIndex()
        {
            _importanceLevel = new ImportanceLevelApiModel();
            _importanceLevel.Color = "#FFFFFF";
            _previousLevels = new List<ImportanceLevelApiModel>();
            _importanceLevels = new List<ImportanceLevelApiModel>();
        }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Importance Levels", SetButtons());
            _previousLevels = await HttpClient.GetFromJsonAsync<List<ImportanceLevelApiModel>>("api/ImportanceLevel");
            _importanceLevels.AddRange(_previousLevels);
        }

        public void OpenModal()
        {
            IsOpened = true;
        }

        public void ClosedEvent(string value)
        {
            _importanceLevel.Color = value;
            IsOpened = false;
        }

        public string ColorField(string color)
        {
            return "background-color:" + color;
        }

        private List<FooterButton> SetButtons()
        {
            List<FooterButton> buttons = new List<FooterButton>();
            buttons.Add(new FooterButton("Save", OnSaveClicked));
            buttons.Add(new FooterButton("Cancel", OnCancelClicked));
            return buttons;
        }

        private void AddRowClicked()
        {
            _importanceLevels.Add(_importanceLevel);
            _importanceLevel = new ImportanceLevelApiModel();
            StateHasChanged();
        }

        private async void OnDeleteClicked(ImportanceLevelApiModel level)
        {
            var message = $"Actually delete\"{level.Name}\" importance level?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteLevel(level);
            }
        }

        public void DeleteLevel(ImportanceLevelApiModel level)
        {
            _importanceLevels.Remove(level);
            StateHasChanged();
        }

        private void OnCancelClicked()
        {
            _importanceLevels.Clear();
            _importanceLevels.AddRange(_previousLevels);
            StateHasChanged();
            NavigationManager.NavigateTo("/importancelevels");
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
