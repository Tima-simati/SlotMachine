using System;
using System.Diagnostics.Metrics;
using System.Threading.Channels;
using System.Globalization;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int playerWallet = Constants.STARTING_CREDITS;  //money balance of player
            int[,] slotMachineArray = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];//array of slotMachine initialized
            int continueGame = 1;
            int gameMode = 0; //var to choose one of the game modes for 3$ wager
            int prevBalance = 0; //variable to track getting wager back for 3$ game
            int bet = 0;

            UI.PrintWelcomeScreen(Constants.STARTING_CREDITS);
            while (continueGame == 1)
            {
                UI.ShowCurrentBalance(playerWallet);
                bet = UI.ChooseWager(playerWallet);
                playerWallet -= bet; //deduct wager from current player balance
                slotMachineArray = Logic.SpinSlotMachine(); //filling the slot machine array with new values

                //1$ GAME: check for just central horizontal line matching
                if (bet == Constants.BET_ONE)
                {
                    if (!Logic.PlayOnlyCenterHorizontalLine(slotMachineArray)) //lose center line game
                    {
                        UI.ShowCurrentGameLossFor1Dollar();
                    }
                    else // win center line game
                    {
                        UI.ShowCurrentGameWinFor1Dollar();
                        playerWallet += Constants.EARNING_FOR_ONE_LINE + Constants.BET_ONE;
                    }
                }
                prevBalance = playerWallet;
                //3$ GAME: check for all horizontal, vertical and diagonal lines matching
                if (bet == Constants.BET_THREE)
                {
                    gameMode = UI.ShowGameModeOptions();

                    switch (gameMode)
                    {
                        case (int)Enums.GameMode.CHOOSE_ALL_HORIZONTAL:
                            playerWallet += Logic.PlayAllHorizontalLines(slotMachineArray);
                            break;
                        case (int)Enums.GameMode.CHOOSE_ALL_VERTICAL:
                            playerWallet += Logic.PlayAllVerticalLines(slotMachineArray);
                            break;
                        case (int)Enums.GameMode.CHOOSE_ALL_DIAGONAL:
                            playerWallet += Logic.PlayAllDiagonalLines(slotMachineArray);
                            break;
                        case (int)Enums.GameMode.CHOOSE_ALL_LINES:
                            playerWallet += Logic.PlayAllHorizontalLines(slotMachineArray) + Logic.PlayAllVerticalLines(slotMachineArray) + Logic.PlayAllDiagonalLines(slotMachineArray);
                            break;
                    }
                }
                //giving back wager, if winCounter for 3$ game was more than 0
                if (playerWallet > prevBalance)
                {
                    playerWallet += bet;
                }
                //output array to Console
                UI.ShowArray(slotMachineArray, Constants.GRID_SIZE);
                UI.ShowCreditsLeft(playerWallet);

                //check if playerWallet is not 0; losing condition
                if (playerWallet <= 0)
                {
                    UI.ShowZeroBalance();
                    break;
                }
                //ask user to continue game
                continueGame = UI.AskPlayerToContinueGame(continueGame);
             
                if (continueGame == Constants.END_GAME)
                {
                    break;
                }
                UI.CleanScreen();
            }
            //output text to show, how much was won or lost
            UI.ShowEndScreen(playerWallet);
            if (playerWallet > Constants.STARTING_CREDITS)
            {
                UI.ShowWinnings(playerWallet, Constants.STARTING_CREDITS);
            }
            if (playerWallet < Constants.STARTING_CREDITS)
            {
                UI.ShowLosses(playerWallet, Constants.STARTING_CREDITS);
            }
        }
    }
}

