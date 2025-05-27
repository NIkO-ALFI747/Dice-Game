using Dice_Game.Dice.DiceParser.Text;
using System.ComponentModel;

namespace Dice_Game.Dice.DiceParser.CheckNumberOfDiceException.Text
{
    internal class CNODHowToCorrectErrorText : HowToCorrectErrorText
    {
        public CNODHowToCorrectErrorText(uint requiredNumberOfDice,
            string description = "The number of the dice must be greater than {0}!"
            )
        {
            Description = string.Format(description, requiredNumberOfDice - 1);
        }
    }
}