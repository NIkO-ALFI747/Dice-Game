using Dice_Game.Dice.DiceParser.CheckNumberOfDiceException.Text;

namespace Dice_Game.Dice.DiceParser.CheckNumberOfDiceException
{
    internal class CNODErrorMessage(uint errorNumberOfDice)
    {
        private CNODErrorText ErrorText { get; } = new (errorNumberOfDice);

        private CNODHowToCorrectErrorText HowToCorrectErrorText { get; } = new ();

        public string GetMarkup()
        {
            return $"{ErrorText.GetAllMarkupText()}\n{HowToCorrectErrorText.GetAllMarkupText()}\n";
        }
    }
}