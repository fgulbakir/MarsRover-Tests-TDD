using System;
using MarsRover.Core;
using MarsRover.Core.Interfaces;
using System.Linq;
using Xunit;

namespace MarsRover.Test
{
    public class RoverTest
    {
        private readonly IPlateaue _plateaue;

        public RoverTest()
        {
            _plateaue = new Plateaue()
            {
                XSide = 5,
                YSide = 5
            };
        }

        [Fact]
        public void When_Rover_Is_Created_At_North()
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            char oriantation = 'N';

            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);
            Assert.IsType(rover.NorthState.GetType(), rover.DetermineCurrentState(oriantation));
        }

        [Fact]
        public void When_Rover_Is_Created_And_TurnRight_TheCurrentState_Is_East()
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            char oriantation = 'N';


            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);
            rover.TurnRight();
            Assert.IsType(rover.EastState.GetType(), rover.CurrentStateBase);
        }


        [Fact]
        public void When_Rover_Is_Created_And_TurnLeft_The_CurrentStates_Is_West()
        {
            int xCoordinate = 3;
            int yCoordinate = 3;
            char oriantation = 'N';


            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);
            rover.TurnLeft();

            Assert.IsType(rover.WestState.GetType(), rover.CurrentStateBase);
        }


        [Fact]
        public void When_Rover_IsCreated_At_North_And_Move_TheRover_WillBe_0_1_Coordinate()
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            char oriantation = 'N';

            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);
            rover.Move();
            Assert.Equal(0, rover.XCoordinate);
            Assert.Equal(1, rover.YCoordinate);
        }

        [Fact]
        public void When_The_Rover_Is_Created_Turn_Right_And_Move_Rover_Will_Be_At_Coordinates_1_0()
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            char oriantation = 'N';
            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);
            rover.TurnRight();
            rover.Move();
            Assert.Equal(1, rover.XCoordinate);
            Assert.Equal(0, rover.YCoordinate);
        }

        [Fact]
        public void When_Start_Move_Turn_Turn_Move_Back_To_Home_Coordindates()
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            char oriantation = 'N';
            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);
            rover.Move();
            rover.TurnLeft();
            rover.TurnLeft();
            rover.Move();
            Assert.Equal(0, rover.XCoordinate);
            Assert.Equal(0, rover.YCoordinate);
        }

        [Theory]
        [InlineData("RMLMMRM", 2, 2, "E")]
        [InlineData("RMLLM", 0, 0, "W")]
        public void TestMovement(string commandString, int xCoordinateExpected, int yCoordinateExpected,
            string directionExpected)
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            char oriantation = 'N';
            var rover = new Rover(xCoordinate, yCoordinate, oriantation, _plateaue);

            RemoteControl(rover, commandString);

            Assert.Equal(xCoordinateExpected, rover.XCoordinate);
            Assert.Equal(yCoordinateExpected, rover.YCoordinate);
            Assert.Equal(directionExpected, rover.CurrentStateBase.ToString());
        }

        public Rover RemoteControl(Rover rover, string commandString)
        {
            var commands = commandString.ToArray();

            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'L':
                        rover.TurnLeft();
                        break;
                    case 'R':
                        rover.TurnRight();
                        break;
                    case 'M':
                        rover.Move();
                        break;
                    default:
                        throw new ArgumentException("undefined command");
                }
            }

            return rover;
        }
    }
}
