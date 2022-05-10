using SBook.logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.makeWord
{
    public class WordCreation
    {
        // Класс для создания экземпляра слова

        List<Word> words; // На одной странице бывает несколько слов
        string name;
        HtmlDownloader hd;

        public WordCreation(string name)
        {
            this.hd = new HtmlDownloader(name);
            this.words = new List<Word>();
            this.name = name;
            this.CreateWords();
        }

        public List<Word> GetWords() => this.words;
        private void CreateWords()
        {
            string html = this.hd.GetRusHtml();
            var nodes = new WordNodesGetter(html).GetNodes();

            if (nodes.Count == 0)
            {
                SBook.logic.models.Logger.Add("** Missing English word abstract [" + name + "].\n");
                html = this.hd.GetEngHtml();
                nodes = new WordNodesGetter(html).GetNodes();

            }
            

            if (nodes.Count != 0)
            {
                SBook.logic.models.Logger.Add("[" + this.name + "] - Added!\n");

                for (int i = 0; i < nodes.Count; i++)
                {
                    var w = new Word(this.name);
                    w = new HtmlNodeHelper(nodes[i].InnerHtml).CreateWord(this.name);
                    this.words.Add(w);
                }
            }
            else
            {
                SBook.logic.models.Logger.Add("*** The Word is missing [" + name + "].\n");
            }
        }
    }
}
