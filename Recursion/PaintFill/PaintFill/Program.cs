using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintFill
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> neighbors = GetNeighbors(5, 5, new Point() { r = 2, c = 2 });
            foreach (var pt in neighbors)
            {
                Console.WriteLine("({0},{1})", pt.r, pt.c);
            }
        }

        // Implement paint fill function 
        // Given a screen (2-d array of colors), a point and a new color,
        // fill in the surrounding area until the color changes from the
        // original color

        struct Point { public int r; public int c; }

        static void PaintFill(byte[,] grid, byte newColor, int r, int c)
        {
            byte oldColor = grid[r, c];
            if (oldColor != newColor)
            {
                PaintFill(grid, oldColor, newColor, r, c);
            }
        }
        static void PaintFill(byte[,] grid, byte oldColor, byte newColor, int r, int c)
        {
            if (r < 0 || c < 0 || 
                r > grid.GetUpperBound(0) || c > grid.GetUpperBound(1))
            {
                return;
            }

            if (grid[r, c] == oldColor)
            {
                grid[r, c] = newColor;

                PaintFill(grid, oldColor, newColor, r - 1, c); // up
                PaintFill(grid, oldColor, newColor, r + 1, c); // down
                PaintFill(grid, oldColor, newColor, r, c - 1); // left
                PaintFill(grid, oldColor, newColor, r, c + 1); // right
            }
        }
    }
}
