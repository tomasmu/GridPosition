using static System.Math;

namespace GridPosition
{
    class Position
    {
        public Coordinate Coordinate { get; set; }
        public Direction Direction { get; set; }

        public Position()
        {
            this.Coordinate = new Coordinate();
            this.Direction = new Direction();
        }

        public Position(Position pos)
        {
            this.Coordinate = new Coordinate(pos.Coordinate);
            this.Direction = new Direction(pos.Direction);
        }

        public void Rotate(int degrees)
        {
            Direction = RotationMatrixCW(degrees) * Direction;
            Direction.NormalizeDirection();
        }

        public void Move()
        {
            Coordinate.Step(Direction);
        }

        private int[,] RotationMatrixCW(int degrees)
        {
            double radians = degrees * PI / 180;
            int cosv = (int)Round(Cos(radians));
            int sinv = (int)Round(Sin(radians));
            return new int[,] {
                { cosv, sinv },
                { -sinv, cosv }
            };
        }

        public string GetCoordinateString() { return $"({Coordinate.X}, {Coordinate.Y})"; }
        public string GetDirectionString()  { return $"[{Direction.X}, {Direction.Y}]"; }
    }
}
