using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.makeWord
{
    public class WordNodesGetter
    {
        readonly List<HtmlNode> nodes;
        public WordNodesGetter(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            this.nodes = htmlDocument.DocumentNode.SelectNodes("//div")
                    .Where(n => n.Attributes["class"] != null && n.Attributes["class"].Value == "pr entry-body__el").ToList();
        }

        public List<HtmlNode> GetNodes() => this.nodes;
    }
}
