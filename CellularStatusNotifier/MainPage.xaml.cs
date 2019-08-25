using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Windows.Networking.Connectivity;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CellularStatusNotifier
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ProfileComparisonRule rule = new ProfileComparisonRule();
        ConnectionProfile lastProfile = null;

        public MainPageModel ViewModel { get; set; } = new MainPageModel();

        public MainPage()
        {
            this.InitializeComponent();
            Debug.WriteLine("Hello");

            ViewModel.Rule = rule;
            ViewModel.CurrentStatus = lastProfile;


            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            var profiles = NetworkInformation.GetConnectionProfiles();
            PrintStatus(profiles);

            var p = NetworkInformation.GetInternetConnectionProfile();
            NotifyCurrentStatus(p);
            this.ViewModel.CurrentStatus = p;

            var PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                var temp_last = this.ViewModel.CurrentStatus;
                var temp_current = NetworkInformation.GetInternetConnectionProfile();

                await Dispatcher.RunAsync(CoreDispatcherPriority.High,
                    () =>
                    {
                        this.ViewModel.CurrentStatus = temp_current;

                        if (!temp_last.IsSameCondition(temp_current, rule))
                        {
                            NotifyCurrentStatus(temp_current);
                        }
                    });

            }, TimeSpan.FromSeconds(3));
        }

        private static void NotifyCurrentStatus(ConnectionProfile p)
        {
            if (p == null) {
                Debug.WriteLine("No service.");
                InvokeNotification("No service.");
                return;
            }

            if (p.IsWwanConnectionProfile)
            {
                Debug.WriteLine("Current: WWan");
                var sb = new StringBuilder();
                sb.AppendLine("WWan: " + p.GetNetworkNames().Aggregate((a, b) => a + " " + b));

                if (p.GetSignalBars() != null)
                {
                    sb.AppendLine("Level: " + p.GetSignalBars().Value);
                }

                var detail = p.WwanConnectionProfileDetails;
                if (detail != null)
                {
                    sb.AppendLine("APN: " + detail.AccessPointName);
                    sb.AppendLine("IPKind: " + detail.IPKind);
                    sb.AppendLine("Registration state: " + detail.GetNetworkRegistrationState().ToString());
                    sb.AppendLine("Connection type: " + detail.GetCurrentDataClass().ToString());
                }

                InvokeNotification(sb.ToString());
            }
            else if (p.IsWlanConnectionProfile)
            {
                Debug.WriteLine("Current: WLan");
                var sb = new StringBuilder();
                sb.Append("WLan: " + p.ProfileName);
                sb.Append(" ");
                sb.Append(p.GetNetworkNames().Aggregate((a, b) => a + " " + b));
                sb.Append(" Level: ");
                if (p.GetSignalBars() != null)
                {
                    sb.Append(p.GetSignalBars().Value);
                }
                InvokeNotification(sb.ToString());
            }
            else
            {
                Debug.WriteLine("No service.");
                InvokeNotification("No service.");
            }
        }

        static void InvokeNotification(string text)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText() { Text = text }
                    }
                }
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddSeconds(5);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            var profiles = NetworkInformation.GetConnectionProfiles();
            PrintStatus(profiles);
            var p = NetworkInformation.GetInternetConnectionProfile();
            if (p != null)
            {
                NotifyCurrentStatus(p);
            }
        }

        private static void PrintStatus(IReadOnlyList<ConnectionProfile> profiles)
        {
            if (profiles != null)
            {
                Debug.WriteLine("ok");
            }
            else
            {
                return;
            }


            foreach (var p in profiles.Where(p => p != null))
            {

                if (p.IsWlanConnectionProfile)
                {
                    Debug.WriteLine("Wi-Fi ");
                    foreach (var n in p.GetNetworkNames())
                    {
                        Debug.WriteLine(n);
                    }
                }
                else if (p.IsWwanConnectionProfile)
                {
                    Debug.WriteLine("WWan");
                }
                foreach (var n in p.GetNetworkNames())
                {
                    Debug.WriteLine(n);
                }
            }
        }
    }
}
