using System;
using System.Collections.Generic;

// Base class
public abstract class Activity
{
    // Private fields
    private DateTime _date;
    private int _durationInMinutes;

    // Constructor
    public Activity(DateTime date, int durationInMinutes)
    {
        _date = date;
        _durationInMinutes = durationInMinutes;
    }

    // Protected properties for derived classes
    protected DateTime Date => _date;
    protected int DurationInMinutes => _durationInMinutes;

    // Virtual methods
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Method to generate summary
    public string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} {GetType().Name} ({DurationInMinutes} min) - Distance {GetDistance()} miles, Speed {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

// Derived class for Running
public class Running : Activity
{
    private double _distance;

    public Running(DateTime date, int durationInMinutes, double distance)
        : base(date, durationInMinutes)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;
    public override double GetSpeed() => (GetDistance() / DurationInMinutes) * 60;
    public override double GetPace() => DurationInMinutes / GetDistance();
}

// Derived class for Cycling
public class Cycling : Activity
{
    private double _speed;

    public Cycling(DateTime date, int durationInMinutes, double speed)
        : base(date, durationInMinutes)
    {
        _speed = speed;
    }

    public override double GetDistance() => (GetSpeed() / 60) * DurationInMinutes;
    public override double GetSpeed() => _speed;
    public override double GetPace() => 60 / GetSpeed();
}

// Derived class for Swimming
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int durationInMinutes, int laps)
        : base(date, durationInMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0;
    public override double GetSpeed() => (GetDistance() / DurationInMinutes) * 60;
    public override double GetPace() => DurationInMinutes / GetDistance();
}

// Main program
class Program
{
    static void Main()
    {
        // Create activities
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 3), 45, 15.0),
            new Swimming(new DateTime(2022, 11, 3), 60, 40)
        };

        // Display summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
