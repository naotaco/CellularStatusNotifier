using System;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
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

    public class SignalLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return "-"; }

            var p = value as ConnectionProfile;
            var l = p.GetSignalBars();
            if (l == null) { return "-"; }
            return l.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class WlanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return Visibility.Collapsed; }

            var p = value as ConnectionProfile;
            if (p.IsWlanConnectionProfile) { return Visibility.Visible; }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class WwanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return Visibility.Collapsed; }

            var p = value as ConnectionProfile;
            if (p.IsWwanConnectionProfile) { return Visibility.Visible; }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class WwanConnectionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return "-"; }

            var p = value as ConnectionProfile;
            if (p.WwanConnectionProfileDetails == null) { return null; }
            switch (p.WwanConnectionProfileDetails.GetCurrentDataClass())
            {
                case WwanDataClass.Cdma1xEvdo:
                    return "CDMA 1x EVDO";
                case WwanDataClass.Cdma1xEvdoRevA:
                    return "CDMA 1x EVDO Rev. A";
                case WwanDataClass.Cdma1xEvdoRevB:
                    return "CDMA 1x EVDO Rev. B";
                case WwanDataClass.Cdma1xEvdv:
                    return "CDMA 1x EVDV";
                case WwanDataClass.Cdma1xRtt:
                    return "CDMA 1x RTT";
                case WwanDataClass.Cdma3xRtt:
                    return "CDMA 3x RTT";
                case WwanDataClass.CdmaUmb:
                    return "CDMA UMB";
                case WwanDataClass.Custom:
                    return "Other";
                case WwanDataClass.Edge:
                    return "EDGE";
                case WwanDataClass.Gprs:
                    return "GPRS";
                case WwanDataClass.Hsdpa:
                    return "HSDPA";
                case WwanDataClass.Hsupa:
                    return "HSUPA";
                case WwanDataClass.LteAdvanced:
                    return "LTE Advanced";
                case WwanDataClass.None:
                    return "None";
                case WwanDataClass.Umts:
                    return "UMTS";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class SsidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return "-"; }

            var p = value as ConnectionProfile;
            if (p.WlanConnectionProfileDetails == null) { return "-"; }
            return p.WlanConnectionProfileDetails.GetConnectedSsid();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class WwanApnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return "-"; }

            var p = value as ConnectionProfile;
            if (p.WwanConnectionProfileDetails == null) { return "-"; }
            return p.WwanConnectionProfileDetails.AccessPointName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class IpTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return "-"; }

            var p = value as ConnectionProfile;
            if (p.WwanConnectionProfileDetails == null) { return "-"; }
            switch (p.WwanConnectionProfileDetails.IPKind)
            {
                case WwanNetworkIPKind.Ipv4:
                    return "IPv4";
                case WwanNetworkIPKind.Ipv4v6:
                    return "IPv4/v6";
                case WwanNetworkIPKind.Ipv4v6v4Xlat:
                    return "464XLAT";
                case WwanNetworkIPKind.Ipv6:
                    return "IPv6";
                case WwanNetworkIPKind.None:
                    return "None";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
