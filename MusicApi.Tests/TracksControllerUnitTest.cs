using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MusicApi.Controllers;
using MusicApi.Interfaces;
using MusicApi.Models;
using MusicApi.Tests.Utils;

namespace MusicApi.Tests
{
    public class TracksControllerUnitTest
    {
        #region PrivateVariables
        private readonly Mock<ILogger<TracksController>> loggerStub = new(); // Mock Stub of logger dependency
        private readonly Mock<ITracksService> tracksServiceStub = new(); // Mock Stub of pokemonService dependency
        #endregion
       
        [Fact]
        public void FindTracksByWord_WithExpectedWord_ReturnsExpectedResult()
        {
            //Arrange
            var trackListMock = DataModels.GetSampleTracksDtoList();
            tracksServiceStub.Setup(tracks => tracks.ListByWordAsync(It.IsAny<string>())).ReturnsAsync(trackListMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);

            // Act 
            var actionResult = controller.FindTracksByWord(It.IsAny<string>());

            // Assert
            actionResult.Result.Should().BeOfType<ActionResult<TrackDTO>>();
        }


        [Fact]
        public void FindTracksByWord_WithExpectedWord_ReturnsOk()
        {
            //Arrange
            var trackListMock = DataModels.GetSampleTracksDtoList();
            tracksServiceStub.Setup(tracks => tracks.ListByWordAsync(It.IsAny<string>())).ReturnsAsync(trackListMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);

            var actionResultShouldbe = new ActionResult<TrackDTO>(new OkObjectResult(trackListMock));

            // Act 
            var actionResult = controller.FindTracksByWord(It.IsAny<string>());

            // Assert
            actionResult.Result.Should().BeEquivalentTo(actionResultShouldbe);
        }

        [Fact]
        public void FindTracksByWord_WithNonExistentWord_ReturnsNotFound()
        {
            //Arrange
            List<TrackDTO> trackListMock = null; // initialise empty list to denote empty result from TracksService
            tracksServiceStub.Setup(tracks => tracks.ListByWordAsync(It.IsAny<string>())).ReturnsAsync(trackListMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);

            var actionResultShouldbe = new ActionResult<TrackDTO>(new NotFoundResult());

            // Act 
            var actionResult = controller.FindTracksByWord(It.IsAny<string>());

            // Assert
            actionResult.Result.Should().BeEquivalentTo(actionResultShouldbe);
        }

        [Fact]
        public void FindTracksByWord_WithSpace_ReturnsBadRequest()
        {
            //Arrange
            var trackListMock = DataModels.GetSampleTracksDtoList();
            tracksServiceStub.Setup(tracks => tracks.ListByWordAsync(It.IsAny<string>())).ReturnsAsync(trackListMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);

            var actionResultShouldbe = new ActionResult<TrackDTO>(new BadRequestResult());

            // Act 
            var actionResult = controller.FindTracksByWord(" ");

            // Assert
            actionResult.Result.Should().BeEquivalentTo(actionResultShouldbe);

        }

        [Fact]
        public void GetTrackCountByWord_WithExpectedWord_ReturnsExpectedResult()
        {
            //Arrange
            int trackCountMock = 3; // Picking the value count of the word "Come" according to the sampledata.json 
            tracksServiceStub.Setup(tracks => tracks.GetTrackCountByWord(It.IsAny<string>())).ReturnsAsync(trackCountMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);
            var actionResultShouldbe = new ActionResult<TrackDTO>(new OkObjectResult($"{{\"count\":{trackCountMock}}})"));

            // Act 
            var actionResult = controller.GetTrackCountByWord("Come");

            // Assert
            actionResult.Result.Should().BeOfType<ActionResult<TrackDTO>>();
        }

        [Fact]
        public void GetTrackCountByWord_WithNonExistentWord_ReturnsNotFound()
        {
            //Arrange
            int trackCountMock = 0;  
            tracksServiceStub.Setup(tracks => tracks.GetTrackCountByWord(It.IsAny<string>())).ReturnsAsync(trackCountMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);
            var actionResultShouldbe = new ActionResult<TrackDTO>(new NotFoundResult());

            // Act 
            var actionResult = controller.GetTrackCountByWord(It.IsAny<string>());

            // Assert
            actionResult.Result.Should().BeEquivalentTo(actionResultShouldbe);
        }

        [Fact]
        public void GetTrackCountByWord_WithSpace_ReturnsBadRequest()
        {
            //Arrange
            int trackCountMock = 0;
            tracksServiceStub.Setup(tracks => tracks.GetTrackCountByWord(It.IsAny<string>())).ReturnsAsync(trackCountMock);

            var controller = new TracksController(tracksServiceStub.Object, loggerStub.Object);
            var actionResultShouldbe = new ActionResult<TrackDTO>(new BadRequestResult());

            // Act 
            var actionResult = controller.GetTrackCountByWord(" ");

            // Assert
            actionResult.Result.Should().BeEquivalentTo(actionResultShouldbe);
        }
    }
}