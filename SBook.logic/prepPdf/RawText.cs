using SBook.logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.prepPdf
{
    public class RawText
    {
        public string Content { get; }
        public RawText(string path)
        {
            this.Content = new PdfHandler(path).GetText();
            File.AppendAllText(@"D:\temp\english\EnglishDictionary\raw.txt", this.Content);
            Logger.Add("Created a file with a length of " + this.Content.Length + " letters.\n");
        }
    }
}
