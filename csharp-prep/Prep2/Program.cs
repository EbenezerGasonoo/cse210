using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string grade = ""; // This will hold the grade letter + sign

        // Determine the grade letter
        if (percent >= 90)
        {
            grade = "A";
        }
        else if (percent >= 80)
        {
            grade = "B";
        }
        else if (percent >= 70)
        {
            grade = "C";
        }
        else if (percent >= 60)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }

        // Determine the grade sign based on the last digit of percent
        if (percent >= 60 && percent <= 100 && percent != 70 && percent != 80 && percent != 90 && percent != 100)
        {
            int lastDigit = percent % 10;

            if (lastDigit >= 7)
            {
                grade += "+";
            }
            else if (lastDigit < 3)
            {
                grade += "-";
            }
            // No sign for digits 3 to 6
        }

        Console.WriteLine($"Your grade is: {grade}");

        // Provide additional feedback based on passing or failing
        if (percent >= 70)
        {
            Console.WriteLine("Well done! You passed!");
        }
        else
        {
            Console.WriteLine("Don't give up! Better luck next time!");
        }
    }
}
