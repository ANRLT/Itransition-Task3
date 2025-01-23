using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace Task3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var diceParse = new DiceParser();
                var listOfDice1 = diceParse.Parse(args);
                var listOfDice2 = diceParse.Parse(args);

                var random = new FairSecuredRandomGenerator();
                var probabilityCalculator = new FaceProbabilityOfWinCalculator();
                var helpTable = new HelpTableGenerator(probabilityCalculator, listOfDice2);
                var fair = new FairPlayGenerator(random);
                
                var game = new DiceGame(listOfDice1, listOfDice2, random, helpTable, fair);
                game.Start();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine("Recommended input format: '2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3'");
                Console.WriteLine("Exit...");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine("Recommended input format: '2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3'");
                Console.WriteLine("Exit...");
            }
        }
    }
}