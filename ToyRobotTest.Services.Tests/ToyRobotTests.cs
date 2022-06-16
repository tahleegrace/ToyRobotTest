using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using ToyRobotTest.Services;

namespace ToyRobotTest.Services.Tests
{
    [TestClass]
    public class ToyRobotTests
    {
        [TestMethod]
        public void ShouldIgnoreEmptyCommands()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("");

            Assert.IsNull(result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldShowInvalidCommandMessageWhenCommandIsUnsuported()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("WALK 1,2");

            Assert.AreEqual("Invalid command", result);
        }

        #region Place Command
        [TestMethod]
        public void ShouldIgnorePlaceCommandWhenXGreaterThanTableWidth()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 6,1,NORTH");

            Assert.IsNull(result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldIgnorePlaceCommandWhenXLessThanTableWidth()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE -1,1,NORTH");

            Assert.IsNull(result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldIgnorePlaceCommandWhenYGreaterThanTableHeight()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 1,6,NORTH");

            Assert.IsNull(result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldIgnorePlaceCommandWhenYLessThanTableHeight()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 1,-1,NORTH");

            Assert.IsNull(result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldShowErrorMessageForPlaceCommandWhenOnlyTheCommandNameIsProvided()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE");

            Assert.AreEqual("Please provide X and Y coordinates and an orientation if the robot is not on the table yet", result);
        }

        [TestMethod]
        public void ShouldShowErrorMessageForPlaceCommandWhenCoordinatesIncomplete()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 1");

            Assert.AreEqual("Please provide X and Y coordinates and an orientation if the robot is not on the table yet", result);
        }

        [TestMethod]
        public void ShouldShowErrorMessageForPlaceCommandWhenCoordinatesNotValidNumbers()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE A,B");

            Assert.AreEqual("Please provide numeric X and Y coordinates", result);
        }

        [TestMethod]
        public void ShouldShowErrorMessageForPlaceCommandOnFirstCallAndOrientationNotProvided()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 1,2");

            Assert.AreEqual("Please provide an orientation", result);
        }

        [TestMethod]
        public void ShouldShowErrorMessageForPlaceCommandWhenOrientationInvalid()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 1,2,TEST");

            Assert.AreEqual("Please provide an orientation of North, South, East or West", result);
        }

        [TestMethod]
        public void ShouldSetCorrectCoordinatesWhenPlaceCommandCalledWithValidArguments()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("PLACE 1,2,NORTH");

            Assert.IsNull(result);
            Assert.AreEqual(1, robot.X);
            Assert.AreEqual(2, robot.Y);
            Assert.AreEqual(Orientation.North, robot.Orientation);
            Assert.IsTrue(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldSetCorrectCoordinatesWhenPlaceCommandIsCalledTwice()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,2,NORTH");
            robot.ProcessCommand("PLACE 2,1");

            Assert.AreEqual(2, robot.X);
            Assert.AreEqual(1, robot.Y);
            Assert.AreEqual(Orientation.North, robot.Orientation);
            Assert.IsTrue(robot.IsPlacedOnTable);
        }
        #endregion

        #region Left Command
        [TestMethod]
        public void ShouldSetCorrectOrientationOnLeftWhenNorth()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,NORTH");
            robot.ProcessCommand("LEFT");

            Assert.AreEqual(Orientation.West, robot.Orientation);
        }

        [TestMethod]
        public void ShouldSetCorrectOrientationOnLeftWhenWest()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,West");
            robot.ProcessCommand("LEFT");

            Assert.AreEqual(Orientation.South, robot.Orientation);
        }

        [TestMethod]
        public void ShouldSetCorrectOrientationOnLeftWhenSouth()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,South");
            robot.ProcessCommand("LEFT");

            Assert.AreEqual(Orientation.East, robot.Orientation);
        }

        [TestMethod]
        public void ShouldSetCorrectOrientationOnLeftWhenEast()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,East");
            robot.ProcessCommand("LEFT");

            Assert.AreEqual(Orientation.North, robot.Orientation);
        }
        #endregion

        #region Right Command
        [TestMethod]
        public void ShouldSetCorrectOrientationOnRightWhenNorth()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,NORTH");
            robot.ProcessCommand("RIGHT");

            Assert.AreEqual(Orientation.East, robot.Orientation);
        }

        [TestMethod]
        public void ShouldSetCorrectOrientationOnRightWhenWest()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,West");
            robot.ProcessCommand("RIGHT");

            Assert.AreEqual(Orientation.North, robot.Orientation);
        }

        [TestMethod]
        public void ShouldSetCorrectOrientationOnRightWhenSouth()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,South");
            robot.ProcessCommand("RIGHT");

            Assert.AreEqual(Orientation.West, robot.Orientation);
        }

        [TestMethod]
        public void ShouldSetCorrectOrientationOnRightWhenEast()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,1,East");
            robot.ProcessCommand("RIGHT");

            Assert.AreEqual(Orientation.South, robot.Orientation);
        }
        #endregion

        #region Move Command
        [TestMethod]
        public void ShouldIgnoreMoveCommandWhenRobotIsNotOnTable()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("MOVE");

            Assert.IsNull(result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPlacedOnTable);
        }

        [TestMethod]
        public void ShouldShouldErrorMessageWhenRobotFallsOffTableInNorthDirection()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,5,NORTH");
            string result = robot.ProcessCommand("MOVE");

            Assert.AreEqual("Robot has fallen off the table", result);
            Assert.AreEqual(1, robot.X);
            Assert.AreEqual(5, robot.Y);
        }

        [TestMethod]
        public void ShouldShouldErrorMessageWhenRobotFallsOffTableInWestDirection()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 0,1,WEST");
            string result = robot.ProcessCommand("MOVE");

            Assert.AreEqual("Robot has fallen off the table", result);
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(1, robot.Y);
        }

        [TestMethod]
        public void ShouldShouldErrorMessageWhenRobotFallsOffTableInSouthDirection()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 1,0,SOUTH");
            string result = robot.ProcessCommand("MOVE");

            Assert.AreEqual("Robot has fallen off the table", result);
            Assert.AreEqual(1, robot.X);
            Assert.AreEqual(0, robot.Y);
        }

        [TestMethod]
        public void ShouldShouldErrorMessageWhenRobotFallsOffTableInEastDirection()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 5,1,EAST");
            string result = robot.ProcessCommand("MOVE");

            Assert.AreEqual("Robot has fallen off the table", result);
            Assert.AreEqual(5, robot.X);
            Assert.AreEqual(1, robot.Y);
        }
        #endregion

        #region Report Command
        [TestMethod]
        public void ShouldIgnoreReportCommandWhenRobotIsNotOnTable()
        {
            ToyRobot robot = new ToyRobot();
            string result = robot.ProcessCommand("REPORT");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldReturnPositionWhenReportCommandCalled()
        {
            ToyRobot robot = new ToyRobot();
            robot.ProcessCommand("PLACE 5,1,EAST");
            string result = robot.ProcessCommand("REPORT");

            Assert.AreEqual("5,1,EAST", result);
        }
        #endregion
    }
}