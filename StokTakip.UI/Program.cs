using System;
using System.Windows.Forms;

namespace StokTakip.UI
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

            // Başlangıç formunu LoginForm olarak ayarlıyoruz
            Application.Run(new LoginForm());
        }
    }
}