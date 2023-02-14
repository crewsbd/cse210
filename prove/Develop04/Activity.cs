public class Activity
{
    private string _name;
    private string _description;

    public Activity()
    {

    }
    protected string Name
    {
        get => _name;
        set => _name = value;
    }
    protected string Description
    {
        get => _description;
        set => _description = value;
    }
    protected void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {Name}.\n\n{Description}\n");
        Console.WriteLine("How long, in seconds, would you like for your session?");
    }

    protected void Pause(int duration, string icon = "O")
    {
        duration *= 10000000; //10,000,000 in a second

        long oldTime = DateTime.Now.Ticks;
        long newTime = DateTime.Now.Ticks;
        int deltaTime = (int)(newTime - oldTime);

        //Console.WriteLine($"{deltaTime}");


        int frameCount = 0;
        int frameIndex = 0;

        Boolean continueLoop = true;

        string[] frames = { $"{icon}--------",$"-{icon}-------",$"--{icon}------",$"---{icon}-----",$"----{icon}----",
        $"-----{icon}---", $"------{icon}--", $"-------{icon}-", $"--------{icon}",
        $"-------{icon}-", $"------{icon}--", $"-----{icon}---", $"----{icon}----", $"---{icon}-----", $"--{icon}------",$"-{icon}-------"};
        int frameDelay = 10000000/frames.Count();
        //frameDelay = 1000000;

        do
        {
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
            }
        } while (continueLoop);
    }
}