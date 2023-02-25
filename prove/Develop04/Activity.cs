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
    public virtual void Launch()
    {
        DisplayStartingMessage();
        _totalTime += _duration; //Set in DisplayStartingMessage
        //Console.Write("Test Pause 60 second: ");
        Pause(5);
        Console.Clear();
        RunActivity();
        DisplayEndingMessage();

    }
    protected abstract void RunActivity();
    protected void DisplayStartingMessage()
    {
        int seconds = _totalTime % 60;
        int minutes = _totalTime / 60 % 60;
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
        Console.WriteLine($"\nWell done!!\n\nYou have completed another {_duration} seconds of the {_name}.");
        Console.ReadLine();
    }

    protected void Pause(int duration, string icon = "*", Boolean cleanup = false)
    {
        duration *= (int)TimeSpan.TicksPerSecond;

        long oldTime = DateTime.Now.Ticks;
        long newTime = DateTime.Now.Ticks;
        int deltaTime = (int)(newTime - oldTime);

        int frameCount = 0;
        int frameIndex = 0;

        Boolean continueLoop = true;

        string[] frames = { $"{icon}==------",$"={icon}=------",$"=={icon}------",$"-=={icon}-----",$"--=={icon}----",
        $"---=={icon}---", $"----=={icon}--", $"-----=={icon}-", $"------=={icon}",
        $"------={icon}=", $"------{icon}==", $"-----{icon}==-", $"----{icon}==--", $"---{icon}==---", $"--{icon}==----",$"-{icon}==-----"};
        int frameDelay = (int)TimeSpan.TicksPerSecond / frames.Count(); //1 second/number of frames

        do
        {
            Thread.Sleep(frameDelay / (int)TimeSpan.TicksPerMillisecond / 2); //Sleep for half a frame.
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
            if (duration < 0)
            {
                continueLoop = false;
                if (cleanup)
                {
                    Console.Write("".PadLeft(frames[frameIndex].Length, ' '));
                    Console.Write("".PadLeft(frames[frameIndex].Length, '\b'));
                }

            }
        } while (continueLoop);
    }
    protected void CountDown(int seconds)
    {
        string timerString;
        while (seconds > 1)
        {
            timerString = $"{seconds}";
            Pause(1, timerString);
            seconds -= 1;

        }
        Pause(1, "0", true); //Prevents blinking countdown

    }
}
