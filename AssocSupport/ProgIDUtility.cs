using AssocSupport.Models;
using Microsoft.Win32;

namespace AssocSupport
{
    static class ProgIDUtility
    {
        #region 내부 함수
        private static void CreateType(string id, FileType type)
        {
            var extensionKey = Registry.ClassesRoot.OpenSubKey(type.Extension, true);
            if (extensionKey == null)
            {
                var typeKey = Registry.ClassesRoot.CreateSubKey(type.Extension);
                typeKey.SetValue(RegistryKeys.Default, id);
                typeKey.SetValue(RegistryKeys.ContentType, type.ContentType);
                typeKey.SetValue(RegistryKeys.PerceivedType, type.PerceivedType.ToString().ToLower());
            }
            else
            {
                var openWithKey = extensionKey.OpenSubKey("OpenWithProgids", true);
                if (openWithKey != null)
                {
                    openWithKey.SetValue(id, new byte[0], RegistryValueKind.None);
                }
            }
        }

        private static void DeleteType(string id, string extension)
        {
            var extensionKey = Registry.ClassesRoot.OpenSubKey(extension, true);
            if (extensionKey.SubKeyCount > 0)
            {
                var openWithKey = extensionKey.OpenSubKey("OpenWithProgids", true);
                if (openWithKey.GetValue(id, null) != null)
                {
                    openWithKey.DeleteValue(id);
                }
            }
            else
            {
                Registry.ClassesRoot.DeleteSubKeyTree(extension);
            }
        }

        private static void CreateCommand(RegistryKey parentKey, ShellCommand command)
        {
            var shellKey = parentKey.CreateSubKey(RegistryKeys.Shell);
            var actionKey = shellKey.CreateSubKey(command.Action ?? "open");
            var cmdKey = actionKey.CreateSubKey(RegistryKeys.Command);

            var cmdStr = $"\"{command.Path}\" \"{command.Argument}\"";
            cmdKey.SetValue(RegistryKeys.Default, cmdStr);
        }
        #endregion

        #region 사용자 함수
        public static void CreateProgID(string id, ProgrammaticID prog)
        {
            // Prog ID 생성
            var progKey = Registry.ClassesRoot.CreateSubKey(id);
            progKey.SetValue(RegistryKeys.Default, prog.Description);
            progKey.SetValue(RegistryKeys.InfoTip, prog.InfoTip ?? prog.Description);

            var iconKey = progKey.CreateSubKey(RegistryKeys.Icon);
            iconKey.SetValue(RegistryKeys.Default, prog.Icon);

            // Prog ID 커멘드 생성
            CreateCommand(progKey, prog.Command);

            // File Type 생성
            CreateType(id, prog.Type);
        }

        public static void DeleteProgID(string id, string extension)
        {
            // File Type 삭제
            DeleteType(id, extension);

            // Prog ID 삭제
            var progKey = Registry.ClassesRoot.OpenSubKey(id);
            if (progKey != null)
            {
                Registry.ClassesRoot.DeleteSubKeyTree(id);
            }
        }
        #endregion
    }
}
