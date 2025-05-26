using Dice_Game.Dice.DiceParser.ParseDieException.Text;

namespace Dice_Game.Dice.DiceParser.ParseDieException
{
    internal class PDErrorMessage<T>(ErrorDie errorDie, RequiredRange<T> requiredRange)
    {
        private PDErrorText ErrorText { get; } = new (errorDie);

        private PDHowToCorrectErrorText<T> HowToCorrectErrorText { get; } = new (requiredRange);

        private PDExamplesErrorText ExamplesErrorText { get; } = new ();

        public string GetMarkup()
        {
            return $"{ErrorText.GetAllMarkupText()}\n" +
                $"{HowToCorrectErrorText.GetAllMarkupText()}\n" +
                $"{ExamplesErrorText.GetAllMarkupText()}\n";
        }
    }
}