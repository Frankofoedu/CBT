using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace rating.Data
{
    public static class Constants
    {
        public static readonly string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CBT");
        public static readonly string ResultFolderPath = Path.Combine(folderPath, "Result");
        public static readonly string userFolderPath = Path.Combine(folderPath, "Users");
        public static readonly string userPath = Path.Combine(userFolderPath, "users.json");
        public static readonly string QuestionPath = Path.Combine(Application.StartupPath, "Data\\questions.json");
        /// <summary>
        /// current logged in user
        /// </summary>
        public static User User;


        public static Dictionary<string, List<string>> ListCentres = new Dictionary<string, List<string>>()
        {
            { "AWKA ZONE", new List<string>{"ST JOHN OF GOD SECONDARY SCHOOL AWKA", "COMPREHENSIVE SECONDARY SCHOOL NAWFIA", "NNAMAKA SECONDARY SCHOOL IFITEDUNU", "GIRLS SECONDARY SCHOOL AWKA", "ADAZI-NNUKWU CBT CENTRE", "COMMUNITY EDUCATION RESOURCE CENTRE OKPUNO" } },
            {"OGIDI ZONE", new List<string>{ "CAVE CITY SECONDARY SCHOOL OGBUNIKE", "NEW ERA SECONDARY SCHOOL NTEJE", "MATER AMABILIS SECONDARY SCHOOL UMUOJI", "GIRLS HIGH SCHOOL ALOR", "URBAN SECONDARY SCHOOL NKPOR", "OUR LADIES SECONDARY SCHOOL NNOBI" } },
            {"NNEWI ZONE", new List<string>{ "NNEWI HIGH SCHOOL NNEWI", "ANGLICAN GIRLS SECONDARY SCHOOL NNEWI", "COMMUNITY SECONDARY SCHOOL AMICHI", "ABBOT GIRLS SECONDARY SCHOOL IHIALA", "OKONGWU MEMORIAL GRAMMAR SCHOOL NNEWI", "ST AUGUSTINE SECONDARY SCHOOL MBOSI", "CAROL STANDARD SECONDARY SCHOOL ICHI" } },
            {"AGUATA ZONE", new List<string>{ "COMMUNITY SECONDARY SCHOOL OKO", "URBAN GIRLS SECONDARY SCHOOL EKWULOBIA", "COMMUNITY SECONDARY SCHOOL ISUOFIA", "GOVERNMENT TECHNICAL COLLEGE UMUNZE" } },
            {"ONITSHA ZONE", new List<string>{ "DENIS MEMORIAL GRAMMAR SCHOOL ONITSHA", "CHRIST THE KING COLLEGE ONITSHA", "EASTERN ACADEMY ONITSHA", "QUEEN OF THE ROSARY COLLEGE ONITSHA", "COMMUNITY SECONDARY SCHOOL IYIOWA-ODEKPE" } },
            {"OTUOCHA ZONE", new List<string>{ "STELLA MARIS COLLEGE UMUERI", "FATHER JOSEPH SECONDARY SCHOOL AGULERI", "COMMUNITY SECONDARY SCHOOL OMOR" } }
        };
    }
}
