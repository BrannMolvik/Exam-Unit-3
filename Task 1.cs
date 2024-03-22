namespace mathAssignment
{
    public class MathFunctionsHelper
    {
        private const string EnterNumber = "Enter a number to square:";
        private const string InvalidInput = "Invalid input. Please enter a valid number.";
        private const string ResultSquare = "Square of {0} is: {1:F3}";
        private const string EnterInches = "Enter a length in inches:";
        private const string ResultInchToMil = "{0} inches is equal to {1} millimeters.";
        private const string EnterRootNumber = "Enter a number to find its square root:";
        private const string ResultRoot = "Square root of {0} is: {1:F3}";
        private const string NotPositive = "Number must be a positive";

        public static double Square(double number)
        {
            return number * number;
        }

        public static double InchesToMillimeters(double inches)
        {
            return inches * 25.4;
        }

        public static double SquareRoot(double number)
        {
            if (number < 0)
            {
                throw new ArgumentException(NotPositive);
            }

            return Math.Round(Math.Sqrt(number), 3);
        }

        public static void Main(string[] args)
        {
            bool isValidInput = false;
            double input;

            Console.WriteLine(EnterNumber);
            while (!isValidInput)
            {
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    double result = Square(input);
                    Console.WriteLine(ResultSquare, input, result);
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine(InvalidInput);
                }
            }

            isValidInput = false;

            Console.WriteLine(EnterInches);
            while (!isValidInput)
            {
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    double resultInMillimeters = InchesToMillimeters(input);
                    Console.WriteLine(ResultInchToMil, input, resultInMillimeters);
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine(InvalidInput);
                }
            }

            isValidInput = false;

            Console.WriteLine(EnterRootNumber);
            while (!isValidInput)
            {
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    try
                    {
                        double resultRoot = SquareRoot(input);
                        Console.WriteLine(ResultRoot, input, resultRoot);
                        isValidInput = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine(InvalidInput);
                }
            }
        }
    }
}