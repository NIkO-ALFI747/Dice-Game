namespace Dice_Game.Dice.DiceParser
{
    internal class RequiredRange<T>(T minValue, T maxValue)
    {
        public T MinValue { get; } = minValue;

        public T MaxValue { get; } = maxValue;
    }
}