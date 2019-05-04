using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screensaver
{
    static class Screensaver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                string tag = args[0].ToLower().Trim().Substring(0, 2);
                switch (tag)
                {
                    // show
                    case "/s":
                        break;

                    // preview
                    case "/p":
                        break;

                    // configure
                    case "/c":
                        MessageBox.Show("Sorry, configuration options are not available at this point.", 
                            "Snowfall Screensaver", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                        break;

                    default:
                        MessageBox.Show("Command line argument error: \"" + tag + "\" is not found", 
                            "Snowfall Screensaver", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Exclamation);
                        break;
                }
            }
            else
            {
                ShowScreens();
            }
        }

        static private void ShowScreens()
        {
            for (int i = Screen.AllScreens.GetLowerBound(0); i <= Screen.AllScreens.GetUpperBound(0); i++)
            {
                Application.Run(new MyScreen(i));
            }
        }
    }
}
