using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace questionCollector
{
    public static class Constants
    {

        public static string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Questions");
        public static string questionPath = Path.Combine(folderPath, "question.json");
        /// <summary>
        /// current logged in user
        /// </summary>
        //public static User User;
    }
}
