using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.models
{
    public class Audio
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public Audio()
        {
            this.Name = String.Empty;
            this.Path = String.Empty;
        }
    }
}
