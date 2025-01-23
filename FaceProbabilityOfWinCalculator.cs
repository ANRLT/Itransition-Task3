using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class FaceProbabilityOfWinCalculator
    {
        public double Probability(Dice diceA, Dice diceB)
        {
            int wins = 0;
            int diceCombinations = diceA.sides.Length * diceB.sides.Length;

            foreach (int a in diceA.sides)
            {
                foreach (int b in diceB.sides)
                {
                    if (a > b)
                    {
                        wins++;
                    }
                }
            }
            return (double)wins / diceCombinations;
        }
    }
}