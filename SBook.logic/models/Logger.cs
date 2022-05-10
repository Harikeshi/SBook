using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.models
{
    static class Logger
    {
        static public void Add(string message)
        {
            try
            {
                Console.Write("[" + DateTime.Now + "]: " + message);
                File.AppendAllText(@"D:\temp\english\EnglishDictionary\" + "log.txt", "[" + DateTime.Now + "]: " + message);
            }
            catch
            {
                Directory.CreateDirectory(@"D:\temp\english\EnglishDictionary");
            }
        }
    }
}
