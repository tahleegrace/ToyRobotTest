using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}