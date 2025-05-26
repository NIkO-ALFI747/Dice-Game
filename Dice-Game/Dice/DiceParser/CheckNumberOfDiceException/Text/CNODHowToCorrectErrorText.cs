using Dice_Game.Dice.DiceParser.Text;

namespace Dice_Game.Dice.DiceParser.CheckNumberOfDiceException.Text
{
    internal class CNODHowToCorrectErrorText(
        string description = "The number of the dice must be greater than 2!"
        ) : HowToCorrectErrorText(description)
    {}
}