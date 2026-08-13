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
        enum gameMode
        {
            ALL_HORIZONTAL,
            ALL_VERTICAL,
            ALL_DIAGONAL,
            ALL_LINES
        }        
        enum gameState
        {
            CONTINUE_GAME,
            END_GAME
        }
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

    }
}
