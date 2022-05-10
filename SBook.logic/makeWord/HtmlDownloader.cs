using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.makeWord
{
    public class HtmlDownloader
    {

        // TODO: Нужна проверка скачалось то что надо или нет.

        string pathRus = "";
        string pathEng = "";

        public string Word { get; set; }

        public HtmlDownloader(string word)
        {
            this.Word = word;
        }
        private string GetHtml(string path)
        {
            return HtmlDownloader.Load(path).Text;
        }

        public string GetRusHtml() => GetHtml(this.pathRus + this.Word);
        public string GetEngHtml() => GetHtml(this.pathEng + this.Word);


        private static HtmlDocument Load(string path)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(path);
        }
    }
}
