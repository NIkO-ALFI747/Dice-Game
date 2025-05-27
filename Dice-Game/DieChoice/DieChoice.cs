using Dice_Game.UI;
using System.Security.Cryptography;

namespace Dice_Game.DieChoice
{
    internal class DieChoice
    {
        public int? PCsChoice { get; private set; }

        public int? UsersChoice { get; private set; }
        
        private static double FindMinOfMaxProbs(List<double> maximums)
        {
            var min = maximums.First();
            foreach (var max in maximums)
                if (max < min) min = max;
            return min;
        }

        private static List<int> FindMinElIndexesOfMaxProbs(List<double> maximums, double min)
        {
            var minElIndexes = new List<int>();
            for (int i = 0; i < maximums.Count; i++)
                if (maximums[i] == min) minElIndexes.Add(i);
            return minElIndexes;
        }

        private static List<int> FindMaxElIndexesOfMaxProbs(
            List<List<double>> probs, double max, int chosenColumn
            )
        {
            var maxElIndexes = new List<int>();
            for (int i = 0; i < probs.Count; i++)
                if ((probs[i][chosenColumn] == max) && (i != chosenColumn)) maxElIndexes.Add(i);
            return maxElIndexes;
        }

        private static List<int> FindMinElIndexesOfMaxProbs(List<List<double>> probs)
        {
            var maxLosingProbs = FindMaxLosingProbs(probs);
            return FindMinElIndexesOfMaxProbs(maxLosingProbs, FindMinOfMaxProbs(maxLosingProbs));
        }

        private static List<double> FindMaxLosingProbs(List<List<double>> probs)
        {
            var maximums = new List<double>();
            for (int j = 0; j < probs.Count; j++)
                maximums.Add(FindMaxInColumn(probs, j));
            return maximums;
        }

        private static double FindMaxInColumn(List<List<double>> probs, int j)
        {
            var max = probs[0][j];
            for (int i = 0; i < probs.Count; i++)
                if (probs[i][j] > max) max = probs[i][j];
            return max;
        }

        private static int ChooseFirstDie(List<List<double>> probs)
        {
            var minElIndexes = FindMinElIndexesOfMaxProbs(probs);
            int randDieIndex = RandomNumberGenerator.GetInt32(0, minElIndexes.Count);
            return minElIndexes[randDieIndex];
        }

        private static int ChooseSecondDie(List<List<double>> probs, int firstDieIndex)
        {
            var max = FindMaxInColumn(probs, firstDieIndex);
            var maxElIndexes = FindMaxElIndexesOfMaxProbs(probs, max, firstDieIndex);
            int randDieIndex = RandomNumberGenerator.GetInt32(0, maxElIndexes.Count);
            return maxElIndexes[randDieIndex];
        }

        public static string[] RemoveRow(string[] arr, int index)
        {
            var res = arr.ToList();
            res.RemoveAt(index);
            return [.. res];
        }

        public static int GetOriginalIndex(int deletedIndex, int newIndex)
        {
            if (newIndex < deletedIndex) return newIndex;
            else return newIndex + 1;
        }

        private int MakePCsChoice(Dice.Dice dice)
        {
            int PCsDie;
            if (UsersChoice != null) PCsDie = ChooseSecondDie(dice.GetProbs(), UsersChoice.Value);
            else PCsDie = ChooseFirstDie(dice.GetProbs());
            Console.WriteLine($"I make the move and choose the {dice.DiceArr[PCsDie]} dice.");
            return PCsDie;
        }

        private int MakeUsersChoice(Dice.Dice dice)
        {
            var menu = new SelectionMenu();
            var diceArr = dice.DiceArr;
            if (PCsChoice != null) diceArr = RemoveRow(diceArr, PCsChoice.Value);
            return GetUsersDie(dice, menu, diceArr);
        }

        private int GetUsersDie(Dice.Dice dice, SelectionMenu menu, string[] diceArr)
        {
            int usersDie = menu.TrySelect(diceArr, dice, "Choose your dice:");
            Console.WriteLine($"Your selection: {usersDie}");
            Console.WriteLine($"You chose the [{diceArr[usersDie]}] dice.");
            if (PCsChoice != null) usersDie = GetOriginalIndex(PCsChoice.Value, usersDie);
            return usersDie;
        }

        private void MakeUsersTurnChoice(Dice.Dice dice)
        {
            UsersChoice = MakeUsersChoice(dice);
            PCsChoice = MakePCsChoice(dice);
        }

        private void MakePCsTurnChoice(Dice.Dice dice)
        {
            PCsChoice = MakePCsChoice(dice);
            UsersChoice = MakeUsersChoice(dice);
        }

        public void Run(bool usersTurn, Dice.Dice dice)
        {
            if (usersTurn) MakeUsersTurnChoice(dice);
            else MakePCsTurnChoice(dice);
        }
    }
}