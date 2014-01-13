using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    class Board
    {
        public static List<Cell> Iterate(List<Cell> cells)
        {
            List<Cell> newCells = new List<Cell>();
            List<Cell> allNeighbors = new List<Cell>();

            foreach (var cell in cells)
            {
                int neighbors = 0;
                allNeighbors.AddRange(cell.GetNeighbors());
                foreach (var neighbor in cell.GetNeighbors())
                {
                    if (cells.Contains(neighbor))
                    {
                        neighbors++;
                    }
                }

                if (neighbors == 2 || neighbors == 3)
                {
                    newCells.Add(cell);
                }
            }

            foreach (var cell in allNeighbors)
            {
                if (allNeighbors.Count(c => c.Equals(cell)) == 3 && !newCells.Contains(cell))
                {
                    newCells.Add(cell);
                }
            }

            return newCells;
        }
    }
}
