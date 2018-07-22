using System;
using System.Runtime.InteropServices;

namespace AssocSupport.Natives
{
    static class NativeMethods
    {
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(HChangeNotifyEventID wEventId, HChangeNotifyFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);
    }
}
