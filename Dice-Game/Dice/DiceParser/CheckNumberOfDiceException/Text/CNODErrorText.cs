using Dice_Game.Dice.DiceParser.Text;

namespace Dice_Game.Dice.DiceParser.CheckNumberOfDiceException.Text
{
    internal class CNODErrorText : ErrorText
    {
        public CNODErrorText(uint errorNumberOfDice) : base()
        {
            Description = GetDescription(errorNumberOfDice);
        }

        private static string GetDescription(uint errorNumberOfDice)
        {
            return $"The number of the dice equals {errorNumberOfDice}!";
        }
    }
}