using System;
using System.Collections.Generic;
using System.Text;
using static SlotMachine.Enums;

namespace SlotMachine
{
    public static class UI
    {

        public static void PrintWelcomeScreen(int credits)
        {
            Console.WriteLine($"SLOTMACHINE ARCADE!!!\nYou start with a balance of {credits}$.\nCome and Play!");
            Console.WriteLine("One Game costs 1$. One WIN is considered, that only one matching row counts. \nYou win 1$ and your investment.");
            Console.WriteLine("You also have an option to bet 3$ and play 3 other game modes. \nHigher Risk, Higher Reward!");
        }
        /// <summary>
        /// method to let player decide, how much he wants to make a wager
        /// </summary>
        /// <param name="balance"></param>
        /// <returns>size of bet</returns>
        public static int ChooseWager(int balance)
        {
            int bet = 0;
            const int BET_ONE = 1;  //for only center horizontal game
            const int BET_THREE = 3;//for all other game modes
            Console.WriteLine("Would you like to insert 1$ or 3$? Press 1 or 3");
            int.TryParse(Console.ReadLine(), out bet);
            //check for wrong input for wager
            while (bet == BET_THREE && balance < BET_THREE)
            {
                Console.WriteLine($"Not sufficient funds! You only have {balance}$ left. Insert 1$ by pressing 1.");
                int.TryParse(Console.ReadLine(), out bet);
                if (bet == BET_ONE)
                {
                    return bet;
                }
            }
            while (bet != BET_ONE || bet != BET_THREE)
            {
                if (bet == BET_ONE || bet == BET_THREE)
                {
                    return bet;
                }
                Console.WriteLine("Wrong bet inserted! Only press 1 or 3");
                int.TryParse(Console.ReadLine(), out bet);
            }
            return bet;
        }
        public static void ShowArray(int[,] array, int sizeOfArray)
        {
            for (int i = 0; i < sizeOfArray; i++)
            {
                for (int j = 0; j < sizeOfArray; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        public static void ShowCurrentBalance(int balance)
        {
            Console.WriteLine($"Your current balance: {balance}$");
        }
        public static void ShowCreditsLeft(int balance)
        {
            Console.WriteLine($"Credits left: {balance}");
        }
        public static void ShowCurrentGameLossFor1Dollar()
        {
            Console.WriteLine("You Lose!");
        }
        public static void ShowCurrentGameWinFor1Dollar()
        {
            Console.WriteLine("You Won! Center horizontal line was a match.");
        }
        public static int ShowGameModeOptions()
        {
            int chosen_gameMode = 0;
            Console.WriteLine("You waged 3$. Would you like to play all horizontal lines or all vertical lines\nor all diagnoal lines or all lines?");
            Console.Write($"Press {(int)Enums.GameMode.CHOOSE_ALL_HORIZONTAL} for {nameof(Enums.GameMode.CHOOSE_ALL_HORIZONTAL)}, {(int)Enums.GameMode.CHOOSE_ALL_VERTICAL} for {nameof(Enums.GameMode.CHOOSE_ALL_VERTICAL)},");
            Console.WriteLine($"\n{(int)Enums.GameMode.CHOOSE_ALL_DIAGONAL} for {nameof(Enums.GameMode.CHOOSE_ALL_DIAGONAL)}, {(int)Enums.GameMode.CHOOSE_ALL_LINES} for {nameof(Enums.GameMode.CHOOSE_ALL_LINES)}.");
            int.TryParse(Console.ReadLine(), out chosen_gameMode);
            //check for wrong user input regarding input
            while (!Enum.IsDefined(typeof(GameMode), chosen_gameMode))
            {
                Console.WriteLine("game mode not found! Only press 1, 2, 3 or 4");
                int.TryParse(Console.ReadLine(), out chosen_gameMode);
                if (Enum.IsDefined(typeof(GameMode), chosen_gameMode))
                {
                    return chosen_gameMode;
                }
            }
            return chosen_gameMode;
        }
        public static int AskPlayerToContinueGame(int continueGame)
        {
            //ask user to continue game
            Console.WriteLine("Do you want to continue?\nType 1 for continue,\ntype 0 to end the game.");
            int.TryParse(Console.ReadLine(), out continueGame);
            while (continueGame != 1 || continueGame != Constants.END_GAME)
            {
                if (continueGame == 1 || continueGame == Constants.END_GAME)
                {
                    return continueGame;
                }
                Console.WriteLine($"No option found for {continueGame} ! Only press 0 or 1");
                int.TryParse(Console.ReadLine(), out continueGame);
            }
            return continueGame;
        }
        public static void ShowZeroBalance()
        {
            Console.WriteLine("You are out of credits.");
        }
        public static void ShowEndScreen(int balance)
        {
            Console.WriteLine($"Game ended! Your balance is: {balance}");
        }
        public static void ShowWinnings(int balance, int credits_start)
        {
            Console.WriteLine($"You won {balance - credits_start}.");
        }
        public static void ShowLosses(int balance, int credits_start)
        {
            Console.WriteLine($"You lost {credits_start - balance}.");
        }
        public static void CleanScreen()
        {
            Console.Clear();
        }
    }
}
