namespace Dice_Game.Dice.DiceParser.CheckNumberOfDiceException
{
    internal class CNODException : Exception
    {
        private uint ErrorNumberOfDice { get; }

        public CNODErrorMessage ErrorMessage { get; }

        public CNODException(uint errorNumberOfDice, uint requiredNumberOfDice)
        {
            ErrorNumberOfDice = errorNumberOfDice;
            ErrorMessage = new CNODErrorMessage(ErrorNumberOfDice, requiredNumberOfDice);
        }
    }
}