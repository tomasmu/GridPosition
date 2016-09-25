using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace GridPosition
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot("Bertil");
            robot.printPosition($"{robot.Name} was just created");
            robot.printDirection($"facing this direction");
            string commands = "DDA";    //bugg: DD = 45+45 ger fel direction :-(
            Console.WriteLine($"* new robot! i command thee: {commands}");
            Position finalPosition = robot.ExecuteCommands(robot.Position, commands);
            Console.WriteLine($"final pos {finalPosition.Coordinate.X}, {finalPosition.Coordinate.Y}");
        }
    }

    class Robot
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        private IDictionary<char, int> rotationDefinition = new Dictionary<char, int>
        {
            ['R'] = 90,
            ['L'] = -90,
            ['U'] = 180,
            ['D'] = 45
        };

        public Robot(string name)
        {
            this.Name = name;
            this.Position = new Position();
        }

        public Position ExecuteCommands(Position pos, string commands)
        {
            if (String.IsNullOrEmpty(commands))
            {
                return pos;
            }
            char cmd = commands[0];
            Position newPosition = new Position(pos);
            if (cmd == 'A')
            {
                newPosition.Move();
            }
            else
            {
                int angle = rotationDefinition[cmd];
                newPosition.Rotate(angle);
            }
            return ExecuteCommands(newPosition, commands.Substring(1));
        }

        public void printPosition(string message = "")
        {
            Console.WriteLine($"position: ({Position.Coordinate.X}, {Position.Coordinate.Y}), {message}");
        }

        public void printDirection(string message = "")
        {
            Console.WriteLine($"direction: [{ Position.Direction.X}, { Position.Direction.Y}], {message}");
        }
    }

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
            Direction = RotationMatrix(degrees) * Direction;
        }

        public void Move()
        {
            Coordinate.Step(Direction);
        }

        private int[,] RotationMatrix(int degrees)
        {
            double radians = degrees * PI / 180;
            int cosv = (int)Round(Cos(radians));
            int sinv = (int)Round(Sin(radians));
            return new int[,] {
                { cosv, sinv },
                { -sinv, cosv }
            };
        }
    }

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

    class Direction
    {
        //must be an array for matrix multiplication
        private int[] direction { get; set; }

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
            this.direction = Directions.North.direction;
        }

        public Direction(Direction dir)
        {
            this.direction = new int[2];
            this.X = dir.X;
            this.Y = dir.Y;
            Console.WriteLine($"new direction: {X}, {Y}");
        }

        public Direction(int x, int y)
        {
            this.direction = new int[] { x, y };
        }

        //recursive matrix multiplication
        public static Direction operator *(int[,] rot, Direction dir)
        {
            Direction newDir = Directions.None;
            for (int i = 0; i < dir.direction.Length; i++)
            {
                for (int j = 0; j < dir.direction.Length; j++)
                {
                    newDir.direction[i] += rot[i, j] * dir.direction[j];
                }
            }
            return newDir;
        }
    }

    static class Directions
    {
        public static Direction None  { get { return new Direction( 0,  0 ); } }
        public static Direction North { get { return new Direction( 0,  1 ); } }
        public static Direction South { get { return new Direction(-1,  0 ); } }
        public static Direction East  { get { return new Direction( 0, -1 ); } }
        public static Direction West  { get { return new Direction( 1,  0 ); } }
    }

}
