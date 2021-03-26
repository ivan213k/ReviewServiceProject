using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using ReviewService.Blazor.Client.Components;

namespace ReviewService.Blazor.Client.Layout
{
    public partial class Header
    {
        [Parameter]
        public string Value { get; set; }
    }
}
