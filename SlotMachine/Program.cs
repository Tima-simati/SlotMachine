namespace SlotMachine
{
    internal class Program
    {

        private static int playOutcome(int a)
        {
            return a;
        }
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
            
            -first think about how you want your slotmachine to work
            fill array with random numbers
            output that array
            check if middle row is all the same
            output win /lose
            keep track of money
            implement more game modes (multiple lines / diagonals / se above description)
             */
            int playerWallet = 30;
            int rows = 3;
            int columns = 3;
            int[,] slotMachineArray = new int[rows, columns];
            const int EARNING_FOR_ONE_LINE = 1;
            int continueGame = 1;
            int endGame = 0;
            Random rng = new Random();
            const int GUESSING_LOWERBOUND = 1;
            const int GUESSING_UPPERBOUND = 9;
            while (continueGame == 1)
            {
                //filling the slot machine array with new values
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        slotMachineArray[i, j] = rng.Next(GUESSING_LOWERBOUND, GUESSING_UPPERBOUND);
                    }
                }
                //output
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Console.Write($"{slotMachineArray[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Do you want to continue?\nType 1 for continue,\ntype 0 to end the game.");
                continueGame = Convert.ToInt16(Console.ReadLine());
                 
            }
            Console.WriteLine($"Game ended! Your balance is: {playerWallet}");
        }
    }
}
