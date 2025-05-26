using Spectre.Console;

namespace Dice_Game.UI
{
    internal class TableGenerator
    {
        public Table Table { get; set; }

        public TableGenerator(List<List<double>> probs, string[] dice)
        {
            Table = new Table()
                .Title("[underline bold]Probability of the win for the user:[/]")
                .Border(TableBorder.Rounded);
            FillTable(probs, dice);
        }

        public void Write()
        {
            AnsiConsole.Write(Table);
        }

        private void AddDataColumns(string[] dice)
        {
            foreach(var die in dice)
                Table.AddColumn(new TableColumn($"[blue]{die}[/]").Centered());
        }

        private void AddDataRows(List<List<double>> probs, string[] dice)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                var rowProbs = new List<string>{$"{dice[i]}"};
                for (int j = 0; j < probs[i].Count; j++)
                    rowProbs.Add($"[green]{probs[i][j]}[/]");
                Table.AddRow(rowProbs.ToArray());
            }
        }

        private void AddTitleColumn()
        {
            Table.AddColumn(new TableColumn("[yellow]User's dice\n below v[/]").Centered());
        }

        private void FillTable(List<List<double>> probs, string[] dice)
        {
            AddTitleColumn();
            AddDataColumns(dice);
            AddDataRows(probs, dice);
        }
    }
}