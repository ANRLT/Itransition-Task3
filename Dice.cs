using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Dice
    {
        public int[] sides { get; }

        public Dice(int[] sides)
        {
            if (sides.Length != 6 || sides.Any(s => s <= 0))
            {
                throw new ArgumentException("The number of sides is different from 6 or negative");
            }
            this.sides = sides;
        }
    }
}
