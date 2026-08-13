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

    }
}
