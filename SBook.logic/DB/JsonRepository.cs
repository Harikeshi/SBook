using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBook.logic.DB;
using SBook.logic.models;

namespace SBook.logic.DB
{
    public class JsonRepository : IRepository<Word>
    {
        // TODO: На данный момент мне надо только добавлять.
        private readonly WordsContent content;
        public string Path { get; }

        public JsonRepository()
        {
            this.content = new WordsContent();           
            this.Path = content.Path;
        }


        public IEnumerable<Word> GetAll => content.Words;


        public void Add(Word word)
        {
            content.Words.Add(word);
            content.Save();
        }


        public void Delete(Word entity)
        {
            throw new NotImplementedException();
        }

        public Word FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Word entity)
        {
            throw new NotImplementedException();
        }
    }
}
