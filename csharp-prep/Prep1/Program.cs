using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your  First Name?  ");
        string  FirstName = Console.ReadLine();

        Console.Write("What is your Last Name?  ");
        string LastName = Console.ReadLine();

        Console.Write($"Your Name is {LastName}, {FirstName} {LastName}. ");

    }
}