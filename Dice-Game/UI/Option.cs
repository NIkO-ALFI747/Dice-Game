namespace Dice_Game.UI
{
    internal class Option(string index, string separator, string value)
    {
        public string Index { get; } = index;

        private string Separator { get; } = separator;

        private string Value { get; } = value;

        public override string ToString()
        {
            return $"{Index}{Separator}{Value}";
        }

        public static string[] ParseChoice(string choice, string separator)
        {
            return choice.Split(separator);
        }
    }
}