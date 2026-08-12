using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public static class UI
    {
      public static void WelcomeScreen(int credits)
        {
            Console.WriteLine($"SLOTMACHINE ARCADE!!!\nYou start with a balance of {credits}$.\nCome and Play!");
            Console.WriteLine("One Game costs 1$. One WIN is considered, that only one matching row counts. \nYou win 1$ and your investment.");
            Console.WriteLine("You also have an option to bet 3$ and play 3 other game modes. \nHigher Risk, Higher Reward!");
        }
        public static void ShowCurrentBalance(int balance)
        {
            Console.WriteLine($"Your current balance: {balance}$");
        }
        public static void ShowZeroBalance()
        {
            Console.WriteLine("You are out of credits.");
        }
        public static void ShowEndScreen(int balance)
        {
            Console.WriteLine($"Game ended! Your balance is: {balance}");
        }
    }
}
