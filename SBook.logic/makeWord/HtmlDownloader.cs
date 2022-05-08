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
        // Должен проверять есть ли слово в русском сегменте, если нет проверять в англоязычном и скачивать.

        // TODO: Нужна проверка скачалась то что надо или нет.

        string pathRus = "https://dictionary.cambridge.org/ru/%D1%81%D0%BB%D0%BE%D0%B2%D0%B0%D1%80%D1%8C/%D0%B0%D0%BD%D0%B3%D0%BB%D0%BE-%D1%80%D1%83%D1%81%D1%81%D0%BA%D0%B8%D0%B9/";
        string pathEng = "https://dictionary.cambridge.org/ru/%D1%81%D0%BB%D0%BE%D0%B2%D0%B0%D1%80%D1%8C/%D0%B0%D0%BD%D0%B3%D0%BB%D0%B8%D0%B9%D1%81%D0%BA%D0%B8%D0%B9/";

        public string Word { get; set; }

        public HtmlDownloader(string word)
        {
            this.Word = word;
        }
        private string GetHtml(string path) => HtmlDownloader.Load(path).Text;

        public string GetRusHtml() => GetHtml(this.pathRus + this.Word);
        public string GetEngHtml() => GetHtml(this.pathEng + this.Word);


        private static HtmlDocument Load(string path)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(path);
        }
    }
}
