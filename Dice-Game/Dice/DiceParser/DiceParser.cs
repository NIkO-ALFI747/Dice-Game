using Spectre.Console;
using System.Numerics;
using Dice_Game.Dice.DiceParser.CheckNumberOfDiceException;
using Dice_Game.Dice.DiceParser.ParseDieException;

namespace Dice_Game.Dice.DiceParser
{
    internal class DiceParser<DPT>(DPT minValue, DPT maxValue, uint requiredNumberOfDice) 
        where DPT : IParsable<DPT>, IMinMaxValue<DPT>
    {
        private RequiredRange<DPT> DPRequiredRange { get; set; } = new (minValue, maxValue);

        private uint RequiredNumberOfDice { get; set; } = requiredNumberOfDice;

        private void CheckNumberOfDice(string[] dice)
        {
            if (dice.Length < RequiredNumberOfDice) throw new CNODException((uint)dice.Length, RequiredNumberOfDice);
        }

        private void TryCheckNumberOfDice(string[] dice)
        {
            try
            {
                CheckNumberOfDice(dice);
            }
            catch (CNODException e)
            {
                AnsiConsole.Markup(e.ErrorMessage.GetMarkup());
                Environment.Exit(ExitStatusCode.OK);
            }
        }

        private List<List<DPT>> ParseDice(string[] dice)
        {
            List<List<DPT>> parsedDice = [];
            foreach (var die in dice)
                parsedDice.Add(TryParseDie(die));
            return parsedDice;
        }

        private List<DPT> TryParseDie(string die)
        {
            List<DPT> parsedDie = [];
            try
            {
                parsedDie = ParseDie(die);
            }
            catch (PDException<DPT> e)
            {
                AnsiConsole.Markup(e.ErrorMessage.GetMarkup());
                Environment.Exit(ExitStatusCode.OK);
            }
            return parsedDie;
        }

        private List<DPT> ParseDie(string die)
        {
            List<DPT> parsedDie = [];
            foreach (var face in die.Split(','))
            {
                DPT parcedFace = ParseFace(die, face);
                parsedDie.Add(parcedFace);
            }
            return parsedDie;
        }

        private DPT ParseFace(string die, string face)
        {
            if (DPT.TryParse(face, null, out DPT? parcedFace))
            {
                if (parcedFace == null) throw new InvalidCastException();
            }
            else
            {
                throw new PDException<DPT>(face, die, DPRequiredRange);
            }
            return parcedFace;
        }

        public List<List<DPT>> CheckAndParseDice(string[] dice)
        {
            TryCheckNumberOfDice(dice);
            return ParseDice(dice);
        }
    }
}