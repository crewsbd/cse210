public class ReflectingActivity : Activity
{
    private StringPool _messagePool;
    private StringPool _promptPool;


    public ReflectingActivity()
    {
        _name = "Reflecting Activity";
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        
        _messagePool = new StringPool( new string[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        }
        );
        _promptPool = new StringPool(
            new string[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        }
        );  
    }

    protected override void RunActivity() //Unique activity code
    {
        int durationCounter = _duration;
        Console.WriteLine($"Consider the following prompt:\n\n---{_messagePool.GetRandom()}---\n\nWhen you have something in mind, press enter to continue.");
        Console.ReadLine();
        Console.WriteLine("Now ponder on each of the following questions as they related to this experience.\nYou may begin in:");
        CountDown(5);
        do
        {
            Console.Write($">{_promptPool.GetRandom()}: ");
            CountDown(10);
            durationCounter -= 10;
            Console.WriteLine();

        } while (durationCounter > 0);

    }
}