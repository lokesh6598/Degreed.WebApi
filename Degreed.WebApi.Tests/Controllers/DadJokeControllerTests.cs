using AutoMapper;
using Degreed.BAL;
using Degreed.WebApi.Constants;
using Degreed.WebApi.Controllers;
using Degreed.WebApi.Model;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Degreed.WebApi.Enum.DegreedEnum;

namespace Degreed.WebApi.Tests.Controllers
{
    public class DadJokeControllerTests
    {
        Mock<IDadJokeService> mockDadJokeService;
        IMapper mapper;
        Mock<IConfigurationSection> mockConfigSection;
        Mock<IConfiguration> mockConfiguration;
        string baseUrl = "https://icanhazdadjoke.com/";
        public DadJokeControllerTests()
        {
            this.mockDadJokeService = new Mock<IDadJokeService>();
            this.mockConfiguration = new Mock<IConfiguration>();
            this.mockConfigSection = new Mock<IConfigurationSection>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelMapping());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public void GetRandomJokes_Successful()
        {
            //Arrange            
            var randomModel = new RandomJokeModel();
            randomModel.Id = "1DQZDY0gVnb";
            randomModel.Joke = "What is a centipedes's favorite Beatle song?  I want to hold your hand, hand, hand, hand...";

            mockDadJokeService.Setup(r => r.GetRandomJokes(baseUrl)).Returns(randomModel);
            mockConfigSection.Setup(x => x.Value).Returns(baseUrl);
            mockConfiguration.Setup(x => x.GetSection(It.Is<string>(k => k == DegreedConstants.ICanHazDadJokesUrl))).Returns(mockConfigSection.Object);

            //Act
            var dadJokeController = new DadJokeController(mapper: mapper, dadJokeService: mockDadJokeService.Object, configuration: mockConfiguration.Object);
            var randomJokeData_result = dadJokeController.GetRandomJokes();

            //Assert
            var randomJokeDataModel = randomJokeData_result as JsonResponse;

            if (randomJokeDataModel.StatusCode == (int)StatusCode.Success)
            {
                Assert.NotNull(randomJokeDataModel);

                var expectedValue = randomJokeDataModel.Result.Id;
                var actualValue = randomModel.Id;

                Assert.Equal(expectedValue, actualValue);
            }
        }

        [Fact]
        public void GetRandomJokes_Failure_NoDataFound()
        {
            //Arrange           
            var randomModel = new RandomJokeModel();

            mockDadJokeService.Setup(r => r.GetRandomJokes(baseUrl)).Returns(randomModel);
            mockConfigSection.Setup(x => x.Value).Returns(baseUrl);
            mockConfiguration.Setup(x => x.GetSection(It.Is<string>(k => k == DegreedConstants.ICanHazDadJokesUrl))).Returns(mockConfigSection.Object);

            //Act
            var dadJokeController = new DadJokeController(mapper: mapper, dadJokeService: mockDadJokeService.Object, configuration: mockConfiguration.Object);
            var randomJokeData_result = dadJokeController.GetRandomJokes();

            //Assert
            var randomJokeDataModel = randomJokeData_result as JsonResponse;

            if (randomJokeDataModel.StatusCode == (int)StatusCode.NoData)
            {
                Assert.Null(randomJokeDataModel.Result);
            }
        }

        [Fact]
        public void GetSearchJokes_Successful()
        {
            SearchJokes_Test("ask", 30, 1);
        }

        [Fact]
        public void GetSearchJokes_WithoutSearchWord()
        {
            SearchJokes_Test("", 30, 1);
        }

        [Fact]
        public void GetSearchJokes_Failure_NoDataFound()
        {
            //Arrange            
            var searchDadJokeModel = new SearchDadJokeModel();
            var lsRandomJokeModel = new List<RandomJokeModel>();

            mockDadJokeService.Setup(r => r.SearchJokes(baseUrl, "NoData", 30, 1)).Returns(searchDadJokeModel);
            mockConfigSection.Setup(x => x.Value).Returns(baseUrl);
            mockConfiguration.Setup(x => x.GetSection(It.Is<string>(k => k == DegreedConstants.ICanHazDadJokesUrl))).Returns(mockConfigSection.Object);

            //Act
            var dadJokeController = new DadJokeController(mapper: mapper, dadJokeService: mockDadJokeService.Object, configuration: mockConfiguration.Object);
            var searchJokeData_result = dadJokeController.GetSearchJokes("NoData", 30, 1);

            //Assert
            var searchJokeData = searchJokeData_result as JsonResponse;

            if (searchJokeData.StatusCode == (int)StatusCode.NoData)
            {
                Assert.Null(searchJokeData.Result);
            }
        }

        private void SearchJokes_Test(string searchWord, int limit, int page)
        {
            //Arrange            
            var searchDadJokeModel = new SearchDadJokeModel();
            var lsRandomJokeModel = new List<RandomJokeModel>();

            searchDadJokeModel.CurrentPage = "1";
            searchDadJokeModel.Limit = 30;
            searchDadJokeModel.NextPage = 1;
            searchDadJokeModel.PreviousPage = 1;

            var randomJokeModel = new RandomJokeModel();
            lsRandomJokeModel.Add(new RandomJokeModel() { Id = "1DQZDY0gVnb", Joke = "What is a centipedes's favorite Beatle song?  I want to hold your hand, hand, hand, hand..." });
            lsRandomJokeModel.Add(new RandomJokeModel() { Id = "scUvsrORKe", Joke = "I <b>[ask]</b>ed a frenchman if he played video games. He said \"Wii\"" });

            searchDadJokeModel.Result = lsRandomJokeModel;

            mockDadJokeService.Setup(r => r.SearchJokes(baseUrl, searchWord, limit, page)).Returns(searchDadJokeModel);
            mockConfigSection.Setup(x => x.Value).Returns(baseUrl);
            mockConfiguration.Setup(x => x.GetSection(It.Is<string>(k => k == DegreedConstants.ICanHazDadJokesUrl))).Returns(mockConfigSection.Object);

            //Act
            var dadJokeController = new DadJokeController(mapper: mapper, dadJokeService: mockDadJokeService.Object, configuration: mockConfiguration.Object);
            var searchJokeData_result = dadJokeController.GetSearchJokes(searchWord, limit, page);

            //Assert
            var searchJokeData = searchJokeData_result as JsonResponse;

            if (searchJokeData.StatusCode == (int)StatusCode.Success)
            {
                Assert.NotNull(searchJokeData.Result);

                var expectedValue = searchJokeData.Result.Result[0].Id;
                var actualValue = searchDadJokeModel.Result[0].Id;

                Assert.Equal(expectedValue, actualValue);
            }
        }

    }
}
