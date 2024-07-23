using System;
using System.Collections.Generic;

// Comment class responsible for tracking the name and text of the comment.
public class Comment
{
    public string Name { get; private set; }
    public string Text { get; private set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Video class responsible for tracking the title, author, length, and list of comments.
public class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Length { get; private set; } // Length in seconds
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return new List<Comment>(comments); // Return a copy to prevent external modification
    }
}

public class Program
{
    public static void Main()
    {
        // Create videos
        Video video1 = new Video("How to cook Banku", "Kofi Amapong", 300);
        Video video2 = new Video("Introduction to Algorithms", "Akosua Assisi", 600);
        Video video3 = new Video("Data Structures", "Godfred Yeboah", 450);

        // Add comments to videos
        video1.AddComment(new Comment("User1", "Great tutorial!"));
        video1.AddComment(new Comment("User2", "Very helpful, thanks!"));
        video1.AddComment(new Comment("User3", "I learned a lot."));

        video2.AddComment(new Comment("User4", "Awesome explanation."));
        video2.AddComment(new Comment("User5", "Well done!"));

        video3.AddComment(new Comment("User6", "This was very clear."));
        video3.AddComment(new Comment("User7", "I appreciate the examples."));

        // Add videos to a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video details and comments
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}
