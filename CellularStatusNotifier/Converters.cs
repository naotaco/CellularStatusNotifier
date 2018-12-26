using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml.Data;

namespace CellularStatusNotifier
{
    public class ConnectionMethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return "-"; }

            var p = value as ConnectionProfile;
            if (p.IsWwanConnectionProfile) { return "WWan"; }
            if (p.IsWlanConnectionProfile) { return "WLan"; }
            return "No connection;";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Only for OneWay binding.
            throw new NotImplementedException();
        }
    }
}
