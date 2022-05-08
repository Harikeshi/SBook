using IronPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.prepPdf
{
    public class PdfHandler
    {
        private PdfDocument document;

        public PdfHandler(string path)
        {
            try
            {
                this.document = new PdfDocument(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string GetText()
        {
            if (document == null)
            {
                return String.Empty;
            }
            else return document.ExtractAllText();

        }
    }
}
