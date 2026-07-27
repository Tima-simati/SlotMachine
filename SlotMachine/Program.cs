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
            const int GUESSING_LOWERBOUND = 1;
            const int GUESSING_UPPERBOUND = 3;
            const int BET_ONE = 1;     //wager to just play horizontal middle line
            const int BET_THREE = 3;   //wager to play all three lines
            const int EARNING_FOR_ONE_LINE = 1;
            const int CHOOSE_ALL_HORIZONTAL = 1;
            const int CHOOSE_ALL_VERTICAL = 2;
            const int CHOOSE_ALL_DIAGONAL = 3;
            const int CHOOSE_ALL_LINES = 4;
            const int GRID_SIZE = 3;     //size of array, e.g. 3x3
            const int CENTRAL_LINE_INDEX_OF_GRID = GRID_SIZE / 2;
            const int LAST_INDEX_GRID = GRID_SIZE - 1;

            Console.WriteLine($"SLOTMACHINE ARCADE!!!\nYou start with a balance of {STARTING_CREDITS}$.\nCome and Play!");
            int playerWallet = STARTING_CREDITS;  //money balance of player

            int[,] slotMachineArray = new int[GRID_SIZE, GRID_SIZE];//array of slotMachine initialized
            int continueGame = 1;
            Random rng = new Random();

            int gameMode = 0; //var to choose one of the game modes for 3$ wager
            bool allLinesEnabled = false; //if all Lines are considered in a game
            int winCounter = 0; //counter for 3$ bets; if one line wins, you get wager back

            Console.WriteLine("One Game costs 1$. One WIN is considered, that only one matching row counts. \nYou win 1$ and your investment.");
            Console.WriteLine("You also have an option to bet 3$ and all lines will count then. High Risk, Higher Reward!");
            while (continueGame == 1)
            {
                Console.WriteLine($"Your current balance: {playerWallet}$");
                Console.WriteLine("Would you like to insert 1$ or 3$? Press 1 or 3");
                int bet = 0;
                int.TryParse(Console.ReadLine(), out bet);
                //check for wrong input for wager
                while (bet == BET_THREE && playerWallet < BET_THREE)
                {
                    Console.WriteLine($"Not sufficient funds! You only have {playerWallet}$ left. Insert 1$ by pressing 1.");
                    int.TryParse(Console.ReadLine(), out bet);
                    if (bet == BET_ONE)
                    {
                        break;
                    }
                }
                while (bet != BET_ONE || bet != BET_THREE)
                {
                    if (bet == BET_ONE || bet == BET_THREE)
                    {
                        break;
                    }
                    Console.WriteLine("Wrong bet inserted! Only press 1 or 3");
                    int.TryParse(Console.ReadLine(), out bet);
                }

                playerWallet -= bet;
                //filling the slot machine array with new values

                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        slotMachineArray[i, j] = rng.Next(GUESSING_LOWERBOUND, GUESSING_UPPERBOUND);

                    }
                }

                //1$ GAME: check for just central horizontal line matching
                if (bet == BET_ONE)
                {
                    bool allEqual = true;
                    for (int i = 0; i < LAST_INDEX_GRID; i++)
                    {

                        if (slotMachineArray[CENTRAL_LINE_INDEX_OF_GRID, i] != slotMachineArray[CENTRAL_LINE_INDEX_OF_GRID, i + 1])
                        {
                            allEqual = false;
                            Console.WriteLine("You Lose!");
                            break;
                        }
                    }
                    if (allEqual)
                    {
                        Console.WriteLine("You Won! Middle horizontal line was a match.");
                        playerWallet += EARNING_FOR_ONE_LINE + BET_ONE;
                    }
                }
                //3$ GAME: check for all horizontal, vertical and diagonal lines matching
                if (bet == BET_THREE)
                {
                    Console.WriteLine("You waged 3$. Would you like to play all horizontal lines or all vertical lines\nor all diagnoal lines or all lines?");
                    Console.WriteLine("Press (1) for all horizontal, (2) for all vertical,\n(3) for all diagonal, (4) for all lines.");
                    int.TryParse(Console.ReadLine(), out gameMode);
                    //check for wrong user input regarding input
                    while (gameMode != CHOOSE_ALL_HORIZONTAL || gameMode != CHOOSE_ALL_VERTICAL || gameMode != CHOOSE_ALL_DIAGONAL || gameMode != CHOOSE_ALL_LINES)
                    {
                        if (gameMode == CHOOSE_ALL_HORIZONTAL || gameMode == CHOOSE_ALL_VERTICAL || gameMode == CHOOSE_ALL_DIAGONAL || gameMode == CHOOSE_ALL_LINES)
                        {
                            break;
                        }
                        Console.WriteLine("game mode not found! Only press 1, 2, 3 or 4");
                        int.TryParse(Console.ReadLine(), out gameMode); ;
                    }
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
                                Console.WriteLine("You Won! A horizontal line was a match.");
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
                                Console.WriteLine($"No matching diagonal line!");
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            Console.WriteLine("You Won! A diagnoal line was a match.");
                            playerWallet += EARNING_FOR_ONE_LINE;
                            winCounter++;
                        }
                        allEqual = true;
                        for (int i = LAST_INDEX_GRID, j = 0; i > 0; i--, j++)
                        {
                            if (slotMachineArray[i, j] != slotMachineArray[i - 1, j + 1])
                            {
                                allEqual = false;
                                Console.WriteLine($"No matching diagonal line!");
                                break;
                            }
                        }
                        if (allEqual)
                        {
                            Console.WriteLine("You Won! A diagonal line was a match.");
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
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        Console.Write($"{slotMachineArray[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Credits left: {playerWallet}");

                //check if playerWallet is not 0; losing condition
                if (playerWallet <= 0)
                {
                    Console.WriteLine("You are out of credits.");
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
                Console.Clear();
            }
            //output text to show, how much was won or lost
            Console.WriteLine($"Game ended! Your balance is: {playerWallet}");
            if (playerWallet > STARTING_CREDITS)
            {
                Console.WriteLine($"You won {playerWallet - STARTING_CREDITS}.");
            }
            if (playerWallet < STARTING_CREDITS)
            {
                Console.WriteLine($"You lost {STARTING_CREDITS - playerWallet}.");
            }
        }
    }
}

