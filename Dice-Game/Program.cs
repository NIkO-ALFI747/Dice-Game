using Dice_Game.Dice;
using Dice_Game.DieChoice;
using Dice_Game.Roll;
using Dice_Game.UI;

var dice = new Dice(args);
GameHeader.Write();

var firstDieChoiceDeterminator = new FirstDieChoiceDeterminator(0, 1);
bool userGuessed = firstDieChoiceDeterminator.Run(dice, "Try to guess my selection.");

var dieChoice = new DieChoice();
dieChoice.Run(userGuessed, dice);

var rollMaker = new RollMaker();
rollMaker.Run(userGuessed, dice, dieChoice);