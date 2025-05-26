using Dice_Game.Dice.DiceParser.Text;

namespace Dice_Game.Dice.DiceParser.ParseDieException.Text
{
    internal class PDErrorText : ErrorText
    {
        public PDErrorText(ErrorDie errorDie) : base()
        {
            Description = GetDescription(errorDie);
        }

        private static string GetDescription(ErrorDie errorDie)
        {
            return GetDescription(errorDie.Face, errorDie.Die);
        }

        private static string GetDescription(string face, string die)
        {
            return $"The face {face} of the die {die} has an invalid format!";
        }
    }
}