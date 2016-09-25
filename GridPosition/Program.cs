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
            string commands = "RRRRLLLLARA";
            Console.WriteLine($"* new robot! i command thee: {commands}");
            robot.ExecuteCommand(commands);
        }
    }

    class Robot
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        private Dictionary<string, int> rotationDefinition = new Dictionary<string, int>
        {
            ["R"] = 90,
            ["L"] = -90
        };

        public Robot(string name)
        {
            this.Name = name;
            this.Position = new Position();
        }

        public void ExecuteCommand(string command)
        {
            for (int i = 0; i < command.Length; i++)
            {
                var cmd = command[i].ToString();
                switch (cmd)
                {
                    case "R":
                    case "L":
                        int angle;
                        if (rotationDefinition.TryGetValue(cmd, out angle)) { Position.Rotate(angle); }
                        else { Console.WriteLine($"index for {cmd} was not found"); }
                        break;
                    case "A":
                        Position.Move();
                        break;
                    default:
                        cmd = cmd + " = unknown command";
                        break;
                }
                printPosition($"cmd {cmd}");
            }
            Console.WriteLine($"done! exclaimed {this.Name} joyfully");
        }

        public void ExecuteCommand(char command)
        {
            ExecuteCommand(command.ToString());
        }

        public void printPosition(string text)
        {
            Console.WriteLine($"hännä: ({Position.Coordinate.X}, {Position.Coordinate.Y}), dir ({Position.Direction.X}, {Position.Direction.Y}), {text}");
        }
        public void printPosition()
        {
            printPosition(":-)");
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

        public void Rotate(int degrees)
        {
            RotationMatrix rotate = new RotationMatrix(degrees);
            Direction = Direction * rotate;
        }

        public void Move()
        {
            Coordinate.Step(Direction);
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

        public void Step(Direction dir)
        {
            this.X += dir.X;
            this.Y += dir.Y;
        }
    }

    class Direction
    {
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
            this.direction = new int[] { 0, 1 };    //north
        }

        public Direction(int[] dirType) //Direction(DirectionType dirType)?
        {
            this.direction = dirType;
        }

        public static Direction operator *(Direction dir, RotationMatrix rot)
        {
            Direction newDir = new Direction(new int[] { 0, 0 });   //new Direction(DirectionTypes.HeadNowhere)?
            for (int i = 0; i < dir.direction.Length; i++)
            {
                for (int j = 0; j < dir.direction.Length; j++)
                {
                    newDir.direction[i] += rot.matrix[i, j] * dir.direction[j];
                }
            }
            return newDir;
        }

    }

    //class DirectionTypes
    //{
    //    public static readonly int[] HeadNowhere = new int[] { 0, 0 };
    //    public static readonly int[] HeadNorth = new int[] {  0,  1 };
    //    public static readonly int[] HeadSouth = new int[] { -1,  0 };
    //    public static readonly int[] HeadEast  = new int[] {  0, -1 };
    //    public static readonly int[] HeadWest  = new int[] {  1,  0 };
    //}

    class RotationMatrix
    {
        public int[,] matrix { get; set; }
        public RotationMatrix(int degrees)
        {
            double radians = degrees * PI / 180;
            int cosv = (int)Round(Cos(radians));
            int sinv = (int)Round(Sin(radians));
            matrix = new int[,] {
                { cosv, sinv },
                { -sinv, cosv }
            };
        }
    }
}
