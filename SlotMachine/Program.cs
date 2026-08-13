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
            const int STARTING_CREDITS = 10;
            const int END_GAME = 0;
            //declaring boundaries of numbers displayed in slotMachine

            const int BET_ONE = 1;  //for only center horizontal game
            const int BET_THREE = 3;//for all other game modes
            const int EARNING_FOR_ONE_LINE = 1;
            const int CHOOSE_ALL_HORIZONTAL = 1;
            const int CHOOSE_ALL_VERTICAL = 2;
            const int CHOOSE_ALL_DIAGONAL = 3;
            const int CHOOSE_ALL_LINES = 4;
            const int GRID_SIZE = 3;     //size of array, e.g. 3x3
            const int LAST_INDEX_GRID = GRID_SIZE - 1;

            var gameMode_choices = new List<int> { CHOOSE_ALL_HORIZONTAL, CHOOSE_ALL_VERTICAL, CHOOSE_ALL_DIAGONAL, CHOOSE_ALL_LINES };

            int playerWallet = Constants.STARTING_CREDITS;  //money balance of player
            int[,] slotMachineArray = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];//array of slotMachine initialized
            int continueGame = 1;
            
            int gameMode = 0; //var to choose one of the game modes for 3$ wager
            bool allLinesEnabled = false; //if all Lines are considered in a game
            int winCounter = 0; //counter for 3$ bets; if one line wins, you get wager back

            UI.PrintWelcomeScreen(Constants.STARTING_CREDITS);
            while (continueGame == 1)
            {
                UI.ShowCurrentBalance(playerWallet);
                int bet = UI.ChooseWager(playerWallet);
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
                //3$ GAME: check for all horizontal, vertical and diagonal lines matching
                if (bet == Constants.BET_THREE)
                {
                    gameMode = UI.ShowGameModeOptions();                    
                 
                    if (gameMode == CHOOSE_ALL_LINES)   //check for all lines
                    {
                        gameMode = CHOOSE_ALL_HORIZONTAL; //since all lines to play are chosen; switch to horizontal lines check
                        allLinesEnabled = true;
                    }
                    if (gameMode == CHOOSE_ALL_HORIZONTAL) //check for any of the horizontal lines winning
                    {
                        bool allEqual = true;
                        for (int i = 0; i < GRID_SIZE; i++)
                        {
                            for (int j = 0; j < LAST_INDEX_GRID; j++)
                            {
                                if (slotMachineArray[i, j] != slotMachineArray[i, j + 1])
                                {
                                    allEqual = false;
                                    Console.WriteLine($"No matching line for row {i}!");
                                    break;
                                }
                            }
                            if (allEqual)
                            {
                                Console.WriteLine($"You Won! Row {i} line was a match.");
                                playerWallet += EARNING_FOR_ONE_LINE;
                                winCounter++;
                            }
                            allEqual = true;
                        }
                        if (allLinesEnabled == true)
                        {
                            gameMode = CHOOSE_ALL_VERTICAL; //for CHOOSE_ALL_LINES switch from horizontal check to vertical check
                        }
                    }
                    if (gameMode == CHOOSE_ALL_VERTICAL) //check for vertical line matching
                    {
                        bool allEqual = true;
                        for (int i = 0; i < GRID_SIZE; i++)
                        {
                            for (int j = 0; j < LAST_INDEX_GRID; j++)
                            {
                                if (slotMachineArray[j, i] != slotMachineArray[j + 1, i])
                                {
                                    allEqual = false;
                                    Console.WriteLine($"No matching line for column {i}!");
                                    break;
                                }
                            }
                            if (allEqual)
                            {
                                Console.WriteLine("You Won! A vertical line was a match.");
                                playerWallet += EARNING_FOR_ONE_LINE;
                                winCounter++;
                            }
                            allEqual = true;
                        }
                        if (allLinesEnabled == true)
                        {
                            gameMode = CHOOSE_ALL_DIAGONAL; //for CHOOSE_ALL_LINES switch from vertical check to horizontal check
                        }
                    }
                    if (gameMode == CHOOSE_ALL_DIAGONAL) //check for diagnoal line matching
                    {
                        bool allEqual = true;
                        for (int i = 0, j = 0; i < LAST_INDEX_GRID; i++, j++)
                        {
                            if (slotMachineArray[i, j] != slotMachineArray[i + 1, j + 1])
                            {
                                allEqual = false;
                                Console.WriteLine($"No matching declining diagonal line!");
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            Console.WriteLine("You Won! Declining diagnoal line was a match.");
                            playerWallet += EARNING_FOR_ONE_LINE;
                            winCounter++;
                        }
                        allEqual = true;
                        for (int i = LAST_INDEX_GRID, j = 0; i > 0; i--, j++)
                        {
                            if (slotMachineArray[i, j] != slotMachineArray[i - 1, j + 1])
                            {
                                allEqual = false;
                                Console.WriteLine($"No matching inclining diagonal line!");
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            Console.WriteLine("You Won! Inclining diagonal line was a match.");
                            playerWallet += EARNING_FOR_ONE_LINE;
                            winCounter++;
                        }
                    }
                }
                //giving back wager, if winCounter for 3$ game was more than 0
                if (winCounter > 0)
                {
                    playerWallet += bet;
                }
                winCounter = 0; //reset Counter for win to 0
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
                Console.WriteLine("Do you want to continue?\nType 1 for continue,\ntype 0 to end the game.");
                int.TryParse(Console.ReadLine(), out continueGame);
                while (continueGame != 1 || continueGame != END_GAME)
                {
                    if (continueGame == 1 || continueGame == END_GAME)
                    {
                        break;
                    }
                    Console.WriteLine($"No option found for {continueGame} ! Only press 0 or 1");
                    int.TryParse(Console.ReadLine(), out continueGame);
                }
                if (continueGame == END_GAME)
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

