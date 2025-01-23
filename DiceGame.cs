using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class DiceGame
    {
        private readonly List<Dice> listOfDice1, listOfDice2;
        private readonly FairSecuredRandomGenerator random;
        private readonly HelpTableGenerator helpTable;
        private readonly FairPlayGenerator fair;
        
        public DiceGame(List<Dice> listOfDice1, List<Dice> listOfDIce2, FairSecuredRandomGenerator random, HelpTableGenerator helpTable, FairPlayGenerator fair)
        {
            this.listOfDice1 = listOfDice1;
            this.random = random;
            this.helpTable = helpTable;
            this.fair = fair;
            this.listOfDice2 = listOfDIce2;
        }
        public void PlayTurn(FairPlayGenerator fair, Dice computerDice, Dice userDice, Random rnd, Action displayHelp)
        {
            Console.WriteLine($"It's time for my throw");
            Console.WriteLine($"I selected a random number in the range 0 to 5");

            int computerNumberMod = fair.SecGenFairNumber(6, out var secretKey2, out var hmac2);
            Console.WriteLine($"Computer's HMAC: {hmac2}");

            int userNumberM = 0;
            while (true)
            {
                Console.WriteLine($"Add your number for modulo 6:");
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine($"[{i}]: {i}");
                }
                Console.WriteLine("X - Exit");
                Console.WriteLine("? - Help");

                string userNumberMod = Console.ReadLine();
                if (userNumberMod?.ToUpper() == "X")
                {
                    Console.WriteLine("Exit...");
                    Environment.Exit(0);
                }

                if (userNumberMod?.ToUpper() == "?")
                {
                    displayHelp();
                    continue;
                }

                if (int.TryParse(userNumberMod, out userNumberM) && userNumberM >= 0 && userNumberM <= 5)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }

            Console.WriteLine($"You chose number: {userNumberM}");
            Console.WriteLine($"My number is: {computerNumberMod}");
            Console.WriteLine($"Secret key: {BitConverter.ToString(secretKey2).Replace("-", "").ToUpper()}");


            int mod = (userNumberM + computerNumberMod) % 6;
            Console.WriteLine($"The result is: {userNumberM} + {computerNumberMod} = {mod} (Modulo 6)");

            int pcThrow = computerDice.sides[rnd.Next(computerDice.sides.Length)];
            Console.WriteLine($"My throw is: {pcThrow}");

            Console.WriteLine($"It's time for your throw");
            Console.WriteLine($"I selected a random number in the range 0 to 5");

            computerNumberMod = fair.SecGenFairNumber(6, out secretKey2, out hmac2);
            Console.WriteLine($"Computer's HMAC: {hmac2}");

            userNumberM = 0;
            while (true)
            {
                Console.WriteLine($"Add your number for modulo 6:");
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine($"[{i}]: {i}");
                }
                Console.WriteLine("X - Exit");
                Console.WriteLine("? - Help");
                string userNumberMod = Console.ReadLine();
                if (userNumberMod?.ToUpper() == "X")
                {
                    Console.WriteLine("Exit...");
                    Environment.Exit(0);
                }
                if (userNumberMod?.ToUpper() == "?")
                {
                    displayHelp();
                    continue;
                }
                if (int.TryParse(userNumberMod, out userNumberM) && userNumberM >= 0 && userNumberM <= 5)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
            Console.WriteLine($"You chose number: {userNumberM}");
            Console.WriteLine($"My number is: {computerNumberMod}");
            Console.WriteLine($"Secret key: {BitConverter.ToString(secretKey2).Replace("-", "").ToUpper()}");

            mod = (userNumberM + computerNumberMod) % 6;
            Console.WriteLine($"The result is: {userNumberM} + {computerNumberMod} = {mod} (Modulo 6)");

            int usThrow = userDice.sides[rnd.Next(userDice.sides.Length)];
            Console.WriteLine($"Your throw is: {usThrow}");

            if (usThrow > pcThrow)
            {
                Console.WriteLine($"You win! ({usThrow} > {pcThrow})");
            }
            else
            {
                Console.WriteLine($"You lose! ({pcThrow} > {usThrow})");
            }
        }
        public void Start()
        {
            var computerNumber = 0;
            var computerNumberMod = 0;
            int usThrow, pcThrow;
            Dice pcDice;
            Dice usDice;
            var UsDice = 0;
            int userNumber, userDiceIndex, computerDiceIndex, userNumberM, mod;
            Random rnd = new Random();

            Console.WriteLine("Welcome to the Dice Game!");
            Console.WriteLine("I selected a random number between 0..1");
            computerNumber = fair.GenerateFairNumber(2, out var secretKey, out var hmac);
            Console.WriteLine($"Computer's HMAC: {hmac}");
            Console.WriteLine("Try to guess my selection");
            while (true)
            { //Menu           
                Console.WriteLine("Select a number between 0..1 ");
                Console.WriteLine("X - Exit");
                Console.WriteLine("? - Help");
                string input = Console.ReadLine();
                if (input?.ToUpper() == "X" || input?.ToUpper() == "x")
                {
                    Console.WriteLine("Exit...");
                    Environment.Exit(0);
                }

                if (int.TryParse(input, out userNumber) && userNumber >= 0 && userNumber <= 1)
                {
                    break;
                }

                if (input?.ToUpper() == "?")
                {
                    helpTable.Display();
                }
            }
            Console.WriteLine($"My number is: {computerNumber}");
            Console.WriteLine($"Secret key: {BitConverter.ToString(secretKey).Replace("-", "").ToUpper()}");
            if (userNumber == computerNumber)
            {
                Console.WriteLine("You guessed right, you are first to choose!");
                while (true)
                {
                    Console.WriteLine("Choose an option ");
                    for (int i = 0; i < listOfDice1.Count; i++)
                    {
                        Console.WriteLine($"[{i}]: {string.Join(",", listOfDice1[i].sides)}");
                    }
                    Console.WriteLine("X - Exit");
                    Console.WriteLine("? - Help");

                    string input = Console.ReadLine();

                    if (input?.ToUpper() == "X" || input?.ToUpper() == "x")
                    {
                        Console.WriteLine("Exit...");
                        Environment.Exit(0);
                    }

                    if (input?.ToUpper() == "?")
                    {
                        helpTable.Display();
                    }

                    if (!int.TryParse(input, out userDiceIndex))
                    {
                        Console.WriteLine("You must type an integer number");
                        continue;
                    }

                    if (userDiceIndex < 0 || userDiceIndex >= listOfDice1.Count)
                    {
                        Console.WriteLine($"The dice {userDiceIndex} doesn't exist in the list");
                        continue;
                    }

                    break;
                }
                usDice = listOfDice1[userDiceIndex];
                listOfDice1.RemoveAt(userDiceIndex);
                Console.WriteLine($"You choose: {userDiceIndex}");
                Console.WriteLine($"Your dice: {string.Join(",", usDice.sides)}");

                computerDiceIndex = rnd.Next(listOfDice1.Count);
                pcDice = listOfDice1[computerDiceIndex];
                listOfDice1.RemoveAt(computerDiceIndex);
                Console.WriteLine($"I choose the dice: {string.Join(",", pcDice.sides)}");
                PlayTurn(fair, pcDice, usDice, rnd, helpTable.Display);
            }
            else
            {
                computerDiceIndex = rnd.Next(listOfDice1.Count);
                pcDice = listOfDice1[computerDiceIndex];
                listOfDice1.RemoveAt(computerDiceIndex);
                Console.Write("I choose first, ");
                Console.WriteLine($"I choose the dice: {string.Join(",", pcDice.sides)}");

                Console.WriteLine("Your turn to choose");
                while (true)
                {
                    Console.WriteLine("Choose an option ");
                    for (int i = 0; i < listOfDice1.Count; i++)
                    {
                        Console.WriteLine($"[{i}]: {string.Join(",", listOfDice1[i].sides)}");
                    }
                    Console.WriteLine("X - Exit");
                    Console.WriteLine("? - Help");

                    string input = Console.ReadLine();
                    if (input?.ToUpper() == "X" || input?.ToUpper() == "x")
                    {
                        Console.WriteLine("Bye\nExit...");
                        Environment.Exit(0);
                    }
                    if (input?.ToUpper() == "?")
                    {
                        helpTable.Display();
                    }
                    if (!int.TryParse(input, out userDiceIndex))
                    {
                        Console.WriteLine("You must type an integer number");
                        continue;
                    }
                    if (userDiceIndex < 0 || userDiceIndex >= listOfDice1.Count)
                    {
                        Console.WriteLine($"The dice {userDiceIndex} doesn't exist in the list");
                        continue;
                    }
                    break;
                }
                usDice = listOfDice1[userDiceIndex];
                listOfDice1.RemoveAt(userDiceIndex);
                Console.WriteLine($"You choose: {userDiceIndex}");
                Console.WriteLine($"Your dice: {string.Join(",", usDice.sides)}");
                PlayTurn(fair, pcDice, usDice, rnd, helpTable.Display);
            }
        }
    }
}
