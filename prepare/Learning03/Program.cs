using System;

namespace FractionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Using the default constructor (1/1)
            Fraction f1 = new Fraction();
            Console.WriteLine(f1.GetFractionString()); // Output: 1/1
            Console.WriteLine(f1.GetDecimalValue()); // Output: 1.0

            // Using the constructor with one parameter (5/1)
            Fraction f2 = new Fraction(5);
            Console.WriteLine(f2.GetFractionString()); // Output: 5/1
            Console.WriteLine(f2.GetDecimalValue()); // Output: 5.0

            // Using the constructor with two parameters (3/4)
            Fraction f3 = new Fraction(3, 4);
            Console.WriteLine(f3.GetFractionString()); // Output: 3/4
            Console.WriteLine(f3.GetDecimalValue()); // Output: 0.75

            // Using the constructor with two parameters (1/3)
            Fraction f4 = new Fraction(1, 3);
            Console.WriteLine(f4.GetFractionString()); // Output: 1/3
            Console.WriteLine(f4.GetDecimalValue()); // Output: ~0.333

            // Additional tests to verify getters and setters
            f4.Numerator = 2;
            f4.Denominator = 5;
            Console.WriteLine(f4.GetFractionString()); // Output: 2/5
            Console.WriteLine(f4.GetDecimalValue()); // Output: 0.4
        }
    }
}
