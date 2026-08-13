using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public static class Constants
    {
        public const int STARTING_CREDITS = 10;
        public const int END_GAME = 0;
        public const int GRID_SIZE = 3;
        public const int CENTRAL_LINE_INDEX_OF_GRID = GRID_SIZE / 2;
        public const int LAST_INDEX_GRID = GRID_SIZE - 1;
        public const int GUESSING_LOWERBOUND = 1;   //lower bound of range of random number
        public const int GUESSING_UPPERBOUND = 3;   //upper bound of range of random number
        public const int BET_ONE = 1;  //for only center horizontal game
        public const int BET_THREE = 3;//for all other game modes
        public const int EARNING_FOR_ONE_LINE = 1;
    }
}
