using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class DiceParser
    {
        public List<Dice> Parse(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("The number of dices must be more than 2");
            }
            var listOfDice = new List<Dice>();

            foreach (var arg in args)
            {
                var sides = arg.Split(',').Select(int.Parse).ToArray();
                listOfDice.Add(new Dice(sides));
            }
            return listOfDice;
        }
    }
}