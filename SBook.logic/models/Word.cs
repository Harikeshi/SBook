using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.models
{
    public class Word
    {
        // Класс для создания и хранения хранения слова 
        public string findName { get; set; }
        public string Name { get; set; }
        public string Part { get; set; }
        public string Pron { get; set; }
        public string AudioUKpath { get; set; }
        public string AudioUSpath { get; set; }
        public string AudioUK { get; set; }
        public string AudioUS { get; set; }
        public List<Example> Examples;
        public Dictionary<string, string> Irregular;
        public Word(string name)
        {
            this.findName = name;
            this.Name = String.Empty;
            this.Part = String.Empty;
            this.Pron = String.Empty;
            this.AudioUK = String.Empty;
            this.AudioUS = String.Empty;
            this.AudioUKpath = String.Empty;
            this.AudioUSpath = String.Empty;
            this.Examples = new List<Example>();
            this.Irregular = new Dictionary<string, string>();
        }
    }
}
