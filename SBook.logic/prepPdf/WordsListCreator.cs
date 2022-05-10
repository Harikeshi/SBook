using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.prepPdf
{
    public class WordsListCreator
    {
        private Dictionary<string, int> words;
        string raw;

        char[] separaters = { '\n', '\t', '\r', ',', ' ', ':', '\"', ';', '.', '!', '?' };

        public WordsListCreator(string txt)
        {
            this.raw = txt;
            this.words = new Dictionary<string, int>();

            this.CreateDictionary();
        }

        private void AddWord(string word)
        {
            if (!words.ContainsKey(word))
                words.Add(word, 0);
        }

        private void CreateDictionary()
        {
            for (int i = 0; i < raw.Length; i++)
            {
                if (IsLetter(raw[i]))
                {
                    string word = String.Empty;
                    //пока не разделитель или не конец текста
                    while (!IsEnd(raw[i], separaters))
                    {
                        word += raw[i];
                        i++;
                    }

                    // Слово должно иметь больше одной буквы и содержать только буквы.
                    if (IsWord(word) == false) { continue; }

                    // Проверка есть ли в слове ' или -                        
                    // Если таких символов нет, то проверяем и записываем в словарь.
                    word = word.ToLower();

                    int check = WithMinus(word);
                    // В слове есть -
                    if (check == -1)
                    {
                        string temp = String.Empty;
                        // Проходим до -, записываем слово, пока не конец слова
                        for (int l = 0; l < word.Length; l++)
                        {
                            if (word[l] == '-')
                            {
                                AddWord(temp);//Count
                                temp = String.Empty;
                            }
                            else if (l == word.Length - 1)
                            {
                                temp += word[l];
                                AddWord(temp);
                            }
                            else { temp += word[l]; }
                        }
                    }
                    // В слове есть '
                    else if (check == 1)
                    {
                        string temp = String.Empty;
                        int h = 0;
                        while (word[h] != '\'')
                        {
                            temp += word[h];
                            h++;
                        }
                        AddWord(temp);
                    }
                    // Если слово без - и '
                    else
                    {
                        AddWord(word);
                    }
                }
            }
            words.Remove("");
        }

        private bool IsLetter(char ch)
        {
            if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch == '-' || ch == '\'')
                return true;
            return false;
        }

        private bool IsEnd(char ch, char[] chrs)
        {
            for (int i = 0; i < chrs.Length; i++)
            {
                if (ch == chrs[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsWord(string word)
        {
            if (word.Length == 1) { return false; }

            for (int j = 0; j < word.Length; j++)
            {
                if (!IsLetter(word[j]))
                {
                    return false;
                }
            }
            return true;
        }

        private int WithMinus(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == '-') return -1;
                if (word[i] == '\'') return 1;
            }
            return 0;
        }

        public Dictionary<string, int> GetWords() => this.words;

    }
}
