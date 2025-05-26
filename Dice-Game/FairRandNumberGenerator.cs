using Dice_Game.UI;
using Spectre.Console;
using System.Security.Cryptography;

namespace Dice_Game
{
    internal class FairRandNumberGenerator
    {
        private int? LowerBound { get; set; }

        protected int? UpperBound { get; private set; }

        public int PCsRandNumber { get; private set; }

        public int UsersRandNumber { get; private set; }

        public int FairRandNumber { get; protected set; }

        public FairRandNumberGenerator(int lowerBound, int upperBound)
        {
            SetBounds(lowerBound, upperBound);
        }

        public FairRandNumberGenerator()
        {}

        public void SetBounds(int lowerBound, int upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        private void SetPCsRandNumber()
        {
            if (LowerBound == null || UpperBound == null) throw new Exception("Bounds aren't set!");
            PCsRandNumber = RandomNumberGenerator.GetInt32(LowerBound.Value, UpperBound.Value + 1);
            WritePCsRandRange();
        }

        private void WritePCsRandRange(string message = "I selected a random value in the range {0}..{1}")
        {
            Console.WriteLine(message, LowerBound, UpperBound);
        }

        private void WritePCsRandNumber(string message = "My selection: {0}")
        {
            Console.WriteLine(message, PCsRandNumber);
        }

        private void WriteUsersRandNumber(string message = "[blue]Your selection:[/] {0}")
        {
            AnsiConsole.MarkupLine(message, UsersRandNumber);
        }

        private void SetUsersRandNumber(int choice)
        {
            UsersRandNumber = choice;
        }

        private void SetUsersRandNumber(Dice.Dice dice, string selectionTitle)
        {
            var menu = new SelectionMenu();
            if (UpperBound == null) throw new Exception("Upper Bound isn't set!");
            var usersRandNumber = menu.TrySelect(UpperBound.Value, dice, selectionTitle);
            SetUsersRandNumber(usersRandNumber);
        }

        private void WriteRandNumbers(HMAC hmac)
        {
            WriteUsersRandNumber();
            WritePCsRandNumber();
            hmac.WriteKey();
        }

        private static HMAC GetHMAC(int PCsRandNumber)
        {
            var hmac = new HMAC();
            hmac.CreateHMAC(PCsRandNumber.ToString());
            hmac.WriteHMAC();
            return hmac;
        }

        protected void SetFairRandNumber()
        {
            if (UpperBound == null) throw new Exception("Upper Bound isn't set!");
            FairRandNumber = (UsersRandNumber + PCsRandNumber) % (UpperBound.Value + 1);
        }

        public void Run(Dice.Dice dice, string selectionTitle)
        {
            SetPCsRandNumber();
            var hmac = GetHMAC(PCsRandNumber);
            SetUsersRandNumber(dice, selectionTitle);
            WriteRandNumbers(hmac);
        }
    }
}