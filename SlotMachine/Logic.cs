using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public static class Logic
    {
        public const int GRID_SIZE = 3;
        public const int CENTRAL_LINE_INDEX_OF_GRID = GRID_SIZE / 2;
        public const int LAST_INDEX_GRID = GRID_SIZE - 1;
        const int GUESSING_LOWERBOUND = 1;
        const int GUESSING_UPPERBOUND = 3;
        public static Random rng = new Random();
        public static int winCounter = 0; //counter for 3$ bets; if one line wins, you get wager back
       
        public static int[,] SpinSlotMachine()
        {
            int[,] array = new int[GRID_SIZE, GRID_SIZE];
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    array[i, j] = rng.Next(GUESSING_LOWERBOUND, GUESSING_UPPERBOUND);

                }
            }
            return array;
        }
        public static bool PlayOnlyCenterHorizontalLine(int[,] array)
        {
            bool allEqual = true;
            for (int i = 0; i < LAST_INDEX_GRID; i++)
            {
                if (array[CENTRAL_LINE_INDEX_OF_GRID, i] != array[CENTRAL_LINE_INDEX_OF_GRID, i + 1])
                {
                    allEqual = false;
                    return allEqual;
                }
            }
            if (allEqual)
            {
                return allEqual;
            }
            return false;
        }
        public static int PlayAllHorizontalLines(int[,] array)
        {
            int win = 0;
            bool allEqual = true;
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.LAST_INDEX_GRID; j++)
                {
                    if (array[i, j] != array[i, j + 1])
                    {
                        allEqual = false;
                        Console.WriteLine($"No matching line for row {i}!");
                        break;
                    }
                }
                if (allEqual)
                {
                    Console.WriteLine($"You Won! Row {i} line was a match.");
                    win += Constants.EARNING_FOR_ONE_LINE;                    
                }
                allEqual = true;
            }
            return win;
        }
        public static int PlayAllVerticalLines(int[,] array)
        {
            int win = 0;
            bool allEqual = true;
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.LAST_INDEX_GRID; j++)
                {
                    if (array[j, i] != array[j + 1, i])
                    {
                        allEqual = false;
                        Console.WriteLine($"No matching line for column {i}!");
                        break;
                    }
                }
                if (allEqual)
                {
                    Console.WriteLine("You Won! A vertical line was a match.");
                    win += Constants.EARNING_FOR_ONE_LINE;                    
                }
                allEqual = true;
            }
            return win;
        }
        public static int PlayAllDiagonalLines(int[,] array)
        {
            int win = 0;
            bool allEqual = true;
            for (int i = 0, j = 0; i < Constants.LAST_INDEX_GRID; i++, j++)
            {
                if (array[i, j] != array[i + 1, j + 1])
                {
                    allEqual = false;
                    Console.WriteLine($"No matching declining diagonal line!");
                    break;
                }
            }
            if (allEqual)
            {
                Console.WriteLine("You Won! Declining diagnoal line was a match.");
                win += Constants.EARNING_FOR_ONE_LINE;
               
            }
            allEqual = true;
            for (int i = Constants.LAST_INDEX_GRID, j = 0; i > 0; i--, j++)
            {
                if (array[i, j] != array[i - 1, j + 1])
                {
                    allEqual = false;
                    Console.WriteLine($"No matching inclining diagonal line!");
                    break;
                }
            }
            if (allEqual)
            {
                Console.WriteLine("You Won! Inclining diagonal line was a match.");
                win += Constants.EARNING_FOR_ONE_LINE;
            }
            return win;
        }
    }
}
