namespace Dice_Game.DieChoice
{
    internal class FirstDieChoiceDeterminator(string invitation = "Let's determine who makes the first move."
        ) : FairRandNumberGenerator(LOWER_BOUND, UPPER_BOUND)
    {
        private const int LOWER_BOUND = 0;

        private const int UPPER_BOUND = 1;

        private string Invitation { get; } = invitation;

        public bool UserGuessed()
        {
            return FairRandNumber == 0;
        }

        public bool Run(Dice.Dice dice,
            int keyHexStringLength = 64,
            string selectionTitle = "Try to guess my selection."
            )
        {
            Console.WriteLine(Invitation);
            Run(dice, selectionTitle, keyHexStringLength);
            SetFairRandNumber();
            return UserGuessed();
        }
    }
}