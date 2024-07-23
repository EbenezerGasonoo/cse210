using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

abstract class MindfulnessActivity
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int Duration { get; protected set; }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {Name}...");
        Console.WriteLine(Description);
        Console.Write("Enter the duration of the activity in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowAnimation(3);
    }

    public void EndActivity()
    {
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed the {Name} activity for {Duration} seconds.");
        ShowAnimation(3);
        LogActivity();
    }

    public void ShowAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("\r|");
            Thread.Sleep(250);
            Console.Write("\r/");
            Thread.Sleep(250);
            Console.Write("\r-");
            Thread.Sleep(250);
            Console.Write("\r\\");
            Thread.Sleep(250);
        }
        Console.WriteLine();
    }

    private void LogActivity()
    {
        string logEntry = $"{DateTime.Now}: {Name} for {Duration} seconds.";
        File.AppendAllText("activity_log.txt", logEntry + Environment.NewLine);
    }

    public abstract void PerformActivity();
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void PerformActivity()
    {
        StartActivity();
        int halfDuration = Duration / 2;
        for (int i = 0; i < halfDuration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowAnimation(2);
            Console.WriteLine("Breathe out...");
            ShowAnimation(2);
        }
        EndActivity();
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> _prompts = new()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> _questions = new()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection Activity";
        Description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public override void PerformActivity()
    {
        StartActivity();
        Random rnd = new Random();
        string prompt = _prompts[rnd.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        ShowAnimation(3);

        int elapsedTime = 0;
        while (elapsedTime < Duration)
        {
            string question = _questions[rnd.Next(_questions.Count)];
            Console.WriteLine(question);
            ShowAnimation(5);
            elapsedTime += 5;
        }
        EndActivity();
    }
}

class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> _prompts = new()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        Name = "Listing Activity";
        Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void PerformActivity()
    {
        StartActivity();
        Random rnd = new Random();
        string prompt = _prompts[rnd.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        ShowAnimation(3);

        List<string> items = new List<string>();
        int elapsedTime = 0;
        while (elapsedTime < Duration)
        {
            Console.Write("Enter an item: ");
            string item = Console.ReadLine();
            items.Add(item);
            ShowAnimation(2);
            elapsedTime += 2;
        }

        Console.WriteLine($"You listed {items.Count} items.");
        EndActivity();
    }
}

class Program
{
    static void Main(string[] args)
    {
        LoadActivityLog();

        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => throw new InvalidOperationException("Invalid choice. Try again.")
            };

            if (activity == null) break;

            activity.PerformActivity();
        }
    }

    static void LoadActivityLog()
    {
        if (File.Exists("activity_log.txt"))
        {
            string[] logEntries = File.ReadAllLines("activity_log.txt");
            Console.WriteLine("Activity Log:");
            foreach (string logEntry in logEntries)
            {
                Console.WriteLine(logEntry);
            }
            Console.WriteLine();
        }
    }
}