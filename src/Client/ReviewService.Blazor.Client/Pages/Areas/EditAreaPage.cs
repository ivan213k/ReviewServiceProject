using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    partial class EditAreaPage
    {
        private AreaApiModel area;
        private AddAreaItemDialog addAreaItemDialog;
        private EditForm editForm;
        private ErrorMessage errorMessage;

        [Parameter]
        public int AreaId { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetHeaderTitle("Edit Area");
            area = await HttpClient.GetFromJsonAsync<AreaApiModel>($"api/Area/{AreaId}");
        }
        private void AddRowClicked()
        {
            addAreaItemDialog.Show();
        }
        private void AddAreaItemClicked()
        {
            area.AreaItems.Add(addAreaItemDialog.AreaItem);
            StateHasChanged();
        }
        private void DeleteRow(AreaItemApiModel areaItem)
        {
            area.AreaItems.Remove(areaItem);
        }
        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/areas");
        }
        private async void OnSaveClicked()
        {
            if (editForm.EditContext.Validate())
            {
                var responseMessage =  await HttpClient.PutAsJsonAsync("api/Area", area);
                if (responseMessage.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/areas");
                }
                else
                {
                    var error = await responseMessage.Content.ReadFromJsonAsync<ProblemDetails>();

                    errorMessage.Show($"{responseMessage.ReasonPhrase}: {error.Title} {error.Detail}");
                }
            }
        }
    }
}
