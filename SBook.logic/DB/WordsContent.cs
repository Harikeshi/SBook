using SBook.logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.DB
{
    public class WordsContent : DBContent<Word>
    {
        public List<Word> Words { get; set; }

        public WordsContent() : base("EnglishDictionary")
        {
            this.Words = objects;// TODO: ?
            this.Path = GetPath() + base.Name + @"\";
        }
        public string Path { get; }

    }
}
