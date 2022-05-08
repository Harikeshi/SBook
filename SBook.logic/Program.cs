// See https://aka.ms/new-console-template for more information
using SBook.logic.makeWord;
using SBook.logic.models;
using SBook.logic.prepPdf;

WordCreation wc = new WordCreation("time");


List<Word> lst = wc.GetWords();


foreach (var item in lst)
{
    Console.WriteLine(item.AudioUKpath);
}

var txt = new WordsList(@"D:\temp\english\pdf\qt.").GetWords();

foreach (var item in txt)
{
    Console.WriteLine(item.Key);
}

