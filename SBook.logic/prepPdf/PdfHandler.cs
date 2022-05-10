using IronPdf;
using SBook.logic.models;
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
            Logger.Add("Start Loading.\n");
            try
            {
                this.document = new PdfDocument(path);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message);
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
