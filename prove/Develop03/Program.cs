using System;

class Program
{
    static void Main(string[] args)
    {
        string referenceString;
        Reference reference;
        ScriptureText bom = new ScriptureText("bom");
        List<Verse> newTexts;
        Console.Clear();

        // bom.PrintBooks(); 

        do
        {
            Console.WriteLine("Which scripture from the Book of Mormon would you like to memorize (book name ch#:v#-v#)?");
            referenceString = Console.ReadLine();
            newTexts = bom.GetTexts(referenceString);
            reference = new Reference(newTexts);
            while(!reference.AllHidden())
            {
                Console.Clear();
                Console.WriteLine(reference.serialize());
                reference.HideNumber(2);
                Console.WriteLine("--NEXT--\n");
                Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(reference.serialize());
            
            Console.WriteLine("Memorization complete! Beep Boop! Do it again(y/n)? ");
        } while(Console.ReadLine().ToLower() == "y");
    }
}