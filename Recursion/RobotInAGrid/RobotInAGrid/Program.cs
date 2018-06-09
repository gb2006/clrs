using System;
using System.Text;

namespace RobotInAGrid
{
    // Given a grid of r rows & c columns
    // A robot is at location 0,0 in the grid
    // Robot can move only right, or down.
    // Certain cells in the grid are marked with '*' and are
    // off limits for the robot. Design an algorithm for
    // robot to find a path from top left to bottom right
    class Program
    {
        static void Main(string[] args)
        {
            char[,] grid = new char[4, 4];

            // Try the case where no path is possible at all
            // grid[3, 3] = '*'; 

            // Try the case where right edge is blocked
            // grid[0, 3] = grid[1, 3] = grid[2, 3] = '*';

            // Try the case where top row is blocked
            // grid[0, 1] = grid[0, 2] = grid[0, 3] = '*';

            StringBuilder sb = new StringBuilder();

            if (FindPath(grid, 0, 0, sb))
            {
                Console.WriteLine("Found path: {0}", sb.ToString());
            }
            else
            {
                Console.WriteLine("No path found");
            }
        }

        // A path is described as a sequence of 'r', or 'd' steps.
        //
        static bool FindPath(char[,] grid, int r, int c, StringBuilder sb)
        {
            // GetUpperBound returns the last index so we need to add 1 to get 
            // actual count.
            int rows = grid.GetUpperBound(0) + 1;
            int cols = grid.GetUpperBound(1) + 1;

            if (r == rows || c == cols || grid[r,c] == '*')
            {
                // no path possible
                return false;
            }

            if (r == rows - 1 && c == cols - 1)
            {
                // found path
                return true;
            }

            // First try to find path to the right from where we are
            int len = sb.Length;
            sb.Append('r');

            if (FindPath(grid, r, c + 1, sb))
            {
                // If we found path, skip everything else and bubble up all the way.
                return true;
            }

            // We didn't find path to the right, so try down
            // but in order to do that, first we need to remove the previous 'r'
            // so reset sb to previous length
            sb.Length = len;
            sb.Append('d');

            if (FindPath(grid, r + 1, c, sb))
            {
                // found path
                return true;
            }

            // didn't find path, reset sb
            sb.Length = len;
            return false;
        }
    }
}
