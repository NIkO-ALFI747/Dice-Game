using Spectre.Console;
using System.Text;

namespace Dice_Game.UI
{
    internal class SelectionMenu
    {
        private Option ExitOption { get; }

        private Option HelpOption { get; }

        private int? SerialUpperBound { get; set; }

        private List<Option>? ArrOptions { get; set; }

        public SelectionMenu()
        {
            ExitOption = new Option("❌", " - ", "Exit");
            HelpOption = new Option("❓", " - ", "Help");
        }

        private static void CheckSerialBounds(int arrLength, int lowerBound, int upperBound)
        {
            CheckRelationBetweenBounds(lowerBound, upperBound);
            CheckArrayAndBoundsRanges(arrLength, lowerBound, upperBound);
        }

        private static void CheckRelationBetweenBounds(int lowerBound, int upperBound)
        {
            if (upperBound - lowerBound < 0) 
                throw new ArgumentException("Upper Bound is lower than Lower Bound");
        }

        private static void CheckArrayAndBoundsRanges(int arrLength, int lowerBound, int upperBound)
        {
            if (upperBound - lowerBound + 1 > arrLength) 
                throw new ArgumentException("Array Length is lower than the range between the bounds");
        }

        private static List<Option> GetArrOptions(string[] arr, int lowerBound, int upperBound)
        {
            var arrOptions = new List<Option>();
            for (int i = lowerBound; i <= upperBound; i++)
                arrOptions.Add(new Option($"{i}", " - ", $"{arr[i]}"));
            return arrOptions;
        }

        private static List<Option> GetArrOptions(int lowerBound, int upperBound)
        {
            var arrOptions = new List<Option>();
            for (int i = lowerBound; i <= upperBound; i++)
                arrOptions.Add(new Option($"{i}", " - ", $"{i}"));
            return arrOptions;
        }

        private string GetArrOptions()
        {
            var sb = new StringBuilder();
            if (ArrOptions == null) throw new NullReferenceException();
            foreach (var option in ArrOptions)
                sb.AppendLine($"{option.ToString()}\n");
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        private string GetMenu()
        {
            return $"{GetArrOptions()}\n{ExitOption}\n{HelpOption}";
        }

        public string GetMenu(string[] arr)
        {
            return GetMenu(arr, 0, arr.Length);
        }

        private void SetArrOptions(string[] arr, int serialLowerBound, int serialUpperBound)
        {
            CheckSerialBounds(arr.Length, serialLowerBound, serialUpperBound);
            ArrOptions = GetArrOptions(arr, serialLowerBound, serialUpperBound);
        }

        private void SetArrOptions(int serialUpperBound, int serialLowerBound = 0)
        {
            CheckRelationBetweenBounds(serialLowerBound, serialUpperBound);
            ArrOptions = GetArrOptions(serialLowerBound, serialUpperBound);
        }

        public string GetMenu(string[] arr, int serialLowerBound, int serialUpperBound)
        {
            SetArrOptions(arr, serialLowerBound, serialUpperBound);
            return GetMenu();
        }

        public string GetMenu(int serialUpperBound, int serialLowerBound = 0)
        {
            SetArrOptions(serialLowerBound, serialUpperBound);
            return GetMenu();
        }

        private List<Option> GetAllOptions(int serialUpperBound, int serialLowerBound = 0)
        {
            SetArrOptions(serialUpperBound, serialLowerBound);
            return GetAllOptions();
        }

        private List<Option> GetAllOptions(string[] arr)
        {
            if (SerialUpperBound != null) SetArrOptions(arr, 0, SerialUpperBound.Value);
            else throw new Exception("UpperBound isn't set");
            return GetAllOptions();
        }

        private List<Option> GetAllOptions()
        {
            return [.. ArrOptions ?? [], ExitOption, HelpOption];
        }

        private List<string> GetAllStrOptions(int serialUpperBound, int serialLowerBound = 0)
        {
            return GetAllStrOptions(GetAllOptions(serialUpperBound, serialLowerBound));
        }

        private List<string> GetAllStrOptions(string[] arr)
        {
            return GetAllStrOptions(GetAllOptions(arr));
        }

        private static List<string> GetAllStrOptions(List<Option> options)
        {
            var str_options = new List<string>();
            foreach (var opt in options)
                str_options.Add(opt.ToString());
            return str_options;
        }

        private static SelectionPrompt<string> GetSelectionPrompt(List<string> options,
            string title, int numOfDisplayedChoices = 10
            )
        {
            return new SelectionPrompt<string>()
                .Title($"[green]{title}[/]")
                .PageSize(numOfDisplayedChoices)
                .AddChoices(options);
        }

        public int TrySelect(int serialUpperBound, Dice.Dice dice, string title)
        {
            var options = GetAllStrOptions(serialUpperBound);
            SerialUpperBound = serialUpperBound;
            return TrySelect(options, dice, title);
        }

        public int TrySelect(string[] arr, Dice.Dice dice, string title)
        {
            SerialUpperBound = arr.Length - 1;
            var options = GetAllStrOptions(arr);
            return TrySelect(options, dice, title);
        }

        public int TrySelect(List<string> options, Dice.Dice dice,
            string title = "Select an option:", int safeCounter = 100)
        {
            int usersRandNumber = 0;
            var selected = false;
            for (; !selected && safeCounter != 0; safeCounter--)
                MakeChoice(options, dice, ref selected, out usersRandNumber, title);
            return usersRandNumber;
        }

        private void MakeChoice(List<string> options, Dice.Dice dice, ref bool selected,
            out int usersRandNumber, string title
            )
        {
            var parsedChoice = GetParsedChoice(options, title);
            if (int.TryParse(parsedChoice[0], out usersRandNumber))
                selected = CheckNumber(usersRandNumber);
            else TryRunOperation(parsedChoice[0], dice);
        }

        private static string[] GetParsedChoice(List<string> options, string title)
        {
            var choice = AnsiConsole.Prompt(GetSelectionPrompt(options, title));
            return Option.ParseChoice(choice, " - ");
        }

        private bool CheckNumber(int number)
        {
            if (SerialUpperBound != null) TryCheckNumber(number, (int)SerialUpperBound);
            else throw new Exception("Set check parameters!");
            return true;
        }

        private static void TryCheckNumber(int number, int serialUpperBound)
        {
            if (!(number >= 0 && number <= serialUpperBound)) throw new Exception();
        }

        private static void ExitOperation()
        {
            AnsiConsole.Markup("[yellow]Goodbyyye!!![/]");
            Environment.Exit(ExitStatusCode.OK);
        }

        private static void HelpOperation(Dice.Dice dice)
        {
            var table = new TableGenerator(dice.GetProbs(), dice.DiceArr);
            table.Write();
            AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
            Console.ReadKey(true);
        }

        private void TryRunOperation(string op, Dice.Dice dice)
        {
            if (op == ExitOption.Index) ExitOperation();
            else if (op == HelpOption.Index) HelpOperation(dice);
            else throw new Exception("Incorrect operation!");
        }
    }
}