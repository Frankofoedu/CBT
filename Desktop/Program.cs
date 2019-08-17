using rating.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rating
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //create all directories
            Directory.CreateDirectory(Constants.folderPath);
            Directory.CreateDirectory(Constants.userFolderPath);
            Directory.CreateDirectory(Constants.ResultFolderPath);
            Application.Run(new StartForm());

        }
    }
}
