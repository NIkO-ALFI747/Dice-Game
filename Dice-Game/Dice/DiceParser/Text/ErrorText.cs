namespace Dice_Game.Dice.DiceParser.Text
{
    internal class ErrorText : Text
    {
        public ErrorText(
            string description,
            string title = "Error: "
            ) : base(description, title)
        {}

        public ErrorText() : base()
        {
            Title = "Error: "; 
        }

        public string GetAllMarkupText()
        {
            return GetAllMarkupText(Title, Description);
        }

        private string GetAllMarkupText(string? title, string? description)
        {
            if (ArentNull(title, description)) return $"[bold red]{title}[/] {description}";
            return "";
        }
    }
}