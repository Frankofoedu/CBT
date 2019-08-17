using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace questionCollector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            var m = Constants.folderPath;
            foreach (var item in Controls.OfType<TextBox>())
            {
                if (string.IsNullOrWhiteSpace(item.Text.Trim()))
                {
                    MessageBox.Show("Please fill all textbox");
                    return;
                }
            }

            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select correct answer");
                return;
            }
            var t = comboBox1;
            var quest = new Questions
            {
                Question = txtQuestion.Text.Trim(),
                A = txtOptionA.Text.Trim(),
                B = txtOptionB.Text.Trim(),
                C = txtOptionC.Text.Trim(),
                D = txtOptionD.Text.Trim(),
                Correct = comboBox1.SelectedItem.ToString()
            };

            AddQuestion(quest);
        }

        void AddQuestion(Questions question)
        {
           var path = Constants.questionPath;

            var questions = new List<Questions>();

            Directory.CreateDirectory(Constants.folderPath);
            if (File.Exists(Constants.questionPath))
            {
                var user_file = File.ReadAllText(Constants.questionPath);
                var ques = JsonConvert.DeserializeObject<List<Questions>>(user_file);

                if (ques != null)
                {
                    questions = ques;
                }
            }
            else
            {
                File.Create(Constants.questionPath).Dispose();
            }
            // System.Security.AccessControl.FileSystemSecurity r;


          //  File.SetAttributes(Constants.questionPath, FileAttributes.Normal);

            questions.Add(question);

            File.WriteAllText(Constants.questionPath, JsonConvert.SerializeObject(questions));


            MessageBox.Show("Added");
            //  return jsonData;
        }
    }
}
