using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask for Name
        Console.Write("What is your  First Name?  ");
        string  firstName = Console.ReadLine();

        Console.Write("What is your Last Name?  ");
        string lastName = Console.ReadLine();

        Console.Write($"Your name is {lastName}, {firstName} {lastName}. ");

    }
}