using Windows.Networking.Connectivity;


namespace CellularStatusNotifier
{
    public static class ConnectionProfileUtils
    {
        public static bool IsSameCondition(this ConnectionProfile self, ConnectionProfile target, ProfileComparisonRule rule)
        {
            if (self == null || target ==null || rule == null) { return false; }

            if (
                ((!rule.CompareProfileName) || self.ProfileName == target.ProfileName) &&
                ((!rule.CompareSigmalBars) || self.GetSignalBars() == target.GetSignalBars())
                )

            {
                // no difference
            }
            else
            {
                return false;
            }

            if (self.IsWlanConnectionProfile && target.IsWlanConnectionProfile)
            {
                if (self.WlanConnectionProfileDetails == null ||
                    target.WlanConnectionProfileDetails == null) { return false; }

                // compare Wlan detail info
                return (!rule.CompareSsid) || self.WlanConnectionProfileDetails.GetConnectedSsid() == target.WlanConnectionProfileDetails.GetConnectedSsid();
            }
            else if (self.IsWwanConnectionProfile && target.IsWwanConnectionProfile)
            {
                if (self.WwanConnectionProfileDetails == null ||
                    target.WwanConnectionProfileDetails == null) { return false; }

                // compare wwan detail.
                var selfD = self.WwanConnectionProfileDetails;
                var targetD = target.WwanConnectionProfileDetails;
                return (
                    ((!rule.CompareApn) || selfD.AccessPointName == targetD.AccessPointName) &&
                    ((!rule.CompareDataClass) || selfD.GetCurrentDataClass() == targetD.GetCurrentDataClass()) &&
                    ((!rule.CompareIPKind) || selfD.IPKind == targetD.IPKind)
                    );
            }
            else
            {
                return !rule.CompareMethod;
            }
        }
    }
}
