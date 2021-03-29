using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    public partial class AddAreaPage
    {
        private AreaApiModel area;
        private EditForm editForm;
        private ErrorMessage errorMessage;
        private AreaItemApiModel areaItem;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public AddAreaPage()
        {
            area = new AreaApiModel();
            area.AreaItems = new List<AreaItemApiModel>();
            areaItem = new AreaItemApiModel();
        }

        protected override void OnInitialized()
        {
            ApplicationState.SetHeaderTitle("Area Add");
        }

        private void AddRowClicked()
        {
            if (area.AreaItems.Any(a=>a.Name == areaItem.Name))
            {
                return;
            }
            area.AreaItems.Add(new AreaItemApiModel 
            {
                Name= areaItem.Name, Description = areaItem.Description 
            });
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
                var responseMessage = await HttpClient.PostAsJsonAsync("api/Area", area);
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
