using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    private string _shortName;
    private string _description;
    protected int _points; // Changed to protected for access in derived classes

    public string ShortName { get { return _shortName; } }
    public string Description { get { return _description; } }
    public int Points { get { return _points; } }

    protected Goal(string name, string description, int points) // Changed constructor to protected
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
        }
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        return $"Simple Goal: {Description} - Points: {Points} - Completed: {(_isComplete ? "[X]" : "[ ]")}";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{ShortName}:{Description}:{Points}:{_isComplete}";
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        _points += 100; // Increase points by 100 each time the event is recorded
    }

    public override bool IsComplete()
    {
        return false; // Eternal goals are never complete
    }

    public override string GetDetailsString()
    {
        return $"Eternal Goal: {Description} - Points: {Points}";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{ShortName}:{Description}:{Points}";
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted = 0)
        : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++;
            if (_amountCompleted == _target)
            {
                _points += _bonus; // Gain bonus points upon reaching target
            }
        }
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        return $"Checklist Goal: {Description} - Points: {Points} - Completed {_amountCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{ShortName}:{Description}:{Points}:{_amountCompleted}:{_target}:{_bonus}";
    }
}

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level; // Track the user's level
    private const int PointsForLevelUp = 1000; // Points needed to level up

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1; // Starting level
    }

    public void Start()
    {
        LoadGoals();
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"Total Score: {_score}");
        Console.WriteLine($"Current Level: {_level}");
        ListGoalDetails();
    }

    public void ListGoalNames()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void ListGoalDetails()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void CreateGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        foreach (var goal in _goals)
        {
            if (goal.ShortName == goalName)
            {
                int initialScore = _score;
                goal.RecordEvent();
                if (goal.IsComplete() && _score == initialScore) // Only add points once
                {
                    _score += goal.Points;
                    CheckLevelUp(); // Check if user levels up
                }
            }
        }
    }

    private void CheckLevelUp()
    {
        if (_score >= _level * PointsForLevelUp)
        {
            _level++;
            Console.WriteLine($"Congratulations! You've leveled up to level {_level}!");
        }
    }

    public void SaveGoals()
    {
        using (StreamWriter sw = new StreamWriter("goals.txt"))
        {
            foreach (var goal in _goals)
            {
                sw.WriteLine(goal.GetStringRepresentation());
            }
            sw.WriteLine($"TotalPoints:{_score}");
            sw.WriteLine($"Level:{_level}"); // Save current level
        }
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt"))
        {
            return;
        }

        using (StreamReader sr = new StreamReader("goals.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var parts = line.Split(':');
                if (parts.Length < 2) continue; // Skip invalid lines

                try
                {
                    switch (parts[0])
                    {
                        case "SimpleGoal":
                            if (parts.Length == 5)
                            {
                                var goal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
                                _goals.Add(goal);
                            }
                            break;

                        case "EternalGoal":
                            if (parts.Length == 4)
                            {
                                var goal = new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                                _goals.Add(goal);
                            }
                            break;

                        case "ChecklistGoal":
                            if (parts.Length == 7)
                            {
                                var goal = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
                                _goals.Add(goal);
                            }
                            break;

                        case "TotalPoints":
                            if (parts.Length == 2)
                            {
                                _score = int.Parse(parts[1]);
                            }
                            break;

                        case "Level":
                            if (parts.Length == 2)
                            {
                                _level = int.Parse(parts[1]);
                            }
                            break;

                        default:
                            Console.WriteLine($"Unknown goal type: {parts[0]}");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error parsing line '{line}': {ex.Message}");
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Eternal Quest Menu");
            Console.WriteLine("1. Display Player Info");
            Console.WriteLine("2. Create New Goal");
            Console.WriteLine("3. Record an Event");
            Console.WriteLine("4. List Goals");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option (1-7): ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    goalManager.DisplayPlayerInfo();
                    break;

                case "2":
                    Console.WriteLine("Enter goal type (Simple, Eternal, Checklist):");
                    string type = Console.ReadLine().ToLower();

                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter goal description: ");
                    string description = Console.ReadLine();

                    Console.Write("Enter points for goal: ");
                    int points = int.Parse(Console.ReadLine());

                    if (type == "simple")
                    {
                        Console.Write("Is the goal completed? (true/false): ");
                        bool isComplete = bool.Parse(Console.ReadLine());
                        goalManager.CreateGoal(new SimpleGoal(name, description, points, isComplete));
                    }
                    else if (type == "eternal")
                    {
                        goalManager.CreateGoal(new EternalGoal(name, description, points));
                    }
                    else if (type == "checklist")
                    {
                        Console.Write("Enter target number of completions: ");
                        int target = int.Parse(Console.ReadLine());

                        Console.Write("Enter bonus points for completing all tasks: ");
                        int bonus = int.Parse(Console.ReadLine());

                        goalManager.CreateGoal(new ChecklistGoal(name, description, points, target, bonus));
                    }
                    else
                    {
                        Console.WriteLine("Invalid goal type.");
                    }
                    break;

                case "3":
                    Console.Write("Enter goal name to record event: ");
                    string goalName = Console.ReadLine();
                    goalManager.RecordEvent(goalName);
                    break;

                case "4":
                    goalManager.ListGoalDetails();
                    break;

                case "5":
                    goalManager.SaveGoals();
                    Console.WriteLine("Goals saved successfully.");
                    break;

                case "6":
                    goalManager.LoadGoals();
                    Console.WriteLine("Goals loaded successfully.");
                    break;

                case "7":
                    running = false;
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice, please select a valid option.");
                    break;
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
