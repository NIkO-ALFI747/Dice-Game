using Dice_Game.Dice.DiceParser.Text;

namespace Dice_Game.Dice.DiceParser.ParseDieException.Text
{
    internal class PDHowToCorrectErrorText<T> : HowToCorrectErrorText
    {
        public PDHowToCorrectErrorText(
            string description = "Write each face as a number followed by a comma."
            ) : base(description)
        { }

        public PDHowToCorrectErrorText(RequiredRange<T> requiredRange) : base()
        {
            Description = GetDescription(requiredRange);
        }

        private static string GetDescription(RequiredRange<T> requiredRange)
        {
            return GetDescription(requiredRange.MinValue, requiredRange.MaxValue);
        }

        private static string GetDescription(T minValue, T maxValue)
        {
            return $"Write each face as a number between {minValue} " +
                $"and {maxValue} followed by a comma.";
        }
    }
}