using System;
using System.Windows.Forms;

namespace AssocSupport.Sample
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                MessageBox.Show(arg, "확장자 연결", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
