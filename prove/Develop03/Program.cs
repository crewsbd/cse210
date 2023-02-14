// Exceeding Requirements: Load scriptures from files. Parsed text using regular expressions.
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
        Boolean quitProgram = false;

        do
        {
            Console.WriteLine("Which scripture from the Book of Mormon would you like to memorize (book name ch#:v#-v#)?");
            referenceString = Console.ReadLine();
            newTexts = bom.GetTexts(referenceString);
            if (newTexts != null)
            {
                reference = new Reference(newTexts);
                Boolean quitReference = false;
                while (!quitReference)
                {
                    Console.Clear();
                    Console.WriteLine(reference.Serialize());
                    reference.HideNumber(2);
                    Console.WriteLine("--NEXT--(\"quit\" to end)\n");
                    if (Console.ReadLine().ToLower() == "quit")
                    {
                        quitReference = true;
                        quitProgram = true;
                    }
                    if (reference.AllHidden())
                    {
                        quitReference = true;
                    }
                }
                Console.Clear();
                Console.WriteLine(reference.Serialize());
            }
            else
            {
                Console.WriteLine("An error occured getting that verse. Try again.");
            }
            if (!quitProgram)
            {
                Console.WriteLine("Memorization complete! Beep Boop! Type \"quit\" to end? ");
                string response = Console.ReadLine();
                if (response.ToLower() == "quit")
                {
                    quitProgram = true;
                }
            }
        } while (!quitProgram);
    }
}