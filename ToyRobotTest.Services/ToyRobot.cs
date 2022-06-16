using System;

namespace ToyRobotTest.Services
{
    /// <summary>
    /// A toy robot that can be moved on a table-top.
    /// </summary>
    public class ToyRobot
    {
        // Note: In a more complex situation (i.e. if you have multiple types of objects that can be on the table),
        // the toy robot and table would be in separate classes.
        private const string InvalidCommandText = "Invalid command";

        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsPlacedOnTable { get; private set; }
        public Orientation Orientation { get; private set; }
        public int TableWidth => 6;
        public int TableHeight => 6;

        /// <summary>
        /// Processes a PLACE command.
        /// </summary>
        private string ProcessPlaceCommand(string[] commandParts)
        {
            if (commandParts.Length < 2)
            {
                return "Please provide X and Y coordinates and an optional orientation";
            }

            string[] arguments = commandParts[1].Split(',');

            if (arguments.Length < 2)
            {
                return "Please provide X and Y coordinates";
            }

            int x;
            int y;

            if (!int.TryParse(arguments[0], out x) || !int.TryParse(arguments[1], out y))
            {
                return "Please provide numeric X and Y coordinates";
            }

            Orientation orientation = this.Orientation;

            if (!this.IsPlacedOnTable && arguments.Length < 3)
            {
                return "Please provide an orientation";
            }

            if (arguments.Length >= 3 && !Enum.TryParse(arguments[2], true, out orientation))
            {
                return "Please provide an orientation of North, South, East or West";
            }

            this.Place(x, y, orientation);

            return null;
        }

        /// <summary>
        /// Places the toy robot at the specified coordinates.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="orientation">The orientation of the the robot.</param>
        private void Place(int x, int y, Orientation? orientation)
        {
            if (x < 0 || x >= TableWidth || y < 0 || y >= TableHeight)
            {
                return;
            }

            this.X = x;
            this.Y = y;
            this.Orientation = orientation != null ? orientation.Value : this.Orientation;
            this.IsPlacedOnTable = true;
        }

        /// <summary>
        /// Moves the toy robot 90 degrees to the left.
        /// </summary>
        private void Left()
        {
            if (!this.IsPlacedOnTable)
            {
                return;
            }

            switch (this.Orientation)
            {
                case Orientation.North:
                    this.Orientation = Orientation.West;
                    break;
                case Orientation.West:
                    this.Orientation = Orientation.South;
                    break;
                case Orientation.South:
                    this.Orientation = Orientation.East;
                    break;
                case Orientation.East:
                    this.Orientation = Orientation.North;
                    break;
            }
        }

        /// <summary>
        /// Moves the toy robot 90 degrees to the right.
        /// </summary>
        private void Right()
        {
            if (!this.IsPlacedOnTable)
            {
                return;
            }

            switch (this.Orientation)
            {
                case Orientation.North:
                    this.Orientation = Orientation.East;
                    break;
                case Orientation.East:
                    this.Orientation = Orientation.South;
                    break;
                case Orientation.South:
                    this.Orientation = Orientation.West;
                    break;
                case Orientation.West:
                    this.Orientation = Orientation.North;
                    break;
            }
        }

        /// <summary>
        /// Moves the toy robot 1 unit forward in its current orientation.
        /// </summary>
        private string Move()
        {
            if (!this.IsPlacedOnTable)
            {
                return null;
            }

            int newX = this.X;
            int newY = this.Y;

            switch (this.Orientation)
            {
                case Orientation.North:
                    newY++;
                    break;
                case Orientation.East:
                    newX++;
                    break;
                case Orientation.South:
                    newY--;
                    break;
                case Orientation.West:
                    newX--;
                    break;
            }

            if (newX < 0 || newX >= TableWidth || newY < 0 || newY >= TableHeight)
            {
                return "Fallen off table";
            }

            this.X = newX;
            this.Y = newY;

            return null;
        }

        /// <summary>
        /// Reports the current position of the toy robot.
        /// </summary>
        private string Report()
        {
            return $"{this.X},{this.Y},{this.Orientation}";
        }

        /// <summary>
        /// Processes the specified command.
        /// </summary>
        /// <returns>The result of the specified command.</returns>
        public string ProcessCommand(string commandText)
        {
            string[] commandParts = commandText.Split(' ');

            if (commandParts.Length == 0)
            {
                return null;
            }

            Command commandName;

            if (!Enum.TryParse(commandParts[0], true, out commandName))
            {
                return InvalidCommandText;
            }

            switch (commandName)
            {
                case Command.Place:
                    return this.ProcessPlaceCommand(commandParts);
                case Command.Move:
                    return this.Move();
                case Command.Left:
                    this.Left();
                    return null;
                case Command.Right:
                    this.Right();
                    return null;
                case Command.Report:
                    return this.Report();
                default:
                    return null;
            }
        }
    }
}