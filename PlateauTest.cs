using MarsRover.Core;
using Xunit;


namespace MarsRover.Test
{
    public class PlateauTest
    {
        [Fact]
        public void When_CreateThe_Plateaue_That_Rover_Position_WillBe_0_0()
        {
            var plateaue = new Plateaue();
            Assert.Equal(0, plateaue.XSide);
            Assert.Equal(0, plateaue.YSide);
        }

    }
}
