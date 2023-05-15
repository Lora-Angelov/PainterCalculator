
using System;
using System.Collections.Generic;

namespace PainterCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Catalogue
            Dictionary<string, double> paintCatalogue = new Dictionary<string, double>()
            {
                {"white", 5.0},
                {"off-white", 4.0},
                {"red", 5.5},
                {"blue", 5.5},
                {"green", 5.5}
            };

            // Choose paint or add your own
            Console.WriteLine("Please choose a paint from the following catalogue:");
            foreach (KeyValuePair<string, double> kvp in paintCatalogue)
            {
                Console.WriteLine($"{kvp.Key}: £{kvp.Value:F2}");
            }
            Console.WriteLine("Or enter your own paint and price:");

            // Get new paint name and price 
            string paintChoice = Console.ReadLine().ToLower();
            double paintPrice = 0.0;
            if (paintCatalogue.ContainsKey(paintChoice))
            {
                paintPrice = paintCatalogue[paintChoice];
            }
            else
            {
                while (paintPrice <= 0)
                {
                    Console.WriteLine("Please enter the price of your custom paint:");
                    string input = Console.ReadLine();
                    if (double.TryParse(input, out paintPrice) && paintPrice > 0)
                    {
                        paintCatalogue.Add(paintChoice, paintPrice);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number.");
                    }
                }
            }





            // Ask for wall dimensions and calc total area
            double totalArea = 0;
            Console.WriteLine("How many walls would you like to paint?");
            int wallCount = 0;
            while (wallCount <= 0)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out wallCount) && wallCount > 0)
                {
                    for (int i = 1; i <= wallCount; i++)
                    {
                     Console.WriteLine($"Enter the height of wall {i} in meters:");
                     double height = 0;
                     while (height <= 0)
                    {
                     string heightInput = Console.ReadLine();
                     if (double.TryParse(heightInput, out height) && height > 0)
                    {
                     Console.WriteLine($"Enter the width of wall {i} in meters:");
                     double width = 0;
                     while (width <= 0)
                    {
                     string widthInput = Console.ReadLine();
                     if (double.TryParse(widthInput, out width) && width > 0)
                    {
                        totalArea += height * width;
                    }
                     else
                    {
                     Console.WriteLine("Invalid input. Please enter a positive number.");
                    }
                    }
                    }
                    else
                    {
                     Console.WriteLine("Invalid input. Please enter a positive number.");
                    }
                    }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                }
            } 



        // Ask for power outlet count and remove area
        Console.WriteLine("How many power outlets are there?");
            int outletCount = 0;
            while (outletCount <= 0)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out outletCount) && outletCount >= 0)
                {
                    double outletArea = outletCount * 0.0222 * 0.0222;
                    totalArea -= outletArea;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                }
            }

            // Ask user for obstacles count and remove area
            Console.WriteLine("\nHow many obstacles (doors and/or windows) are there?");
            string obstacleCountInput = Console.ReadLine();
            int obstacleCount;

            // Error handling for obstacle count
            while (!int.TryParse(obstacleCountInput, out obstacleCount))
            {
                Console.WriteLine("\nInvalid input. Please enter a whole number:");
                obstacleCountInput = Console.ReadLine();
            }

            double obstacleArea = 0;
            for (int i = 1; i <= obstacleCount; i++)
            {
                Console.WriteLine($"\nPlease enter the dimensions of obstacle {i} (height x width in meters):");
                Console.Write("Height: ");
                string obstacleHeightInput = Console.ReadLine();
                double obstacleHeight;

                // Error handling for obstacle height???
                while (!double.TryParse(obstacleHeightInput, out obstacleHeight))
                {
                    Console.WriteLine("\nInvalid input. Please enter a decimal number:");
                    obstacleHeightInput = Console.ReadLine();
                }

                Console.Write("Width: ");
                string obstacleWidthInput = Console.ReadLine();
                double obstacleWidth;

                // Error handling for obstacle width???
                while (!double.TryParse(obstacleWidthInput, out obstacleWidth))
                {
                    Console.WriteLine("\nInvalid input. Please enter a decimal number:");
                    obstacleWidthInput = Console.ReadLine();
                }

                obstacleArea += obstacleHeight * obstacleWidth;
            }

            totalArea -= obstacleArea;





            Console.Write("Enter number of coats of paint: ");
            int numCoats = int.Parse(Console.ReadLine());

            // Ask user to select paint can size
            Console.WriteLine("\nWhat size of paint cans would you like to purchase?");
            Console.WriteLine("0.5 litre");
            Console.WriteLine("1 litre");
            Console.WriteLine("3 litres");
            Console.WriteLine("5 litres");
            Console.WriteLine("10 litres");

            // Error handling for paint can size selection
            double paintCanSize = 0;
            string paintCanSizeInput = Console.ReadLine();

            while (paintCanSize == 0)
            {
                switch (paintCanSizeInput)
                {
                    case "0.5":
                    case "0.5 litre":
                        paintCanSize = 0.5;
                        break;

                    case "1":
                    case "1 litre":
                        paintCanSize = 1;
                        break;

                    case "3":
                    case "3 litres":
                        paintCanSize = 3;
                        break;

                    case "5":
                    case "5 litres":
                        paintCanSize = 5;
                        break;

                    case "10":
                    case "10 litres":
                        paintCanSize = 10;
                        break;

                    default:
                        Console.WriteLine("\nInvalid input. Please enter a valid paint can size:");
                        paintCanSizeInput = Console.ReadLine();
                        break;
                }
            }

            // Calc required paint and number of cans
            double paintRequired = totalArea / 2.5;
            int cansRequired = (int)Math.Ceiling(paintRequired / paintCanSize);

            // calc required litres of paint and cost
            double litresRequired = totalArea * numCoats / paintRequired;
            double paintCost = litresRequired * paintPrice;

            // Calculate cost
            double cost = cansRequired * paintPrice;

            // Output results
            Console.WriteLine($"Total area to be painted: {totalArea} square meters");
            Console.WriteLine($"Litres of paint required: {paintRequired:F2}");
            Console.WriteLine($"Cans of paint required: {cansRequired}");
            Console.WriteLine($"Total cost of paint: £{cost:F2}");

            // Check for a cheaper option
            double cheapestPrice = double.MaxValue;
            string cheapestPaint = "";
            foreach (KeyValuePair<string, double> kvp in paintCatalogue)
            {
                if (kvp.Value < cheapestPrice)
                {
                    cheapestPrice = kvp.Value;
                    cheapestPaint = kvp.Key;
                }
            }
            if (paintPrice < cheapestPrice)
            {
                Console.WriteLine($"This is the cheapest option. {cheapestPaint} is £{cheapestPrice - paintPrice:F2} cheaper.");
            }
            else
            {
                Console.WriteLine($"This is not the cheapest option. {cheapestPaint} is £{cheapestPrice - paintPrice:F2} cheaper per litre.");
                Console.WriteLine("Cheaper paint options:");
                foreach (KeyValuePair<string, double> kvp in paintCatalogue)
                {
                    if (kvp.Value < paintPrice)
                    {
                        Console.WriteLine($"{kvp.Key}: £{kvp.Value:F2}");
                    }
                }
            }
        }

    }
}













