using iTextSharp.text.pdf;
using Newtonsoft.Json;
using rating.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFCertificateGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private readonly HttpClient client = new HttpClient();

        private async void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var response = await client.GetAsync("http://cbt.insyt.com.ng/api/Users");//, new StringContent(str, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var jsonObject = await response.Content.ReadAsStringAsync();

                    var users = JsonConvert.DeserializeObject<List<User>>(jsonObject);
                    if (users.Count < 1)
                    {
                        MessageBox.Show("No users registered");
                        return;
                    }

                    File.WriteAllText(Constants.allUserPath, jsonObject);


                    MessageBox.Show("User data has been collected");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"Users.json"))
            {
                var user_file = File.ReadAllText(@"Users.json");

                var jsonData = JsonConvert.DeserializeObject<List<User>>(user_file);

                if (jsonData != null)
                {
                    var i = 0;
                    Cursor = Cursors.WaitCursor;
                    foreach (var user in jsonData)
                    {
                        try
                        {
                            FillForm(user);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    Cursor = Cursors.Default;
                    MessageBox.Show($"{i} certificates generated");
                    return;
                }

                MessageBox.Show("An error occured while parsing data. contact ebuka");
                return;
               // return jsonData;
            }
            MessageBox.Show("File does not exist");
           // return null;
        }


        private void FillForm(User user)
        {
            string pdfTemplate = @"fill3.pdf";

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Certificate");

            Directory.CreateDirectory(path);



            string newFile = Path.Combine( path, user.Email + ".pdf") ; 

            var pdfReader = new PdfReader(pdfTemplate);
            var pdfStamper = new PdfStamper(pdfReader, new FileStream(
                newFile, FileMode.Create));
            var pdfFormFields = pdfStamper.AcroFields;

            var fontpath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"fonts\TypographerRotunda.ttf");
            var fontCustom = BaseFont.CreateFont(fontpath, BaseFont.CP1252, BaseFont.EMBEDDED);
            var font = new iTextSharp.text.Font(fontCustom, 12f);


            // pdfFormFields.SetFieldProperty("name", "textsize", 16f, null);
            // var s = FontFactory.GetFont(FontFactory.COURIER_BOLD, 2f, iTextSharp.text.Font.BOLD);
            //pdfFormFields.AddSubstitutionFont(s.BaseFont);
            pdfFormFields.SetFieldProperty("name", "textfont", font.BaseFont, null);
            pdfFormFields.SetField("name", user.FullName.ToUpper());


            pdfFormFields.SetField("serialNo", user.Id.ToString("00000"));


            pdfStamper.FormFlattening = true;

            // close the pdf

            pdfStamper.Close();
            //throw new NotImplementedException();
        }
        private void FillForm(string FullName, string Email, string sNo)
        {
            string pdfTemplate = @"fill3.pdf";

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Certificate");
            Directory.CreateDirectory(path);



            string newFile = Path.Combine(path, Email + ".pdf");

            var pdfReader = new PdfReader(pdfTemplate);
            var pdfStamper = new PdfStamper(pdfReader, new FileStream(
                newFile, FileMode.Create));
            var pdfFormFields = pdfStamper.AcroFields;

            var fontpath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"fonts\TypographerRotunda.ttf");
            var fontCustom = BaseFont.CreateFont(fontpath, BaseFont.CP1252, BaseFont.EMBEDDED);
            var font = new iTextSharp.text.Font(fontCustom, 12f);


            // pdfFormFields.SetFieldProperty("name", "textsize", 16f, null);
            // var s = FontFactory.GetFont(FontFactory.COURIER_BOLD, 2f, iTextSharp.text.Font.BOLD);
            //pdfFormFields.AddSubstitutionFont(s.BaseFont);
            pdfFormFields.SetFieldProperty("name", "textfont", font.BaseFont, null);
            pdfFormFields.SetField("name", FullName);


            pdfFormFields.SetField("serialNo", sNo);


            pdfStamper.FormFlattening = true;

            // close the pdf

            pdfStamper.Close();
            //throw new NotImplementedException();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            FillForm(txtName.Text, txtEmail.Text, "001");
            Cursor = Cursors.Default;

            MessageBox.Show("Generated");
        }
    }
}
