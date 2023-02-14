public class BreathingActivity : Activity
{

    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override void RunActivity()
    {   int durationCounter = _duration;
        do
        {
            Console.Write("\nBreathe in... ");
            CountDown(5);
            durationCounter -= 5;
            Console.Write("\nBreathe out...");
            CountDown(5);
            durationCounter -= 5;
        } while (durationCounter > 0);
    }

    //RunActivity helpers.
    
}