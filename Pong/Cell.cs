using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Cell : IEqualityComparer<Cell>, IEquatable<Cell>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public bool Drew { get; set; }
        public Cell(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            Drew = false;
            Size = 40;
        }
        public Cell():
            this(0,0)
        {
            Size = 40;
        }
        public List<Cell> GetNeighbors()
        {
            List<Cell> cells = new List<Cell>();

            cells.Add(new Cell(this.X + Size, this.Y + Size));
            cells.Add(new Cell(this.X + Size, this.Y));
            cells.Add(new Cell(this.X , this.Y + Size));
            cells.Add(new Cell(this.X, this.Y - Size));

            cells.Add(new Cell(this.X - Size, this.Y - Size));
            cells.Add(new Cell(this.X - Size, this.Y));
            cells.Add(new Cell(this.X - Size, this.Y + Size));
            cells.Add(new Cell(this.X + Size, this.Y - Size));

            return cells;
        }

        public bool Equals(Cell x, Cell y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Cell obj)
        {
            if (this.X == obj.X && this.Y == obj.Y)
            {
                return 0;
            }
            else return 1;
        }

        public bool Equals(Cell other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
