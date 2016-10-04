using System;
using System.Collections.Generic;
using static System.Math;

namespace GridPosition
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot("Bertil");
            robot.printPosition("just created: ");
            string commands = "AUARRxRRArrrrArrrrAAllAAllAUlAlUAAA";   //testing 45 degree turns, U-turns, unknowns, etc
            //string commands = "ARALARAxLARALARA";
            Console.WriteLine($"* robot! i command thee: {commands}");
            robot.Execute(commands);
            robot.printPosition("final position: ");

            WaitForKeyIfDebugging();
        }

        private static void WaitForKeyIfDebugging()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.Write("Press any key to exit . . . ");
                Console.ReadKey();
            }
        }

        public static bool IsDebug
        {
            get
            {
                bool debug = false;
#if DEBUG       //false if Release
                debug = true;
#endif
                return debug;
            }
        }
    }
}
