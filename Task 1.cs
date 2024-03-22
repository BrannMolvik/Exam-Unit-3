namespace Exam3
{
    public class MathFunctions
    {
        private const string EnterNumberSquare = "Enter a number to square:";
        private const string InvalidInput = "Invalid input. Please enter a valid number.";
        private const string ResultSquare = "Square of {0} is: {1:F3}";
        private const string EnterInches = "Enter a length in inches:";
        private const string ResultInchToMil = "{0} inches is equal to {1} millimeters.";
        private const string EnterRootNumber = "Enter a number to find its square root:";
        private const string ResultRoot = "Square root of {0} is: {1:F3}";
        private const string NotPositive = "Number must be a positive";
        private const string EnterNumberQube = "Enter a number to Qube:";
        private const string ResultQube = "Qube of {0} is: {1:F3}";
        private const string EnterRadius = "Enter the radius of circle:";
        private const string ResultCircleArea = "The area of circle with radius {0} is: {1:F3}";
        private const string EnterName = "Enter your name:";

        private static readonly Random random = new Random();

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

        public static double Qube(double number)
        {
            return number * number * number;
        }

        public static double CircleArea(double radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException(NotPositive);
            }

            return Math.PI * Math.Pow(radius, 2);
        }

        public static string RandomGreeting(string name)
        {
            string[] greetings = {
                $"Howdy {name}",
                $"Yo {name}",
                $"Hello {name}",
                $"Morning {name}",
                $"Hello {name}. I will fillet you like a salmon",
                $"Hah. Did you really think outsmart bullet {name}?",
                $"Hello {name}. Do you even know who you are talking to?",
                $"If I were a bad demoman. I would not have been sitting here discussing with {name}. Would I?",
                $"If fighting is sure to result in victory then {name} must fight!",
            };

            int index = random.Next(greetings.Length);
            return greetings[index];
        }

        public static void RunMathAndName()
        {
            bool isValidInput = false;
            double input;

            Console.WriteLine(EnterNumberSquare);
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

            isValidInput = false;

            Console.WriteLine(EnterNumberQube);
            while (!isValidInput)
            {
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    double result = Qube(input);
                    Console.WriteLine(ResultQube, input, result);
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine(InvalidInput);
                }
            }

            isValidInput = false;

            Console.WriteLine(EnterRadius);
            while (!isValidInput)
            {
                if (double.TryParse(Console.ReadLine(), out input))
                {
                    try
                    {
                        double result = CircleArea(input);
                        Console.WriteLine(ResultCircleArea, input, result);
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

            isValidInput = false;


            Console.WriteLine(EnterName);
            string name = Console.ReadLine();

            Console.WriteLine(RandomGreeting(name));
        }
    }
}