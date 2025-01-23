using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class HelpTableGenerator
    {
        private readonly FaceProbabilityOfWinCalculator calculator;
        private readonly List<Dice> listOfDice;

        public HelpTableGenerator(FaceProbabilityOfWinCalculator calculator, List<Dice> listOfDice)
        {
            this.calculator = calculator;
            this.listOfDice = listOfDice;
        }
        public void Display()
        {
            int cellWidth = 12;
            int totalWidth = cellWidth * (listOfDice.Count + 1) - 1;
            string divider = "|" + new string('-', totalWidth) + "|";

            Console.WriteLine("Probability of the win fоr the user:\n");
            Console.WriteLine($"|{"User dice v"}|{string.Join("|", listOfDice.Select(d => $"{string.Join(",", d.sides)}"))}|");
            Console.WriteLine(divider);

            for (int i = 0; i < listOfDice.Count; i++)
            {
                Console.Write($"|{string.Join(",", listOfDice[i].sides)}" + "|");

                for (int j = 0; j < listOfDice.Count; j++)
                {
                    var probability = calculator.Probability(listOfDice[i], listOfDice[j]);

                    if (probability.ToString().StartsWith("0,33"))
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = probability >= 0.55 ? ConsoleColor.Green : ConsoleColor.Red;

                    Console.Write($"{probability.ToString("F3")}".PadLeft(8) + "".PadLeft(cellWidth / 3 - 1));
                    Console.ResetColor();
                    Console.Write("|");
                }
                Console.WriteLine();
                Console.WriteLine(divider);
            }
        }
    }
}