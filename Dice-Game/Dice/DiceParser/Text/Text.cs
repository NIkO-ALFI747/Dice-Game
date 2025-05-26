namespace Dice_Game.Dice.DiceParser.Text
{
    internal class Text
    {
        protected string? Title { get; set; }

        protected string? Description { get; set; }

        protected string? AllText { get; set; }

        public Text()
        {}

        public Text(string allText)
        {
            AllText = allText;
        }

        public Text(string description, string title)
        {
            Title = title;
            Description = description;
        }

        public string GetAllText()
        {
            return GetAllText(Title, Description);
        }

        private static string GetAllText(string? title, string? description)
        {
            if (ArentNull(title, description)) return $"{title} {description}";
            return "";
        }

        protected static bool ArentNull(string? s1, string? s2)
        {
            return s1 != null && s2 != null;
        }
    }
}