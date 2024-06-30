using System;

namespace FractionApp
{
    public class Fraction
    {
        private int _numerator;
        private int _denominator;

        // Constructor with no parameters, initializes to 1/1
        public Fraction()
        {
            _numerator = 1;
            _denominator = 1;
        }

        // Constructor with one parameter, initializes to numerator/1
        public Fraction(int numerator)
        {
            _numerator = numerator;
            _denominator = 1;
        }

        // Constructor with two parameters, initializes to numerator/denominator
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero");
            }

            _numerator = numerator;
            _denominator = denominator;
        }

        // Getters and Setters
        public int Numerator
        {
            get { return _numerator; }
            set { _numerator = value; }
        }

        public int Denominator
        {
            get { return _denominator; }
            set 
            {
                if (value == 0)
                {
                    throw new ArgumentException("Denominator cannot be zero");
                }
                _denominator = value; 
            }
        }

        // Method to get fraction string
        public string GetFractionString()
        {
            return $"{_numerator}/{_denominator}";
        }

        // Method to get decimal value
        public double GetDecimalValue()
        {
            return (double)_numerator / _denominator;
        }
    }
}
