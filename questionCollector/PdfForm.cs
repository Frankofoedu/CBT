using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.IO;

namespace questionCollector
{
    public partial class PdfForm : Form
    {
        public PdfForm()
        {
            InitializeComponent();
        }

        private void PdfForm_Load(object sender, EventArgs e)
        {
            ListFieldNames();
            FillForm();
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// List all of the form fields into a textbox. The
        /// form fields identified can be used to map each of the
        /// fields in a PDF.
        /// <span class="code-SummaryComment"></summary></span>
        private void ListFieldNames()
        {
            string pdfTemplate = @"fill.pdf";
            // title the form

            this.Text += " - " + pdfTemplate;
            // create a new PDF reader based on the PDF template document

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            // create and populate a string builder with each of the
            // field names available in the subject PDF

            StringBuilder sb = new StringBuilder();

            foreach (var de in pdfReader.AcroFields.Fields)
            {
                sb.Append(de.Key.ToString() + Environment.NewLine);
            }
            // Write the string builder's content to the form's textbox

            textBox1.Text = sb.ToString();
            textBox1.SelectionStart = 0;
        }

        private void FillForm()
        {
            string pdfTemplate = @"fill2.pdf";
            string newFile = @"done.pdf";

            var pdfReader = new PdfReader(pdfTemplate);
            var pdfStamper = new PdfStamper(pdfReader, new FileStream(
                newFile, FileMode.Create));
            var pdfFormFields = pdfStamper.AcroFields;

            var fontpath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "fonts/TypographerRotunda.ttf");
            var fontCustom = BaseFont.CreateFont(fontpath, BaseFont.CP1252, BaseFont.EMBEDDED);
            var font = new iTextSharp.text.Font(fontCustom, 12f);


            // pdfFormFields.SetFieldProperty("name", "textsize", 16f, null);
           // var s = FontFactory.GetFont(FontFactory.COURIER_BOLD, 2f, iTextSharp.text.Font.BOLD);
            //pdfFormFields.AddSubstitutionFont(s.BaseFont);
            pdfFormFields.SetFieldProperty("name", "textfont", font.BaseFont, null);
            pdfFormFields.SetField("name", "NDIGWE CHINENYE PRECIOUS");
            


            pdfStamper.FormFlattening = true;
            
            // close the pdf

            pdfStamper.Close();
            //throw new NotImplementedException();
        }
    }
}
