using SBook.logic.DB;
using SBook.logic.models;
using SBook.logic.prepPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.makeWord
{
    public class DictionaryUpdater
    {
        //TODO: Должен получить текст, обработать его и добавить в словарь то что недостает.

        JsonRepository Repository;
        int count = 1;
        string path = "";
        List<string> words = new List<string>();
        Dictionary<string, int> dict;
        string text;


        public DictionaryUpdater(string text)
        {
            this.text = text;
            this.Repository = new JsonRepository();
            this.dict = new WordsList(text).GetWords();
        }

        // Добавить в репозиторий полный список из скачанного контента, если слова нет
        // 0 - не скачано, 1 - добавлено, 2 - уже есть
        public async Task Update()
        {
            var lst = Repository.GetAll.ToList();

            foreach (var item in dict)
            {
                if (!Contain(item.Key, lst))
                {
                    Logger.Add("[" + count + "]\n");
                    await AddWord(item);
                    words.Add(item.Key);
                    count++;
                }
                else
                {
                    SBook.logic.models.Logger.Add("*" + item.Key + " - not Added! * *  [" + count + "]\n");
                    count++;
                }
            }
        }

        private async Task AddWord(KeyValuePair<string, int> item)
        {
            var wc = new WordCreation(item.Key);
            var words = wc.GetWords();
            foreach (var word in words)
            {
                Repository.Add(word);
                await GetMp3(word, this.Repository.Path);
            }
        }
        private async Task GetMp3(Word word, string path)
        {
            WebClient client = new WebClient();
            if (word.AudioUK != "")
            {
                string link1 = this.path + word.AudioUKpath;
                await client.DownloadFileTaskAsync(new Uri(link1), path + @"uk\" + word.AudioUK);
            }
            if (word.AudioUS != "")
            {
                string link2 = this.path + word.AudioUSpath;
                await client.DownloadFileTaskAsync(new Uri(link2), path + @"us\" + word.AudioUS);
            }
            client.Dispose();
        }

        private bool Contain(string name, List<Word> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].findName == name) return true;
            }

            return false;
        }
    }
}
