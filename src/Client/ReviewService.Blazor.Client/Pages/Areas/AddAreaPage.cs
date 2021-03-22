using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    public partial class AddAreaPage
    {
        private AreaApiModel area;
        private AddAreaItemDialog addAreaItemDialog;
        private EditForm editForm;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }
        public AddAreaPage()
        {
            area = new AreaApiModel();
            area.AreaItems = new List<AreaItemApiModel>();
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
                await HttpClient.PostAsJsonAsync("api/Area", area);
                NavigationManager.NavigateTo("/areas");
            } 
        }
    }
}
