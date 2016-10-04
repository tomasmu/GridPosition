using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPosition
{
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate()
        {
            this.X = this.Y = 0;
        }

        public Coordinate(Coordinate coord)
        {
            this.X = coord.X;
            this.Y = coord.Y;
        }

        public void Step(Direction dir)
        {
            this.X += dir.X;
            this.Y += dir.Y;
        }
    }
}
