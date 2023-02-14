public class BreathingActivity : Activity
{

    public BreathingActivity()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }
    public void Launch()
    {
        Boolean continueLoop = true;
        DisplayStartingMessage();
        string durationString = Console.ReadLine();
        int duration = int.Parse(durationString);
       
        Pause(5);
        do
        {
            Console.Write("Breathe in...");
            CountDown(5);
            duration -= 5;
            if(duration <= 0)
            {
                continueLoop = false;
            }
        } while (continueLoop);


    }
    private void CountDown(int seconds)
    {
        do
        {
            string message = $"{seconds}";
            //Console.Write($"{message} ");
            Pause(1, message);
            //Console.Write("".PadLeft(message.Length-1, '\b'));
            seconds -= 1;

        } while (seconds >= 0);
    }
}