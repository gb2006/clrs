using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueensProblem
{
    // Problem:
    // Given a chess board of size N x N (N > 2)
    // Arrange N queens on the board in such a way that none of them
    // can kill each other. A queen can kill another queen if both
    // of them share same row, or same column or are diagonally across
    // from each other (not just main diagon but any)
    //
    class Program
    {
        public class Solver
        {
            // Dictionary maps a given row index to a queen's position in a column.
            // For all intents and purposes this dictionary is the chess board.
            private Dictionary<int, int> queenPositions;
            private int queens;

            public Solver(int n)
            {
                queenPositions = new Dictionary<int, int>(n);
                queens = n;
            }

            public void Solve()
            {
                Solve(0);
            }

            private void Solve(int currRow)
            {
                if (currRow == queens)
                {
                    // Successful in placement of all queens
                    PrintBoard();
                }

                for (int currCol = 0; currCol < queens; currCol++)
                {
                    if (CanPlace(currRow, currCol))
                    {
                        queenPositions.Add(currRow, currCol);
                        Solve(currRow + 1);
                        queenPositions.Remove(currRow);
                    }
                }
            }

            // Print current chess board
            private void PrintBoard()
            {
                for (int i = 0; i < queens; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append('.', queens);

                    // Find queen in row i
                    int j = queenPositions[i];
                    sb[j] = '*';

                    Console.WriteLine(sb.ToString());
                }

                for (int i = 0; i <= queens; i++)
                {
                    Console.Write('-');
                }
                Console.WriteLine();
            }

            // Can a queen be placed at a given location
            private bool CanPlace(int targetRow, int targetCol)
            {
                if (targetRow >= queens || targetCol >= queens)
                {
                    return false;
                }

                foreach (int usedRow in queenPositions.Keys)
                {
                    int usedCol = queenPositions[usedRow];

                    // if same row, or same column, 
                    // or if are diagonal to each other,
                    // return false
                    if (usedCol == targetCol || usedRow == targetRow ||
                        (Math.Abs(targetCol - usedCol) == Math.Abs(targetRow - usedRow)))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        static void Main(string[] args)
        {
            new Solver(8).Solve();
        }

        // Go row by row
        // For each row, go column by column
        //  In current row & current column, can a queen be placed?
        //  If yes Put a queen in current row, current column and 
        //  solve remaining queens recursively.
        //  If not, move to next column.
        //  If out of all columns, no placement possible.
        //  
    }
}
