using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rating.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using NLog;

namespace rating.UI
{
    public partial class ExamControl : UserControl
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        List<Questions> Questions = new List<Questions>();
        List<QuestionAndAnswer> Answers = new List<QuestionAndAnswer>();
        int count;
        private int _ticks = 3600;
        readonly string UserResultPath;
        public ExamControl()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                backgroundWorker1.RunWorkerAsync();
                Cursor = Cursors.AppStarting;
                UserResultPath = Constants.ResultFolderPath + "/" + Constants.User.Email + ".json";
                
            }
        }

        private void ExamControl_Load(object sender, EventArgs e)
        {


        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Questions = Data.Questions.GetAllQuestions();

                if (File.Exists(UserResultPath))
                {
                    //var userAnswers = JsonConvert.DeserializeObject<UserAnswers>(File.ReadAllText(UserResultPath));

                    Answers = JsonConvert.DeserializeObject<UserAnswers>(File.ReadAllText(UserResultPath)).QuestionAndAnswers;
                    //  _ticks = Constants.User.TimeLeft;
                }
                else
                {
                    foreach (var item in Questions)
                    {
                        Answers.Add(new QuestionAndAnswer() { Question = item.Question, Answer = null });

                    }

                    // _ticks = 3600;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("An Error Occured");
            }

            //count = Properties.Settings.Default.position;

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Display(count);
            Cursor = Cursors.Arrow;
            DisplayButtons(count);

            timer1.Start();


        }

        /// <summary>
        /// display questions
        /// </summary>
        /// <param name="i">index of question</param>
        private void Display(int i)
        {
            var currQuestion = Questions[i];
            var currAnswer = Answers[i];

            lblQuestion.Text = currQuestion.Question;
            radA.Text = currQuestion.A;
            radB.Text = currQuestion.B;
            radC.Text = currQuestion.C;
            radD.Text = currQuestion.D;
            lblCount.Text = (i + 1).ToString() + " out of " + Questions.Count();

            SelectRad(currAnswer.Answer);
        }



        private void BtnPrev_Click(object sender, EventArgs e)
        {
            Answers[count].Answer = GetRadValue();
            Answers[count].CorrectAnswer = Questions[count].Correct;

            DisplayButtons(count - 1);
            if (count > 0)
            {
                count--;
                Display(count);
            }
        }
        private void BtnNext_Click(object sender, EventArgs e)
        {


            Answers[count].Answer = GetRadValue();
            Answers[count].CorrectAnswer = Questions[count].Correct;
            DisplayButtons(count + 1);

            if (count < Questions.Count && count != Questions.Count - 1)
            {
                count++;
                Display(count);
            }
        }

        void DisplayButtons(int count)
        {
            if (count == 0)
            {
                btnPrev.Visible = false;
                btnNext.Visible = true;
                btnSubmit.Visible = false;
            }
            else if (count > 0 && count < Questions.Count)
            {
                btnPrev.Visible = true;
                btnNext.Visible = true;
                btnSubmit.Visible = false;
            }
            else
            {
                btnPrev.Visible = true;
                btnNext.Visible = false;
                btnSubmit.Visible = true;

            }
        }

        private async void BtnSubmit_Click(object sender, EventArgs e)
        {
           await SubmitAnswersAsync();



            //Properties.Settings.Default.position = count;
            //Properties.Settings.Default.Save();
        }

        private async Task SubmitAnswersAsync()
        {
            try
            {

                var savedAnswers = new UserAnswers { User = Constants.User, QuestionAndAnswers = Answers };
                var str = JsonConvert.SerializeObject(savedAnswers);

                var users = User.GetAllUsers();

                var curruser = users.Find(x => x.Email == Constants.User.Email && x.Password == Constants.User.Password);

                curruser.isDone = true;
                var strUsers = JsonConvert.SerializeObject(users);

                File.WriteAllText(Constants.userPath, strUsers);

                File.WriteAllText(UserResultPath, str);

                //using (var client = new HttpClient())
                //{
                //    var response = await client.PostAsync("https://cbt.insyt.com.ng/api/UserAnswers", new StringContent(str, Encoding.UTF8, "application/json"));
                    
                //    if (response.IsSuccessStatusCode)
                //    {
                //        //log information
                //    }
                //}

                MessageBox.Show("The exam has ended. Thank You.");

                Dispose();
            }

            catch (Exception)
            {

                throw;
            }




        }

        private void SelectRad(string answer)
        {
            switch (answer)
            {
                case "A":
                    radA.Checked = true;
                    break;
                case "B":
                    radB.Checked = true;
                    break;
                case "C":
                    radC.Checked = true;
                    break;
                case "D":
                    radD.Checked = true;
                    break;
                default:
                    radA.Checked = false;
                    radB.Checked = false;
                    radC.Checked = false;
                    radD.Checked = false;
                    break;
            }
        }


        private string GetRadValue()
        {
            string rtn;
            if (radD.Checked) rtn = "D";
            else if (radB.Checked) rtn = "B";
            else if (radC.Checked) rtn = "C";
            else if (radA.Checked) rtn = "A";
            else rtn = null;

            return rtn;
        }

        private async void Timer1_Tick(object sender, EventArgs e)
        {
            if (_ticks < 1 )
            {
                timer1.Stop();


                //Implement submit here
               await SubmitAnswersAsync();
                //return;
            }
            _ticks--;
            lblTimer.Text = _ticks / 60 + ":" + ((_ticks % 60) >= 10 ? (_ticks % 60).ToString() : "0" + _ticks % 60);

        }
    }

}
