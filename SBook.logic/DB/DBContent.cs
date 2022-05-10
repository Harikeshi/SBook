using Newtonsoft.Json;
using SBook.logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.DB
{
    public class DBContent<T> where T : class
    {
        string defaultPath = "";
        public string Name { get; set; }
        protected List<T> objects;

        public DBContent(string name)
        {
            this.Name = name;
            this.objects = new List<T>();
            this.Load();
        }

        protected string GetPath()
        {
            return defaultPath;
        }

        private void Load()
        {
            if (File.Exists(defaultPath + this.Name + @"\db.json"))
            {
                string txt = File.ReadAllText(defaultPath + this.Name + @"\db.json");
                this.objects = JsonConvert.DeserializeObject<List<T>>(txt);
            }
            else
            {
                Logger.Add("Directory is not exist! Created new Directory.\n");
                this.objects = new List<T>();
                CreateDirectories();
                string txt = JsonConvert.SerializeObject(this.objects);
                File.WriteAllText(defaultPath + this.Name + @"\db.json", txt);
            }
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(defaultPath + this.Name);
            Directory.CreateDirectory(defaultPath + this.Name + @"\uk");
            Directory.CreateDirectory(defaultPath + this.Name + @"\us");
        }

        protected void Add(T obj)
        {
            //TODO: дописывать в файл стринг объект, а не просто переписывать весь.
            if (objects.Contains(obj))
            {
                this.objects.Add(obj);
            }
            else { }
        }

        public async Task Save()
        {
            string json = JsonConvert.SerializeObject(this.objects);

            await File.WriteAllTextAsync(defaultPath + this.Name + @"\db.json", json);

        }
    }
}
