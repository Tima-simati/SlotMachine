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


            Console.WriteLine($"SLOTMACHINE ARCADE!!!\nYou start with a balance of {STARTING_CREDITS}$.\nCome and Play!");
            int playerWallet = STARTING_CREDITS;  //money balance of player

            int[,] slotMachineArray = new int[GRID_SIZE, GRID_SIZE];//array of slotMachine initialized
            int continueGame = 1;
            Random rng = new Random();

            int gameMode = 0; //var to choose one of the game modes for 3$ wager
            bool allLinesEnabled = false; //if all Lines are considered in a game
            int winCounter = 0;

            Console.WriteLine("One Game costs 1$. One WIN is considered, that only one matching row counts. \nYou win 1$ and your investment.");
            Console.WriteLine("You also have an option to bet 3$ and all lines will count then. High Risk, Higher Reward!");
            while (continueGame == 1)
            {
                Console.WriteLine($"Your current balance: {playerWallet}$");
                Console.WriteLine("Would you like to insert 1$ or 3$? Press 1 or 3");
                int bet = 0;
                int.TryParse(Console.ReadLine(), out bet);
                //check for wrong input for wager
                while (bet != BET_ONE || bet != BET_THREE)
                {
                    if (bet == BET_ONE || bet == BET_THREE)
                    {
                        break;
                    }
                    /*This will fail unexpectidly with an exception here
                     * because a letter cannot be converted into an integer.
                     * How about using TryParse to verify that the input is a number ? 
                    */
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

                if (bet == BET_ONE)         //check for just middle horizontal line matching
                {
                    if (slotMachineArray[1, 0] == slotMachineArray[1, 1] && slotMachineArray[1, 0] == slotMachineArray[1, 2])
                    {
                        Console.WriteLine("You Won! Middle horizontal line was a match.");
                        playerWallet += EARNING_FOR_ONE_LINE + BET_ONE; //won wager back and 1$ for matching horizontal line
                    }
                    else
                    {
                        Console.WriteLine("You Lose!");
                    }
                }
                /*The general idea behind this exercise is to work with loops and make sure that you can calculate the wins dynamically, 
                 * in a way that if the grid changes from 3X3 to a 5X5 or a 7X7, no coding changes will be required, in order to calculate the wins.
                Currently, this will fail if the grid is anything else but 3X3.
                */
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
                        for (int i = 0; i < GRID_SIZE; i++)
                        {
                            if (slotMachineArray[i, 0] == slotMachineArray[i, 1] && slotMachineArray[i, 0] == slotMachineArray[i, 2])
                            {
                                Console.WriteLine("You Won! One horizontal line was a match.");
                                playerWallet += EARNING_FOR_ONE_LINE;
                                winCounter++;
                            }
                            else
                            {
                                Console.WriteLine("You Lose!");
                            }
                        }
                        if (allLinesEnabled == true)
                        {
                            gameMode = CHOOSE_ALL_VERTICAL; //for CHOOSE_ALL_LINES switch from horizontal check to vertical check
                        }
                    }
                    if (gameMode == CHOOSE_ALL_VERTICAL) //check for vertical line matching
                    {
                        for (int i = 0; i < GRID_SIZE; i++)
                        {
                            if (slotMachineArray[0, i] == slotMachineArray[1, i] && slotMachineArray[0, i] == slotMachineArray[2, i])
                            {
                                Console.WriteLine("You Won! One vertical line was a match.");
                                playerWallet += EARNING_FOR_ONE_LINE;
                                winCounter++;
                            }
                            else
                            {
                                Console.WriteLine("You Lose!");
                            }
                        }
                        if (allLinesEnabled == true)
                        {
                            gameMode = CHOOSE_ALL_DIAGONAL; //for CHOOSE_ALL_LINES switch from vertical check to horizontal check
                        }
                    }
                    if (gameMode == CHOOSE_ALL_DIAGONAL) //check for diagnoal line matching
                    {
                        if (slotMachineArray[0, 0] == slotMachineArray[1, 1] && slotMachineArray[1, 1] == slotMachineArray[2, 2])
                        {
                            Console.WriteLine("You Won! One diagonal line was a match.");
                            playerWallet += EARNING_FOR_ONE_LINE;
                            winCounter++;
                        }
                        if (slotMachineArray[0, 2] == slotMachineArray[1, 1] && slotMachineArray[1, 1] == slotMachineArray[2, 0])
                        {
                            Console.WriteLine("You Won! One diagonal line was a match.");
                            playerWallet += EARNING_FOR_ONE_LINE;
                            winCounter++;
                        }
                        else
                        {
                            Console.WriteLine("You Lose!");
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
                //ask user to continue
                Console.WriteLine("Do you want to continue?\nType 1 for continue,\ntype 0 to end the game.");
                int.TryParse(Console.ReadLine(), out continueGame);
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
