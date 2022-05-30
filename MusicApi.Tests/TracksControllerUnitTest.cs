using Microsoft.Extensions.Logging;
using Moq;
using MusicApi.Controllers;
using MusicApi.Interfaces;

namespace MusicApi.Tests
{
    public class TracksControllerUnitTest
    {
        #region PrivateVariables
        private readonly Mock<ILogger<TracksController>> loggerStub = new(); // Mock Stub of logger dependency
        private readonly Mock<ITracksService> tracksServiceStub = new(); // Mock Stub of pokemonService dependency
        #endregion

        [Fact]
        public void Test1()
        {

        }
    }
}