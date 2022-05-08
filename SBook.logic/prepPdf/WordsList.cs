using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.prepPdf
{
    public class WordsList
    {
        private Dictionary<string, int> Words;
        public WordsList(string path)
        {
            this.Words = new WordsListCreator(path).GetWords();
        }

        public Dictionary<string, int> GetWords() => this.Words;

    }
}
