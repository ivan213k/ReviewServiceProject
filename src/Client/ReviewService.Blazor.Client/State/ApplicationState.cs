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

        public event Action OnChangeTitle;

        public event Action OnChangeFooter;

        public event Action OnChange;

        public List<FooterButton> FooterButtons { get; set; }

        public void SetHeaderTitle(string value)
        {
            HeaderTitle = value;
            NotifyStateTitleChanged();
        }

        public void SetButtons(List<FooterButton> buttons)
        {
            FooterButtons = new List<FooterButton>();
            FooterButtons.AddRange(buttons);
            NotifyStateFooterChanged();
        }

        public void Set(string value, List<FooterButton> buttons)
        {
            HeaderTitle = value;
            FooterButtons = new List<FooterButton>();
            FooterButtons.AddRange(buttons);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
        private void NotifyStateTitleChanged() => OnChangeTitle?.Invoke();
        private void NotifyStateFooterChanged() => OnChangeFooter?.Invoke();
    }
}
