using DZen.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    public class FairSecuredRandomGenerator
    {
        private readonly RandomNumberGenerator randomNumberGen = RandomNumberGenerator.Create();
        public int GenerateUniform(int min, int max)
        {
            var range = (long)max - min;
            var bytes = new byte[8];
            long randomValue;
            do
            {
                randomNumberGen.GetBytes(bytes);
                randomValue = BitConverter.ToInt64(bytes, 0) & long.MaxValue;
            } while (randomValue >= long.MaxValue - (long.MaxValue % range));

            return (int)(min + (randomValue % range));
        }

        public byte[] GenerateKey(int length)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[length];
                rng.GetBytes(key);
                using (var sha3 = SHA3.Create())
                {
                    return sha3.ComputeHash(key);
                }
            }
        }

        public string CalculateHMAC(byte[] key, string message)
        {
            var hmac = new HMac(new Sha3Digest(256));
            string keyHex = BitConverter.ToString(key).Replace("-", "").ToUpper();

            byte[] keyHexToByte = Encoding.UTF8.GetBytes(keyHex);

            hmac.Init(new KeyParameter(keyHexToByte));
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] result = new byte[hmac.GetMacSize()];
            hmac.BlockUpdate(messageBytes, 0, messageBytes.Length);
            hmac.DoFinal(result, 0);

            return BitConverter.ToString(result).Replace("-", "").ToUpper();
        }
    }
}