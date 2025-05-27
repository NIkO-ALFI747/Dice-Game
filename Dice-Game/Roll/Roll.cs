namespace Dice_Game.Roll
{
    internal class Roll : FairRandNumberGenerator
    {
        public void WriteResult()
        {
            Console.WriteLine($"The fair number generation result is " +
                $"{PCsRandNumber} + {UsersRandNumber} = {FairRandNumber} (mod {UpperBound + 1}).");
        }

        public void Run(Dice.Dice dice, string rollTitle,
            string selectionTitle, int keyHexStringLength = 64)
        {
            Console.WriteLine(rollTitle);
            Run(dice, selectionTitle, keyHexStringLength);
            SetFairRandNumber();
            WriteResult();
        }
    }
}