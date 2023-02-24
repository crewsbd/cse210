public abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;
    protected int _totalTime;

    public Activity()
    {
        _name = "Generic Activity";
        _description = "This activity is not defined.";
        _duration = 0;
        _totalTime = 0;
    }
    public string Name
    {
        get => _name;
    }
    /*public string Description
    {
        get => _description;
    } */

    public virtual void Launch()
    {
        DisplayStartingMessage();
        _totalTime += _duration; //Set in DisplayStartingMessage
        Pause(5);
        Console.Clear();
        RunActivity();
        DisplayEndingMessage();

    }
    protected abstract void RunActivity();
    protected void DisplayStartingMessage()
    {
        int seconds = _totalTime % 60;
        int minutes = _totalTime / 60;
        int hours = _totalTime / 60 / 60;
        
        Console.WriteLine($"Welcome to the {_name}.\n\n{_description}\nYou have spent {(hours > 0 ? hours : "")}{(hours > 0 ? " hours, " : "")}{(minutes > 0 || hours > 0 ? minutes : "")}{(minutes > 0 || hours > 0 ? " minutes and " : "")}{seconds} seconds in this activity.");
        do
        {
            Console.WriteLine("How long, in seconds, would you like for your session?");
        } while (!(int.TryParse(Console.ReadLine(), out _duration)));
        Console.WriteLine("Get Ready...");
    }
    protected void DisplayEndingMessage()
    {
        Console.WriteLine($"Well done!!\n\nYou have completed another {_duration} seconds of the {_name}.");
        Console.ReadLine();
    }

    protected void Pause(int duration, string icon = "*")
    {
        duration *= 10000000; //10,000,000 in a second

        long oldTime = DateTime.Now.Ticks;
        long newTime = DateTime.Now.Ticks;
        int deltaTime = (int)(newTime - oldTime);

        int frameCount = 0;
        int frameIndex = 0;

        Boolean continueLoop = true;

        string[] frames = { $"{icon}==------",$"={icon}=------",$"=={icon}------",$"-=={icon}-----",$"--=={icon}----",
        $"---=={icon}---", $"----=={icon}--", $"-----=={icon}-", $"------=={icon}",
        $"------={icon}=", $"------{icon}==", $"-----{icon}==-", $"----{icon}==--", $"---{icon}==---", $"--{icon}==----",$"-{icon}==-----"};
        int frameDelay = 10000000 / frames.Count() + 6;

        do
        {
            Thread.Sleep(frameDelay/10000); //Give it some finer granularity.
            newTime = DateTime.Now.Ticks;
            deltaTime = (int)(newTime - oldTime);
            oldTime = newTime;

            frameCount += deltaTime;
            duration -= deltaTime;

            if (frameCount > frameDelay)
            {
                Console.Write(frames[frameIndex]);
                Console.Write("".PadLeft(frames[frameIndex].Length, '\b'));
                frameCount = frameCount - frameDelay;
                frameIndex += 1;
                if (frameIndex >= frames.Count())
                {
                    frameIndex = 0;
                }
            }
            if (duration <= 0)
            {
                continueLoop = false;
                Console.Write("".PadLeft(frames[frameIndex].Length, ' '));
                Console.Write("".PadLeft(frames[frameIndex].Length, '\b'));
            }
        } while (continueLoop);
    }
    protected void CountDown(int seconds)
    {
        do
        {
            string timerString = $"{seconds}";
            Pause(1, timerString);
            seconds -= 1;

        } while (seconds >= 0);
    }
}
