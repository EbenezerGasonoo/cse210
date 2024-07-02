using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    private const string SaveFilePath = "progress.txt";

    static void Main(string[] args)
    {
        // Attempt to load previous progress
        Scripture scripture = LoadProgress();

        while (true)
        {
            // Clear the console and display the current state of the scripture
            Console.Clear();
            Console.WriteLine(scripture);

            // Prompt the user for input
            Console.WriteLine("\nPress Enter to hide a few words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                // Save progress and exit if the user types 'quit'
                SaveProgress(scripture);
                break;
            }

            // Hide a few random words in the scripture
            scripture.HideRandomWords();

            // Check if all words are hidden and end the program if they are
            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture);
                Console.WriteLine("\nAll words are hidden. Program will exit.");
                File.Delete(SaveFilePath);
                break;
            }

            // Save progress after each iteration
            SaveProgress(scripture);
        }
    }

    // Method to save the current state of the scripture to a file
    static void SaveProgress(Scripture scripture)
    {
        using (StreamWriter writer = new StreamWriter(SaveFilePath))
        {
            writer.WriteLine(scripture.Reference);
            foreach (var word in scripture.Words)
            {
                writer.WriteLine($"{word.Text}|{word.Hidden}");
            }
        }
    }

    // Method to load the previous state of the scripture from a file, if it exists
    static Scripture LoadProgress()
    {
        if (File.Exists(SaveFilePath))
        {
            using (StreamReader reader = new StreamReader(SaveFilePath))
            {
                string referenceLine = reader.ReadLine();
                Reference reference = Reference.Parse(referenceLine);

                List<Word> words = new List<Word>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    var word = new Word(parts[0]);
                    if (bool.Parse(parts[1]))
                    {
                        word.Hide();
                    }
                    words.Add(word);
                }

                return new Scripture(reference, words);
            }
        }
        else
        {
            return new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
        }
    }
}

class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    // Constructor for reference with optional end verse
    public Reference(string book, int chapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    // Parses a string into a Reference object
    public static Reference Parse(string reference)
    {
        var parts = reference.Split(' ');
        string book = parts[0];
        var chapterVerse = parts[1].Split(':');
        int chapter = int.Parse(chapterVerse[0]);
        var verses = chapterVerse[1].Split('-');
        int startVerse = int.Parse(verses[0]);
        int? endVerse = verses.Length > 1 ? int.Parse(verses[1]) : (int?)null;

        return new Reference(book, chapter, startVerse, endVerse);
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}" : $"{Book} {Chapter}:{StartVerse}";
    }
}

class Word
{
    public string Text { get; private set; }
    public bool Hidden { get; private set; }

    // Constructor for Word
    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }

    // Method to hide the word
    public void Hide()
    {
        Hidden = true;
    }

    // Method to set the hidden state (used during deserialization)
    public void SetHidden(bool hidden)
    {
        Hidden = hidden;
    }

    public override string ToString()
    {
        return Hidden ? "____" : Text;
    }
}

class Scripture
{
    public Reference Reference { get; private set; }
    public List<Word> Words { get; private set; }
    private Random _random = new Random();

    // Constructor for Scripture
    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    // Constructor for Scripture with existing words
    public Scripture(Reference reference, List<Word> words)
    {
        Reference = reference;
        Words = words;
    }

    // Method to hide a few random words
    public void HideRandomWords()
    {
        int wordsToHide = Math.Min(3, Words.Count(word => !word.Hidden));
        int hiddenCount = 0;

        while (hiddenCount < wordsToHide)
        {
            int index = _random.Next(Words.Count);
            if (!Words[index].Hidden)
            {
                Words[index].Hide();
                hiddenCount++;
            }
        }
    }

    // Method to check if all words are hidden
    public bool AllWordsHidden()
    {
        return Words.All(word => word.Hidden);
    }

    public override string ToString()
    {
        string scriptureText = string.Join(" ", Words);
        return $"{Reference}\n{scriptureText}";
    }
}
