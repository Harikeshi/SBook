using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.models
{
    public class Example
    {
        public string Name { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string Level { get; set; }
        public string Helper { get; set; }
        public string Translate { get; set; }
        public List<string> Sentences;
        public int Type { get; set; }

        public Example()
        {
            this.Name = String.Empty;
            this.a = String.Empty;
            this.b = String.Empty;
            this.Level = String.Empty;
            this.Helper = String.Empty;
            this.Translate = String.Empty;
            this.Type = 0;
            this.Sentences = new List<string>();
        }
    }
}
