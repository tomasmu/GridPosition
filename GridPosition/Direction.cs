using System.Collections.Generic;

using static System.Math;

namespace GridPosition
{
    class Direction
    {
        //must be an array for matrix multiplication
        private int[] direction { get; set; }

        //ease of access
        public int X
        {
            get { return this.direction[0]; }
            set { this.direction[0] = value; }
        }

        public int Y
        {
            get { return this.direction[1]; }
            set { this.direction[1] = value; }
        }

        public Direction()
        {
            this.direction = DirectionData.North.direction;
        }

        public Direction(int x, int y)
        {
            this.direction = new int[] { x, y };
        }

        public Direction(Direction dir) : this(dir.X, dir.Y)
        {
        }

        //for multiplying rotation matrix with direction
        public static Direction operator *(int[,] rot, Direction dir)
        {
            Direction newDir = DirectionData.None;
            int size = dir.direction.Length;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    newDir.direction[i] += rot[i, j] * dir.direction[j];
            }
            return newDir;
        }

        //correction: if |step| > maxStep, set to (+\-)maxStep
        //needed because of integer arithmetic and 45 degree turns
        internal void NormalizeDirection()
        {
            for (int i = 0; i < this.direction.Length; i++)
                this.direction[i] = NormalizeStep(this.direction[i]);
        }

        private int NormalizeStep(int dir)
        {
            if (Abs(dir) > DirectionData.maxStep)
                return Sign(dir) * DirectionData.maxStep;
            else
                return dir;
        }
    }

    static class DirectionData
    {
        public static int maxStep = 3;

        public static Direction None { get { return new Direction(0, 0); } }
        public static Direction North { get { return new Direction(0, maxStep); } }
        public static Direction South { get { return new Direction(0, -maxStep); } }
        public static Direction East { get { return new Direction(maxStep, 0); } }
        public static Direction West { get { return new Direction(-maxStep, 0); } }

        public static IDictionary<char, int> rotationDefinition = new Dictionary<char, int>
        {
            ['R'] = 90,
            ['L'] = -90,
            ['U'] = 180,
            ['r'] = 45,
            ['l'] = -45
        };
    }
}
