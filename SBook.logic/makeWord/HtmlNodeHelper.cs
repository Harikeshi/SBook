using HtmlAgilityPack;
using SBook.logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.makeWord
{
    public class HtmlNodeHelper
    {
        // Имеем строку Html и по ней нам надо получить поля         
        public string Html { get; }
        public HtmlNodeCollection Spans { get; }
        public HtmlNodeCollection Divs { get; }
        public HtmlDocument Document { get; }

        public HtmlNodeHelper(string html)
        {
            this.Html = html;
            this.Document = new HtmlDocument();
            this.Document.LoadHtml(Html);

            this.Spans = Document.DocumentNode.SelectNodes("//span");
            this.Divs = Document.DocumentNode.SelectNodes("//div");
        }

        public Word? CreateWord(string name)
        {
            Word word = new Word(name);
            word.Name = this.GetName();
            word.Part = this.GetPart();
            word.Pron = this.GetPron();
            word.Irregular = this.GetIrregular();
            word.Examples = this.GetExamples();
            this.SetAudio(ref word);

            return word;
        }
        private void SetAudio(ref Word word)
        {
            List<Audio>? lst = this.GetAudios();

            try
            {
                word.AudioUK = lst[0].Name;
                word.AudioUKpath = lst[0].Path;
                try
                {
                    word.AudioUS = lst[1].Name;
                    word.AudioUSpath = lst[1].Path;
                }
                catch
                {
                    Console.WriteLine("Audio US missed.");
                }
            }
            catch
            {
                Console.WriteLine("Audio UK missed.");
            }
        }

        private string GetName() => this.GetString(this.Spans, "hw dhw");
        private string GetPart() => this.GetString(this.Spans, "pos dpos");
        private string GetPron() => this.GetString(this.Spans, "ipa dipa lpr-2 lpl-1");
        private Dictionary<string, string> GetIrregular() => this.GetIrr(this.Spans, "irreg-infls dinfls ");
        private List<Audio>? GetAudios()
        {
            // first is the UK, second is the US
            List<HtmlNode>? audios = GetAudioNodes();
            if (audios != null)
            {
                List<Audio> a = new List<Audio>();
                foreach (var audio in audios)
                {
                    string raw = audio.Attributes["src"].Value;
                    a.Add(new Audio
                    {
                        Path = raw,
                        Name = GetAudioName(raw)
                    });
                }
                return a;
            }
            else return null;
        }
        private List<Example> GetExamples()
        {
            List<Example> lst = new List<Example>();
            var nodes = CreateExampleNodes("pr dsense ");
            if (nodes != null)
            {
                CreateExamplesOfType(ref lst, nodes, 1);
            }

            nodes = CreateExampleNodes("pr dsense dsense-noh");
            if (nodes != null)
            {
                CreateExamplesOfType(ref lst, nodes, 2);
            }
            return lst;
        }

        // Name = "hw dhw"
        // Part = "pos dpos"
        // Pron = "ipa dipa lpr-2 lpl-1"
        private string GetString(HtmlNodeCollection collection, string attribute)
        {
            HtmlNode? check = GetNode(this.Spans, attribute);
            if (check != null)
                return check.InnerText;
            else
            {
                return String.Empty;
            }
        }
        private HtmlNode? GetNode(HtmlNodeCollection collection, string attribute)
        {
            return collection.FirstOrDefault(n => n.Attributes["class"] != null && n.Attributes["class"].Value == attribute);
        }
        private static string ClearString(string str)
        {
            string t = String.Empty;

            int i = 0;
            while ((str[i] == ' ' || str[i] == '\n' || str[i] == '\t' || str[i] == '\r'))
            {
                i++;
                if (i >= str.Length) return t;
            }

            int max = str.Length - 1;
            while (str[max] == ' ' || str[max] == '\n' || str[max] == '\r' || str[max] == '\t')
            {
                max--;
            }

            for (; i < max; i++)
            {
                if (str[i] == '\n' || str[i] == '\t' || str[i] == '\r')
                {
                }
                else if (str[i] == ' ' && (str[i + 1] == ' ' || str[i + 1] == '.' || str[i + 1] == '!' || str[i + 1] == '?')) { }
                else t += str[i];
            }
            t += str[max];
            return t;
        }
        private List<HtmlNode>? GetNodes(HtmlNode node, string attribute)
        {
            return node.SelectNodes("//span").Where(n => n.Attributes["class"] != null && n.Attributes["class"].Value == attribute).ToList();
        }
        private List<HtmlNode>? GetAudioNodes()
        {
            return this.Document.DocumentNode.SelectNodes("//source")
                    .Where(n => n.Attributes["type"].Value == "audio/mpeg").ToList();
        }
        private static string GetAudioName(string str)
        {
            string s = String.Empty;
            int i = str.Length - 1;
            while (str[i] != '/')
            {
                i--;
            }
            for (++i; i < str.Length; i++)
            {
                s += str[i];
            }
            return s;
        }

        // 1 - "pr dsense "
        // 2 - "pr dsense dsense-noh"
        private IEnumerable<HtmlNode>? CreateExampleNodes(string attribute)
        {
            return Divs.Where(n => n.Attributes["class"] != null && n.Attributes["class"].Value == attribute);
        }
        private void CreateExamplesOfType(ref List<Example> list, IEnumerable<HtmlNode> nodes, int type)
        {

            foreach (var node in nodes)
            {
                var hd = new HtmlDocument();
                hd.LoadHtml(node.InnerHtml);

                Example ex = new Example();
                var b = hd.DocumentNode.SelectSingleNode("//div");
                var spans = b.SelectNodes("//span");

                HtmlNode? check;
                check = GetNode(spans, "hw dsense_hw");
                if (check != null) ex.Name = check.InnerText;

                check = GetNode(spans, "pos dsense_pos");
                if (check != null) ex.a = check.InnerText;

                check = GetNode(spans, "guideword dsense_gw");
                if (check != null) ex.b = ClearString(check.InnerText);

                check = GetNode(spans, "def-info ddef-info");
                if (check != null) ex.Level = ClearString(check.InnerText);

                check = GetNode(spans, "def ddef_d db");
                if (check != null) ex.Helper = check.InnerText;

                check = GetNode(spans, "trans dtrans dtrans-se ");
                if (check != null) ex.Translate = check.InnerText;

                var sents = GetNodes(b, "eg deg");

                // Предложения примеры
                if (sents != null)
                    foreach (var s in sents)
                    {
                        string sen = s.InnerText;
                        ex.Sentences.Add(sen);
                    }
                ex.Type = type;

                list.Add(ex);
            }
        }
        private Dictionary<string, string> GetIrr(HtmlNodeCollection collection, string attribute)
        {
            //Irregular = "irreg-infls dinfls "
            Dictionary<string, string> irregular = new Dictionary<string, string>();
            HtmlNode? check = GetNode(this.Spans, attribute);
            if (check != null)
            {
                //"inf-group dinfg "
                var pairs = GetNodes(check, "inf-group dinfg ");
                if (pairs != null)
                    foreach (var item in pairs)
                    {
                        if (item.ChildNodes != null)
                        {
                            string k = "", v = "";
                            for (int i = 0; i < item.ChildNodes.Count; i++)
                            {
                                if (item.ChildNodes[i].Name == "span") k += item.ChildNodes[i].InnerText + " ";

                                if (item.ChildNodes[i].Name == "b") v = item.ChildNodes[i].InnerText;
                            }
                            // Проверка на добавление новых ключей в словарь
                            // Если совпадает можно сохранить в виде ключ + " " или сделать новый класс 
                            if (irregular.ContainsKey(k) == false)
                            {
                                irregular.Add(k, v);
                            }
                        }
                    }
            }
            return irregular;
        }

    }
}
