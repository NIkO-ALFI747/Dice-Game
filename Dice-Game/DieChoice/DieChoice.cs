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

        private static List<int> FindMinElIndexesOfMaxProbs(List<List<double>> probs)
        {
            var maxLosingProbs = FindMaxLosingProbs(probs);
            return FindMinElIndexesOfMaxProbs(maxLosingProbs, FindMinOfMaxProbs(maxLosingProbs));
        }

        private static List<double> FindMaxLosingProbs(List<List<double>> probs)
        {
            var maximums = new List<double>();
            for (int j = 0; j < probs.Count; j++)
                maximums.Add(FindMaxInColumn(ref probs, j));
            return maximums;
        }

        private static double FindMaxInColumn(ref List<List<double>> probs, int j)
        {
            var max = probs[0][j];
            for (int i = 0; i < probs.Count; i++)
                if (probs[i][j] > max) max = probs[i][j];
            return max;
        }

        private static int ChooseDie(List<List<double>> probs)
        {
            var minElIndexes = FindMinElIndexesOfMaxProbs(probs);
            int randDieIndex = RandomNumberGenerator.GetInt32(0, minElIndexes.Count);
            return minElIndexes[randDieIndex];
        }

        public static void CheckMatrix(List<List<double>> matrix)
        {
            if (matrix == null || matrix.Count == 0)
                throw new ArgumentNullException(nameof(matrix), "The matrix is empty or null!");
            if (matrix.Any(row => row == null))
                throw new ArgumentNullException(nameof(matrix), "The row of the matrix is null!");
        }

        public static void CheckIndexes(int rowIndex, int colIndex, int matrixSize)
        {
            if (rowIndex < 0 || rowIndex >= matrixSize)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index is out of bounds!");
            if (colIndex < 0 || colIndex >= matrixSize)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Column index is out of bounds!");
        }

        public static void RemoveRowAndColumn(List<List<double>> matrix, int rowIndex, int colIndex)
        {
            CheckMatrix(matrix);
            CheckIndexes(rowIndex, colIndex, matrix.Count);
            matrix.RemoveAt(rowIndex);
            foreach (var row in matrix)
                row.RemoveAt(colIndex);
        }

        public static void RemoveRowAndColumn(List<List<double>> matrix, int index)
        {
            RemoveRowAndColumn(matrix, index, index);
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
            var probs = dice.GetProbs();
            if (UsersChoice != null) RemoveRowAndColumn(probs, UsersChoice.Value);
            var PCsDie = ChooseDie(probs);
            if (UsersChoice != null) PCsDie = GetOriginalIndex(UsersChoice.Value, PCsDie);
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