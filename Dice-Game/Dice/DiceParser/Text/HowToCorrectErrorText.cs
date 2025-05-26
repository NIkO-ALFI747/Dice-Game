namespace Dice_Game.Dice.DiceParser.Text
{
    internal class HowToCorrectErrorText : Text
    {
        public HowToCorrectErrorText(
            string description,
            string title = "How to correct: "
            ) : base(description, title)
        {}

        public HowToCorrectErrorText() : base()
        { 
            Title = "How to correct: "; 
        }

        public string GetAllMarkupText()
        {
            return GetAllMarkupText(Title, Description);
        }

        private string GetAllMarkupText(string? title, string? description)
        {
            if (ArentNull(title, description)) return $"[bold green]{title}[/] {description}";
            return "";
        }
    }
}