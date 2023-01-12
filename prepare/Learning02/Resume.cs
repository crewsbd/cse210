using System;

class Resume
{
    private string _name;
    private List<Job> _jobs;
    public Resume(string name)
    {
        _name = name;
        _jobs = new List<Job>();
    }
    public void Display()
    {
        Console.WriteLine($"Name: {_name}\nJobs:");
        foreach(Job job in _jobs)
        {
            job.Display();
        }
    }
    public string Name {
        get => _name;
        set => _name = value;
    }
    public void AddJob(string company, string title, int startYear, int endYear)
    {
        _jobs.Add(new Job(company, title, startYear, endYear));
    }
    public void AddJob(Job newJob)
    {
        _jobs.Add(newJob);
    }
    public void RemoveJob(int index)
    {
        _jobs.RemoveAt(index);
    }
}