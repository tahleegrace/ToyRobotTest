namespace ToyRobotTest.Services
{
    /// <summary>
    /// A toy robot that can be moved on a table-top.
    /// </summary>
    public class ToyRobot
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public bool IsPlacedOnTable { get; set; }
        public Orientation Orientation { get; set; }
        public int TableWidth => 6;
        public int TableHeight => 6;

        /// <summary>
        /// Places the toy robot at the specified coordinates.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="orientation">The orientation of the the robot.</param>
        private void Place(int x, int y, Orientation orientation)
        {
            if (x < 0 || x >= TableWidth || y < 0 || y >= TableHeight)
            {
                return;
            }

            this.XPosition = x;
            this.YPosition = y;
            this.Orientation = orientation;
            this.IsPlacedOnTable = true;
        }

        /// <summary>
        /// Moves the toy robot 90 degrees to the left.
        /// </summary>
        private void MoveLeft()
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
        private void MoveRight()
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
    }
}