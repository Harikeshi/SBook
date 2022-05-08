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
        }
    }
}
