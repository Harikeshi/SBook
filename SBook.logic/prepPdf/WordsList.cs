using SBook.logic.makeWord;
using SBook.logic.models;
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

        public WordsList(string txt)
        {

            this.Words = new WordsListCreator(txt).GetWords();
            SBook.logic.models.Logger.Add("The list contains " + Words.Count + " words.\n");

        }

        public Dictionary<string, int> GetWords() => this.Words;
        public void LoadFromTxt(string path)
        {

        }

    }
}
