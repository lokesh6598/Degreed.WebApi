using System.Collections.Generic;
using Degreed.ICanHazDadJoke;
using Degreed.Logger;
using Moq;
using Xunit;

namespace Degreed.BAL.Tests
{
    public class DadJokeServiceTests
    {
        Mock<IDadJokeExternalService> mockIDadJokeExternalService;
        Mock<IDegreedLogger> mockLogger;       
        string baseUrl = "https://icanhazdadjoke.com/";

        public DadJokeServiceTests()
        {
            mockIDadJokeExternalService = new Mock<IDadJokeExternalService>();
            mockLogger = new Mock<IDegreedLogger>();                       
        }

        [Fact]
        public void GetRandomJokes_Success()
        {
            //Arrange
            var randomJokeModel = new Degreed.ICanHazDadJoke.RandomJokeModel();

            randomJokeModel.id = "1DQZDY0gVnb";
            randomJokeModel.joke = "What is a centipedes's favorite Beatle song?  I want to hold your hand, hand, hand, hand...";

            mockIDadJokeExternalService.Setup(r => r.GetRandomJokes(baseUrl)).Returns(randomJokeModel);

            //Act
            var dadJokeService = new DadJokeService(dadJokeExternalService: mockIDadJokeExternalService.Object, degreedLogger: mockLogger.Object);
            var getResult = dadJokeService.GetRandomJokes(baseUrl);

            //Assert
            var expectedValue = getResult.Id;
            var actualValue = randomJokeModel.id;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void GetRandomJokes_Success_WithNoData()
        {
            //Arrange
            var randomJokeModel = new Degreed.ICanHazDadJoke.RandomJokeModel();
            mockIDadJokeExternalService.Setup(r => r.GetRandomJokes(baseUrl)).Returns(randomJokeModel);

            //Act
            var dadJokeService = new DadJokeService(dadJokeExternalService: mockIDadJokeExternalService.Object, degreedLogger: mockLogger.Object);
            var getResult = dadJokeService.GetRandomJokes(baseUrl);

            //Assert  
            Assert.Null(getResult.Id);
        }

        [Fact]
        public void GetRandomJokes_Failure_UriFormatException()
        {
            //Arrange
            baseUrl = "";
            mockIDadJokeExternalService.Setup(r => r.GetRandomJokes(baseUrl)).Throws(new System.UriFormatException());

            //Act
            var dadJokeService = new DadJokeService(dadJokeExternalService: mockIDadJokeExternalService.Object, degreedLogger: mockLogger.Object);

            //Assert
            Assert.Throws<System.UriFormatException>(() => dadJokeService.GetRandomJokes(baseUrl));
        }

        [Fact]
        public void SearchJokes_Success_WithSearchWord()
        {
            SearchJokesTest("ask", 30, 1);
        }

        [Fact]
        public void SearchJokes_Success_WithoutSearchWord()
        {
            SearchJokesTest("", 30, 1);
        }

        [Fact]
        public void SearchJokes_Success_WithNoData()
        {
            //Arrange
            var searchDadJokeModel = new Degreed.ICanHazDadJoke.SearchDadJokeModel();
            var lsRandomJokeModel = new List<Degreed.ICanHazDadJoke.RandomJokeModel>();
            searchDadJokeModel.results = lsRandomJokeModel;

            mockIDadJokeExternalService.Setup(r => r.SearchJokes(baseUrl, "Test", 30, 1)).Returns(searchDadJokeModel);

            //Act
            var dadJokeService = new DadJokeService(dadJokeExternalService: mockIDadJokeExternalService.Object, degreedLogger: mockLogger.Object);
            var getResult = dadJokeService.SearchJokes(baseUrl, "Test", 30, 1);

            //Assert          
            Assert.Null(getResult.Result);
        }

        [Fact]
        public void SearchJokes_Failure_UriFormatException()
        {
            //Arrange
            baseUrl = "";
            mockIDadJokeExternalService.Setup(r => r.SearchJokes(baseUrl, "ask", 30, 1)).Throws(new System.UriFormatException());

            //Act
            var dadJokeService = new DadJokeService(dadJokeExternalService: mockIDadJokeExternalService.Object, degreedLogger: mockLogger.Object);

            //Assert
            Assert.Throws<System.UriFormatException>(() => dadJokeService.SearchJokes(baseUrl, "ask", 30, 1));
        }

        private void SearchJokesTest(string searchWord, int limit, int page)
        {
            //Arrange
            var searchDadJokeModel = new Degreed.ICanHazDadJoke.SearchDadJokeModel();
            var lsRandomJokeModel = new List<Degreed.ICanHazDadJoke.RandomJokeModel>();

            searchDadJokeModel.current_page = "1";
            searchDadJokeModel.limit = 30;
            searchDadJokeModel.next_page = 1;
            searchDadJokeModel.previous_page = 1;

            var randomJokeModel = new Degreed.ICanHazDadJoke.RandomJokeModel();
            lsRandomJokeModel.Add(new Degreed.ICanHazDadJoke.RandomJokeModel() { id = "1DQZDY0gVnb", joke = "What is a centipedes's favorite Beatle song?  I want to hold your hand, hand, hand, hand..." });
            lsRandomJokeModel.Add(new Degreed.ICanHazDadJoke.RandomJokeModel() { id = "scUvsrORKe", joke = "I <b>[ask]</b>ed a frenchman if he played video games. He said \"Wii\"" });

            searchDadJokeModel.results = lsRandomJokeModel;

            mockIDadJokeExternalService.Setup(r => r.SearchJokes(baseUrl, searchWord, limit, page)).Returns(searchDadJokeModel);

            //Act
            var dadJokeService = new DadJokeService(dadJokeExternalService: mockIDadJokeExternalService.Object, degreedLogger: mockLogger.Object);
            var getResult = dadJokeService.SearchJokes(baseUrl, searchWord, limit, page);

            //Assert
            string expectedValue = string.Empty;
            string actualValue = string.Empty;
            if (string.IsNullOrEmpty(searchWord))
            {
                expectedValue = getResult.Result[0].Id;
                actualValue = searchDadJokeModel.results[0].id;
            }
            else
            {
                expectedValue = getResult.Result[0].Id;
                actualValue = searchDadJokeModel.results[1].id;
            }

            Assert.Equal(expectedValue, actualValue);
        }
    }
}
