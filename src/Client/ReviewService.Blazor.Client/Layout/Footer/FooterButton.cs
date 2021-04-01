using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Layout.Footer
{
    public class FooterButton
    {
        public string Title { get; set; }
        public Action OnClick { get; set; }

        public FooterButton(string title, Action action)
        {
            Title = title;
            OnClick = action;
        }
    }
}
