using System.Numerics;

namespace Dice_Game.Dice
{
    internal static class DiceProbabilityCalculator<T> where T : INumber<T>
    {
        public static List<List<double>> Get(List<List<T>> dice)
        {
            var probs = InitDefaultProbs(dice.Count);
            CountProbs(dice, ref probs);
            return probs;
        }

        private static List<List<double>> InitDefaultProbs(int numberOfDice)
        {
            var probs = new List<List<double>>(numberOfDice);
            for (int i=0; i<numberOfDice; i++)
                probs.Add(Enumerable.Repeat(0.0,numberOfDice).ToList());
            return probs;
        }

        private static void CountProbs(List<List<T>> dice, ref List<List<double>> probs)
        {
            for (int i = 0; i < dice.Count; i++)
                for (int j = 0; j < dice.Count; j++)
                    if (i != j) probs[i][j] = CountProb(dice[i], dice[j]);
        }

        private static double CountProb(List<T> die1, List<T> die2)
        {
            var wins = CountWins(die1, die2);
            return Convert.ToDouble(wins) / Convert.ToDouble(die1.Count * die2.Count);
        }

        private static int CountWins(List<T> die1, List<T> die2)
        {
            return die1.SelectMany(_ => die2, (face1, face2) => face1 > face2).Count(face1 => face1);
        }
    }
}