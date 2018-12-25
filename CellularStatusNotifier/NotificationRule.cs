using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularStatusNotifier
{
    public class ProfileComparisonRule
    {
        // common
        public bool CompareMethod { get; set; } = true;
        public bool CompareProfileName { get; set; } = true;
        public bool CompareSigmalBars { get; set; } = true;

        // Wwan
        public bool CompareApn { get; set; } = true;
        public bool CompareDataClass { get; set; } = true;
        public bool CompareIPKind { get; set; } = true;

        // Wlan
        public bool CompareSsid { get; set; } = true;

    }
}
