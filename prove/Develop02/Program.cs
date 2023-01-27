/* Exceeding Requirements:
    - Boxit static class to simplify formatting and display of text
    - Added more valid menu option keys. The first letter of the menu line works.
    - Used CSV files
    - Loaded prompts from a file
    - Kept track of which prompts have been used.  An improvement would be to include that information in the journal or prompt file
    -
*/

using System;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;
        string userInput = "";
        Journal journal = new Journal("Brian");

        //Console.OutputEncoding = System.Text.Encoding.Unicode;

        do
        {
            PrintMenu();
            Console.Write("(1-5)> ");
            userInput = Console.ReadLine();
            //Handle menu
            switch(userInput.ToLower())
            {
                case "1":
                case"w":
                {   
                    journal.WriteEntry();
                    break;
                }
                case "2":
                case "d":  //Display
                {   
                    journal.DisplayEntry();
                    break; 
                }
                case "3":
                case "s": //Save
                {   
                    journal.Save();
                    break; 
                }
                case "4":
                case "l":  //Load
                {   
                    journal.Load();
                    break; 
                    }
                case "5":
                case "q":  //Quit
                {   
                    running = false;
                    break; 
                }
                default:    
                {   
                    Console.WriteLine("Invalid option");
                    break; 
                }
            }
        }
        while(running == true);

        Console.WriteLine("Normal Termination");
    }
    static void PrintMenu()
    {
        Console.WriteLine("");
        Console.WriteLine(Boxit.ShadowBox("1 Write Entry\n2 Display Journal\n3 Save Journal\n4 Load Journal\n5 Quit", title: "MENU"));
    }
}