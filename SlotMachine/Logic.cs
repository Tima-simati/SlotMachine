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
        public static void PlayAllHorizontalLines(int[,] array, int balance)
        {
          
        }
        public static void PlayAllVerticalLines(int[,] array, int balance)
        {

        }
        public static void PlayAllDiagonalLines(int[,] array, int balance)
        {

        }
    }
}
