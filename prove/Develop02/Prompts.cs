using System;

class Prompts
{
    private List<string> _prompts = new List<string>();
    private List<int> _usedPrompts = new List<int>();
    private Random _randomGenerator = new Random(DateTime.Now.Millisecond);

    public Prompts()
    {   //some default prompts
        _prompts = new List<string>();
        _prompts.Add("What's your favorite idea?");
        _prompts.Add("Where'd you get the idea?");
        _prompts.Add("How many licks does it take to get to the center of a tootsie pop?");
    }
    public Prompts(List<string> promptList)
    {
        _prompts = promptList;
    }
    public string GetPrompt()
    {
        int newIndex = 0;
        if(_prompts.Count <= _usedPrompts.Count)
        {
            ResetUsed();
        }
        do
        {
            newIndex = _randomGenerator.Next(_prompts.Count);
        } while(_usedPrompts.Contains(newIndex) );
        _usedPrompts.Add(newIndex);
        return _prompts[newIndex];
    }
    public void AddPrompt(string newPrompt)
    {
        _prompts.Add(newPrompt);
        return;
    }
    public void RemovePrompt(int promptIndex)
    {
        _prompts.RemoveAt(promptIndex);
        return;
    }
    public void ResetUsed()
    {
        _usedPrompts.Clear();
        return;
    }
}