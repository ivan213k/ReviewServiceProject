using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        [Parameter]
        public int AreaId { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
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
                await HttpClient.PutAsJsonAsync("api/Area", area);
                NavigationManager.NavigateTo("/areas");
            }
        }
    }
}
