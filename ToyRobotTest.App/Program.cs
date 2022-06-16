using System;

using ToyRobotTest.Services;

namespace ToyRobotTest.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Toy Robot Test";

            Console.WriteLine("Toy Robot. Enter a command - PLACE X,Y,DIRECTION; MOVE; LEFT; RIGHT; REPORT;");

            ToyRobot robot = new ToyRobot();

            // In a production application, while(true) wouldn't be a great idea.
            while (true)
            {
                string command = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(command))
                {
                    break;
                }

                string result = robot.ProcessCommand(command);

                if (result != null)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}