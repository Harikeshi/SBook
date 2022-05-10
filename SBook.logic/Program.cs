// See https://aka.ms/new-console-template for more information
using SBook.logic.DB;
using SBook.logic.makeWord;
using SBook.logic.models;
using SBook.logic.prepPdf;

RawText txt = new RawText(@"D:\temp\english\sharp.pdf");

DictionaryUpdater du = new DictionaryUpdater(txt.Content);

await du.Update();

Console.WriteLine();
