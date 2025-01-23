using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class FairPlayGenerator
    {
        private readonly FairSecuredRandomGenerator random;

        public FairPlayGenerator(FairSecuredRandomGenerator random) => this.random = random;

        public int GenerateFairNumber(int range, out byte[] secretKey, out string hmac)
        {
            string numberYoString;
            secretKey = random.GenerateKey(32);
            var number = random.GenerateUniform(0, range);
            numberYoString = number.ToString();
            hmac = random.CalculateHMAC(secretKey, numberYoString);
            return number;
        }

        public int SecGenFairNumber(int range, out byte[] secretKey, out string hmac)
        {
            string numberToString;
            secretKey = random.GenerateKey(32);
            var number = random.GenerateUniform(0, range);
            numberToString = number.ToString();
            hmac = random.CalculateHMAC(secretKey, numberToString);
            return number;
        }
    }
}