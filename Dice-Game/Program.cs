using Dice_Game.Dice;
using Dice_Game.DieChoice;
using Dice_Game.Roll;
using Dice_Game.UI;

const uint REQUIRED_NUMBER_OF_DICE = 3;
var dice = new Dice(args, REQUIRED_NUMBER_OF_DICE);
GameHeader.Write();

const int KEY_HEX_STRING_LENGTH = 64;

var firstDieChoiceDeterminator = new FirstDieChoiceDeterminator();
bool userGuessed = firstDieChoiceDeterminator.Run(dice, KEY_HEX_STRING_LENGTH);

var dieChoice = new DieChoice();
dieChoice.Run(userGuessed, dice);

var rollMaker = new RollMaker();
rollMaker.Run(userGuessed, dice, dieChoice, KEY_HEX_STRING_LENGTH);