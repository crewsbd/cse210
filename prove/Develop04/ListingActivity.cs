public class ListingActivity : Activity
{
    private StringPool _promptPool;
    public ListingActivity()
    {
        _name = "Listing Activity";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        _promptPool = new StringPool( new string[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        });
    }

    protected override void RunActivity() //Unique activity code
    {
        long oldTime;
        long newTime;

        int durationCounter = _duration;
        Console.Write($"List as many responses as you can to the following prompt:\n --- {_promptPool.GetRandom()} ---\nYou may begin in:\n");
        CountDown(5);
        do
        {
            oldTime = DateTime.Now.Ticks;
            Console.Write(">");
            string response = Console.ReadLine();
            newTime = DateTime.Now.Ticks;
            durationCounter -= (int)Math.Ceiling((float)((newTime - oldTime)/10000000));

        } while (durationCounter > 0);
    }

}