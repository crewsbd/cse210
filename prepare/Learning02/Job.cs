using System;

public class Job
{
    private string _company;
    private string _jobTitle;
    private int _startYear;
    private int _endYear;
    public Job(string company, string jobTitle, int startYear, int endYear)
    {
        _company = company;
        _jobTitle = jobTitle;
        _startYear = startYear;
        _endYear = endYear;
    }
    public Job()
    {
        _company = "";
        _jobTitle = "";
        _startYear = 0;
        _endYear = 0;
    }
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company})".PadRight(40) + $"{_startYear}-{_endYear}".PadLeft(10));
    }
    public string Company
    {
        get { return _company; } 
        set { _company = value; }
    }
    public string JobTitle
    {
        get => _jobTitle;
        set => _jobTitle = value;
    }
    public int StartYear
    {
        get => _startYear;
        set => _startYear = value;
    }
    public int EndYear
    {
        get => _endYear;
        set => _endYear = value;
    }
}