using System.Security.Cryptography;
using System.Text;

namespace Dice_Game
{
    internal class HMAC
    {
        public string? Key { get; private set; }

        public string? HMACHash { get; private set; }

        public void CreateHMAC(string message, int keyHexStringLength = 64)
        {
            Key = RandomNumberGenerator.GetHexString(keyHexStringLength, true);
            byte[] HMAC = HMACSHA3_256.HashData(
                Encoding.ASCII.GetBytes(Key),
                Encoding.ASCII.GetBytes(message)
                );
            HMACHash = Convert.ToHexStringLower(HMAC);
        }

        public void WriteHMAC()
        {
            Console.WriteLine($"(HMAC={HMACHash}).\n");
        }

        public void WriteKey()
        {
            Console.WriteLine($"(KEY={Key}).\n");
        }
    }
}