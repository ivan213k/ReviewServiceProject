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

        public void SetHeaderTitle(string value)
        {
            HeaderTitle = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
