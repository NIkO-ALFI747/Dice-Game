namespace Dice_Game.Dice.DiceParser.ParseDieException
{
    internal class PDException<T> : Exception
    {
        ErrorDie ErrorDie { get; }

        public PDErrorMessage<T> ErrorMessage { get; }

        public PDException(string face, string die, RequiredRange<T> requiredRange)
        {
            ErrorDie = new ErrorDie(face, die);
            ErrorMessage = new PDErrorMessage<T>(ErrorDie, requiredRange);
        }
    }
}