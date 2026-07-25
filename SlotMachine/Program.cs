using System;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Design a game where the user can play a make-believe slot machine. The user will be asked to make a wager to play various lines in a 3 x 3 grid. 
             * They can play center line, all three horizontal lines, all vertical lines and diagonals.
            For instance the user can enter $3 dollars and play all three horizontal lines. If the top line hits a winning combination, they earn $1 dollar for that line.

            rocket Tips: The mention of a grid here should be a dead giveaway that you need a 2D array. 
            You will also need functionality that can check a horizontal line, a vertical line and a diagonal. 
            Depending on the number of lines they play, you may need to execute all three of these statements one or multiple times to look for winning lines. 
            If they are playing three lines, you would call your horizontal line check function three times... one for the top row, one for the center row and one for the bottom row. 
            Each of these row checking algorithms will then need to look for winning combinations. The result is then dumped into the player’s money total. 
            As for the mechanism to determine what the wheels produce per spin, use a random number generating function.
        
             */
            const int STARTING_CREDITS = 10;
            Console.WriteLine($"SLOTMACHINE ARCADE!!!\nYou start with a balance of {STARTING_CREDITS}$.\nCome and Play!");
            int playerWallet = STARTING_CREDITS;  //money balance of player

            int rows = 3;           //rows of array
            int columns = 3;        //columns of array
            int[,] slotMachineArray = new int[rows, columns];//array of slotMachine initialized

            int continueGame = 1;
            const int END_GAME = 0;

            Random rng = new Random(); //declaring boundaries of numbers displayed in slotMachine
            const int GUESSING_LOWERBOUND = 1;
            const int GUESSING_UPPERBOUND = 3;

            const int BET_ONE = 1;     //wager to just play horizontal middle line
            const int BET_THREE = 3;   //wager to play all three lines
            const int EARNING_FOR_ONE_LINE = 1;

            int gameMode = 0; //var to choose one of the game modes for 3$ wager
            const int CHOOSE_ALL_HORIZONTAL = 1;
            const int CHOOSE_ALL_VERTICAL = 2;
            const int CHOOSE_ALL_DIAGONAL = 3;
            const int CHOOSE_ALL_LINES = 4;
            bool allLinesEnabled = false;

            int winCounter = 0;

            Console.WriteLine("One Game costs 1$. One WIN is considered, that only one matching row counts. \nYou win 1$ and your investment.");
            Console.WriteLine("You also have an option to bet 3$ and all lines will count then. High Risk, Higher Reward!");
            while (continueGame == 1)
            {
                Console.WriteLine("Would you like to insert 1$ or 3$? Press 1 or 3");
                int bet = Convert.ToInt16(Console.ReadLine());
                //check for wrong input for wager
                while (bet != BET_ONE || bet != BET_THREE)
                {
                    if (bet == BET_ONE || bet == BET_THREE)
                    {
                        break;
                    }
                    Console.WriteLine("Wrong bet inserted! Only press 1 or 3");
                    bet = Convert.ToInt16(Console.ReadLine());
                }
                playerWallet -= bet;
                //filling the slot machine array with new values
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
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
                if (bet == BET_THREE)
                {
                    Console.WriteLine("You waged 3$. Would you like to play all horizontal lines or all vertical lines\nor all diagnoal lines or all lines?");
                    Console.WriteLine("Press (1) for all horizontal, (2) for all vertical,\n(3) for all diagonal, (4) for all lines.");
                    gameMode = Convert.ToInt16(Console.ReadLine());
                    //check for wrong user input regarding input
                    while (gameMode != CHOOSE_ALL_HORIZONTAL || gameMode != CHOOSE_ALL_VERTICAL || gameMode != CHOOSE_ALL_DIAGONAL || gameMode != CHOOSE_ALL_LINES)
                    {
                        if (gameMode == CHOOSE_ALL_HORIZONTAL || gameMode == CHOOSE_ALL_VERTICAL || gameMode == CHOOSE_ALL_DIAGONAL || gameMode == CHOOSE_ALL_LINES)
                        {
                            break;
                        }
                        Console.WriteLine("game mode not found! Only press 1, 2, 3 or 4");
                        gameMode = Convert.ToInt16(Console.ReadLine());
                    }
                    if (gameMode == CHOOSE_ALL_LINES)   //check for all lines
                    {
                        gameMode = CHOOSE_ALL_HORIZONTAL; //since all lines to play are chosen; switch to horizontal lines check
                        allLinesEnabled = true;
                    }
                    if (gameMode == CHOOSE_ALL_HORIZONTAL) //check for any of the horizontal lines winning
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            if (slotMachineArray[i, 0] == slotMachineArray[i, 1] && slotMachineArray[i, 0] == slotMachineArray[i, 2])
                            {
                                Console.WriteLine("You Won! One horizontal line was a match.");
                                playerWallet += EARNING_FOR_ONE_LINE;
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
                        for (int i = 0; i < columns; i++)
                        {
                            if (slotMachineArray[0, i] == slotMachineArray[1, i] && slotMachineArray[0, i] == slotMachineArray[2, i])
                            {
                                Console.WriteLine("You Won! One vertical line was a match.");
                                playerWallet += EARNING_FOR_ONE_LINE;
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
                        }
                        if (slotMachineArray[0, 2] == slotMachineArray[1, 1] && slotMachineArray[1, 1] == slotMachineArray[2, 0])
                        {
                            Console.WriteLine("You Won! One diagonal line was a match.");
                            playerWallet += EARNING_FOR_ONE_LINE;
                        }
                        else
                        {
                            Console.WriteLine("You Lose!");
                        }
                    }

                }
                //output array to Console
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
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
                continueGame = Convert.ToInt16(Console.ReadLine());
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
