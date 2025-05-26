namespace Dice_Game.DieChoice
{
    internal class FirstDieChoiceDeterminator(
        int lowerBound,
        int upperBound,
        string invitation = "Let's determine who makes the first move."
        ) : FairRandNumberGenerator(lowerBound, upperBound)
    {
        private string Invitation { get; } = invitation;

        public bool UserGuessed()
        {
            return FairRandNumber == 0;
        }

        public new bool Run(Dice.Dice dice, string selectionTitle)
        {
            Console.WriteLine(Invitation);
            base.Run(dice, selectionTitle);
            SetFairRandNumber();
            return UserGuessed();
        }
    }
}