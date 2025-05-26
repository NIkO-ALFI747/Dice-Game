using Dice_Game.Dice.DiceParser.Text;

namespace Dice_Game.Dice.DiceParser.ParseDieException.Text
{
    internal class PDExamplesErrorText(
        string description = "1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6",
        string title = "Examples of the correct dice format: "
        ) : ExamplesErrorText(description, title)
    {}
}