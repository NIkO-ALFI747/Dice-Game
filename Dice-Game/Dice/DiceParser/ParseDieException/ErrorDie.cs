namespace Dice_Game.Dice.DiceParser.ParseDieException
{
    internal class ErrorDie(string face, string die)
    {
        public string Face { get; } = face;

        public string Die { get; } = die;
    }
}