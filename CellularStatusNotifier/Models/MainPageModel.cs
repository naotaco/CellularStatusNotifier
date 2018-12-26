using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Networking.Connectivity;

namespace CellularStatusNotifier
{

    public class MainPageModel : INotifyPropertyChanged
    {
        public ConnectionProfile CurrentStatus
        {
            get { return currentStatus; }
            set { this.currentStatus = value; this.OnPropertyChanged(); }
        }
        private ConnectionProfile currentStatus = null;
        public ProfileComparisonRule Rule
        {
            get
            {
                if (rule == null) { return defaultRule; }
                else { return rule; }
            }
            set { this.rule = value; }
        }
        private ProfileComparisonRule rule = null;
        private readonly ProfileComparisonRule defaultRule = new ProfileComparisonRule();

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
