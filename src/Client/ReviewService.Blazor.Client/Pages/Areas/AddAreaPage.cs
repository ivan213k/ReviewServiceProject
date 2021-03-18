using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Areas
{
    public partial class AddAreaPage
    {
        private AreaApiModel area;

        public AddAreaPage()
        {
            area = new AreaApiModel();
            area.AreaItems = new List<AreaItemApiModel>();
        }
    }
}
