using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPosition
{
    class Robot
    {
        public string Name { get; set; }
        public Position Position { get; set; }

        public Robot(string name)
        {
            this.Name = name;
            this.Position = new Position();
        }

        public void Execute(string commands)
        {
            Position = ExecuteCommands(Position, commands);
        }

        private Position ExecuteCommands(Position pos, string commands)
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
                if (Program.IsDebug)
                    Console.WriteLine($"cmd {cmd} -> new coord: " + newPosition.GetCoordinateString());
            }
            else
            {
                int angle;
                if (DirectionData.rotationDefinition.TryGetValue(cmd, out angle))
                {
                    newPosition.Rotate(angle);
                    if (Program.IsDebug)
                        Console.WriteLine($"cmd {cmd} -> new dir: " + newPosition.GetDirectionString());
                }
                else
                {
                    Console.WriteLine($"cmd '{cmd}' not found");
                }
            }
            return ExecuteCommands(newPosition, commands.Substring(1));
        }

        public void printPosition(string message = "")
        {
            Console.WriteLine($"{message}{Name}: coordinates {Position.GetCoordinateString()}, direction {Position.GetDirectionString()}");
        }
    }
}
