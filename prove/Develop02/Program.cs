using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JournalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayJournal();
                        break;
                    case "3":
                        journal.SaveJournal();
                        break;
                    case "4":
                        journal.LoadJournal();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.\n");
                        break;
                }
            }
        }
    }

    public class JournalEntry
    {
        public string Date { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }

        public JournalEntry(string date, string prompt, string response)
        {
            Date = date;
            Prompt = prompt;
            Response = response;
        }

        public override string ToString()
        {
            return $"{Date} - {Prompt}: {Response}";
        }
    }

    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();
        private List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        public void AddEntry()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            Console.Write($"Prompt: {prompt}\nResponse: ");
            string response = Console.ReadLine();
            JournalEntry entry = new JournalEntry(date, prompt, response);
            entries.Add(entry);
            Console.WriteLine("Entry added successfully!\n");
        }

        public void DisplayJournal()
        {
            if (!entries.Any())
            {
                Console.WriteLine("No entries to display.\n");
                return;
            }

            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine();
        }

        public void SaveJournal()
        {
            Console.Write("Enter the filename to save the journal: ");
            string filename = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{EscapeCsv(entry.Date)},{EscapeCsv(entry.Prompt)},{EscapeCsv(entry.Response)}");
                }
            }
            Console.WriteLine($"Journal saved to {filename}\n");
        }

        public void LoadJournal()
        {
            Console.Write("Enter the filename to load the journal: ");
            string filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File {filename} does not exist.\n");
                return;
            }
            using (StreamReader reader = new StreamReader(filename))
            {
                entries = new List<JournalEntry>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = SplitCsv(line);
                    if (parts.Length == 3)
                    {
                        string date = parts[0];
                        string prompt = parts[1];
                        string response = parts[2];
                        JournalEntry entry = new JournalEntry(date, prompt, response);
                        entries.Add(entry);
                    }
                }
            }
            Console.WriteLine($"Journal loaded from {filename}\n");
        }

        private string EscapeCsv(string value)
        {
            if (value.Contains(",") || value.Contains("\""))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }
            return value;
        }

        private string[] SplitCsv(string csvLine)
        {
            List<string> parts = new List<string>();
            bool inQuotes = false;
            string part = string.Empty;

            for (int i = 0; i < csvLine.Length; i++)
            {
                char c = csvLine[i];

                if (inQuotes)
                {
                    if (c == '"' && i + 1 < csvLine.Length && csvLine[i + 1] == '"')
                    {
                        part += '"';
                        i++;
                    }
                    else if (c == '"')
                    {
                        inQuotes = false;
                    }
                    else
                    {
                        part += c;
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else if (c == ',')
                    {
                        parts.Add(part);
                        part = string.Empty;
                    }
                    else
                    {
                        part += c;
                    }
                }
            }

            parts.Add(part);

            return parts.ToArray();
        }
    }
}
