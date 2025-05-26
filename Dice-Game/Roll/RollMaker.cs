namespace Dice_Game.Roll
{
    internal class RollMaker
    {
        private enum GameResults { PCS_VICTORY, USERS_VICTORY, DRAW }

        private readonly Dictionary<GameResults, string> gameResultMessages = new()
        {
            { GameResults.PCS_VICTORY, "I won!" },
            { GameResults.USERS_VICTORY, "You won!" },
            { GameResults.DRAW, "It's a draw!" }
        };

        private static byte MakeRoll(int? choice, Roll roll, Dice.Dice dice,
            string rollResultMessage, string rollTitle
            )
        {
            if (choice == null) throw new Exception("The die choice hasn't been made!");
            var upperBound = dice.ParsedDice[choice.Value].Count - 1;
            roll.SetBounds(0, upperBound);
            roll.Run(dice, rollTitle, $"Add your number modulo {upperBound + 1}.");
            return GetRollResult(dice, choice.Value, roll.FairRandNumber, rollResultMessage);
        }

        private static byte GetRollResult(Dice.Dice dice, int choice, 
            int fairRandNumber, string rollResultMessage
            )
        {
            var number = dice.ParsedDice[choice][fairRandNumber];
            Console.WriteLine(rollResultMessage, number);
            return number;
        }

        private void SetGameResultMessages(byte PCsNumber, byte usersNumber)
        {
            if (PCsNumber > usersNumber) 
                gameResultMessages[GameResults.PCS_VICTORY] = $"I won ({PCsNumber} > {usersNumber})!";
            else if (PCsNumber < usersNumber) 
                gameResultMessages[GameResults.USERS_VICTORY] = $"You won ({PCsNumber} < {usersNumber})!";
            else gameResultMessages[GameResults.DRAW] = $"It's a draw ({PCsNumber} = {usersNumber})!";
        }

        private GameResults GetGameResult(byte PCsNumber, byte usersNumber)
        {
            SetGameResultMessages(PCsNumber, usersNumber);
            if (PCsNumber > usersNumber) return GameResults.PCS_VICTORY;
            else if (PCsNumber < usersNumber) return GameResults.USERS_VICTORY;
            else return GameResults.DRAW;
        }

        private void MakeUsersTurnRoll(Dice.Dice dice, DieChoice.DieChoice dieChoice, Roll roll)
        {
            var usersNumber = MakeRoll(dieChoice.UsersChoice, roll, dice,
                    "Your roll result is {0}.", "It's time for your roll.");
            var PCsNumber = MakeRoll(dieChoice.PCsChoice, roll, dice,
                "My roll result is {0}.", "It's time for my roll.");
            Console.WriteLine(gameResultMessages[GetGameResult(PCsNumber, usersNumber)]);
        }

        private void MakePCsTurnRoll(Dice.Dice dice, DieChoice.DieChoice dieChoice, Roll roll)
        {
            var PCsNumber = MakeRoll(dieChoice.PCsChoice, roll, dice,
                "My roll result is {0}.", "It's time for my roll.");
            var usersNumber = MakeRoll(dieChoice.UsersChoice, roll, dice,
                "Your roll result is {0}.", "It's time for your roll.");
            Console.WriteLine(gameResultMessages[GetGameResult(PCsNumber, usersNumber)]);
        }

        public void Run(bool usersTurn, Dice.Dice dice, DieChoice.DieChoice dieChoice)
        {
            var roll = new Roll();
            if (usersTurn) MakeUsersTurnRoll(dice, dieChoice, roll);
            else MakePCsTurnRoll(dice, dieChoice, roll);
        }
    }
}