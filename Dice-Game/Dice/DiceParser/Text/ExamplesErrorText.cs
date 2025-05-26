namespace Dice_Game.Dice.DiceParser.Text
{
    internal class ExamplesErrorText(
        string description,
        string title = "Correct examples: "
        ) : Text(description, title)
    {
        public string GetAllMarkupText()
        {
            return GetAllMarkupText(Title, Description);
        }

        private string GetAllMarkupText(string? title, string? description)
        {
            if (ArentNull(title, description)) return $"[bold blue]{title}[/] {description}";
            return "";
        }
    }
}