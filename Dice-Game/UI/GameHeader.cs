using Spectre.Console;

namespace Dice_Game.UI
{
    internal class GameHeader
    {
        private FigletText GameTitle { get; set; }

        private Panel Panel { get; }

        public GameHeader(string gameTitleText = "DICE GAME") {
            GameTitle = SetGameTitle(gameTitleText);
            Panel = SetPanel(GameTitle);
        }

        public static Panel GetPanel(string gameTitleText)
        {
            var gameTitle = SetGameTitle(gameTitleText);
            return SetPanel(gameTitle);
        }

        private static Panel SetPanel(FigletText gameTitle)
        {
            return new Panel(gameTitle)
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Green))
                .Padding(1, 1);
        }

        private static FigletText SetGameTitle(string gameTitleText)
        {
            return new FigletText(gameTitleText)
                .Centered()
                .Color(Color.Green);
        }

        public void Write()
        {
            AnsiConsole.Write(Panel);
        }

        public static void Write(string gameTitleText = "DICE GAME")
        {
            AnsiConsole.Write(GetPanel(gameTitleText));
        }
    }
}