using Dice_Game.Dice.DiceParser.CheckNumberOfDiceException.Text;

namespace Dice_Game.Dice.DiceParser.CheckNumberOfDiceException
{
    internal class CNODErrorMessage(uint errorNumberOfDice, uint requiredNumberOfDice)
    {
        private CNODErrorText ErrorText { get; } = new (errorNumberOfDice);

        private CNODHowToCorrectErrorText HowToCorrectErrorText { get; } = new (requiredNumberOfDice);

        public string GetMarkup()
        {
            return $"{ErrorText.GetAllMarkupText()}\n{HowToCorrectErrorText.GetAllMarkupText()}\n";
        }
    }
}