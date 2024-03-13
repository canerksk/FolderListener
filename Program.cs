using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderListener
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] processByName = Process.GetProcessesByName(processName);
            if (processByName.Length > 1)
            {
                MessageBox.Show("Uygulama zaten çalışıyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }

            Application.Run(new Form1());



        }
    }
}
