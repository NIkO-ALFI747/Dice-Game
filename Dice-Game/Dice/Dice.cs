using Dice_Game.Dice.DiceParser;

namespace Dice_Game.Dice
{
    internal class Dice
    {
        public string[] DiceArr { get; }

        public List<List<byte>> ParsedDice { get; }

        public Dice(string[] dice)
        {
            var diceParser = new DiceParser<byte>(byte.MinValue, byte.MaxValue, 3);
            ParsedDice = diceParser.CheckAndParseDice(dice);
            DiceArr = [.. dice];
        }

        public List<List<double>> GetProbs()
        {
            return DiceProbabilityCalculator<byte>.Get(ParsedDice);
        }
    }
}