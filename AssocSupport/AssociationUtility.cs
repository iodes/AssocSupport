using AssocSupport.Models;
using AssocSupport.Natives;
using Microsoft.Win32;
using System;

namespace AssocSupport
{
    public static class AssociationUtility
    {
        public static bool Register(Software target)
        {
            // Software 등록
            var softwarePath = $@"Software\{target.CompanyName}\{target.Name}\Capabilities";
            if (Registry.LocalMachine.OpenSubKey(softwarePath) == null)
            {
                var softwareKey = Registry.LocalMachine.CreateSubKey(softwarePath);
                softwareKey.SetValue(RegistryKeys.ApplicationName, target.Name);
                softwareKey.SetValue(RegistryKeys.ApplicationIcon, target.Icon);
                softwareKey.SetValue(RegistryKeys.ApplicationDescription, target.Description);

                var registeredKey = Registry.LocalMachine.OpenSubKey(@"Software\RegisteredApplications", true);
                if (registeredKey != null)
                {
                    registeredKey.SetValue(target.Name, softwarePath);
                }

                // Prog ID 등록
                var associationsKey = softwareKey.CreateSubKey("FileAssociations");
                foreach (var prog in target.Identifiers)
                {
                    var extName = prog.Type.Extension.Replace(".", "").ToUpper();
                    var keyID = $"{target.Name}.AssocFile.{extName}";

                    // Prog ID 생성
                    ProgIDUtility.CreateProgID(keyID, prog);

                    // Prog ID 연결
                    associationsKey.SetValue(prog.Type.Extension, keyID);
                }

                NativeMethods.SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
                return true;
            }

            return false;
        }

        public static bool Unregister(string name, string companyName)
        {
            var companyPath = $@"Software\{companyName}";
            var softwarePath = $@"{companyPath}\{name}";
            var sofrwareKey = Registry.LocalMachine.OpenSubKey(softwarePath);
            if (sofrwareKey != null)
            {
                // Prog ID 삭제
                var assocKey = sofrwareKey.OpenSubKey(@"Capabilities\FileAssociations");
                foreach (var extension in assocKey.GetValueNames())
                {
                    var progID = assocKey.GetValue(extension).ToString();
                    ProgIDUtility.DeleteProgID(progID, extension);
                }

                // Software 삭제
                Registry.LocalMachine.DeleteSubKeyTree(softwarePath);
                Registry.LocalMachine.OpenSubKey(@"Software\RegisteredApplications", true).DeleteValue(name);

                // Comapny 삭제
                if (Registry.LocalMachine.OpenSubKey(companyPath).SubKeyCount <= 0)
                {
                    Registry.LocalMachine.DeleteSubKeyTree(companyPath);
                }

                NativeMethods.SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
                return true;
            }

            return false;
        }
    }
}
