using ReviewService.Blazor.Client.Layout.Footer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.State
{
    public class ApplicationState
    {
        public string HeaderTitle { get; set; } = "Review Service";

        public event Action OnChange;

        public List<FooterButton> FooterButtons { get; set; } = new List<FooterButton>();

        public void SetState(string value, List<FooterButton> buttons = null)
        {
            HeaderTitle = value;
            if(buttons != null)
            {
                FooterButtons.Clear();
                FooterButtons.AddRange(buttons);
            }
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
