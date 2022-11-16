using AutoMapper;
using Degreed.BAL;
using Degreed.WebApi.Constants;
using Degreed.WebApi.Enum;
using Degreed.WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Degreed.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadJokeController : BaseController
    {
        #region Declarations
        private readonly IDadJokeService dadJokeService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        string baseUrl = string.Empty;
        #endregion

        #region Constructor
        public DadJokeController(IMapper mapper, IDadJokeService dadJokeService, IConfiguration configuration)
        {
            this.dadJokeService = dadJokeService;
            this.mapper = mapper;
            this.configuration = configuration;
            baseUrl = configuration.GetValue<string>(DegreedConstants.ICanHazDadJokesUrl);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method will get the random jokes
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRandomJokes")]
        public JsonResponse GetRandomJokes()
        {

            var getResult = dadJokeService.GetRandomJokes(baseUrl);
            var randomJokeViewModel = mapper.Map<RandomJokeViewModel>(getResult);
            if (randomJokeViewModel.Id == null)
                return JsonResult((int)DegreedEnum.StatusCode.NoData, DegreedConstants.DataNotFound);
            return JsonResult((int)DegreedEnum.StatusCode.Success, DegreedConstants.Success, randomJokeViewModel);
        }

        /// <summary>
        /// This method will get the search word macthing jokes and if the search word is emppty it will return
        /// all the jokes
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSearchJokes")]
        public JsonResponse GetSearchJokes(string searchTerm, int limit, int page)
        {
            var getResult = dadJokeService.SearchJokes(baseUrl, searchTerm, limit, page);
            var searchDadJokeViewModel = mapper.Map<SearchDadJokeViewModel>(getResult);
            if (searchDadJokeViewModel.Result.Count == 0)
                return JsonResult((int)DegreedEnum.StatusCode.NoData, DegreedConstants.DataNotFound);
            return JsonResult((int)DegreedEnum.StatusCode.Success, DegreedConstants.Success, searchDadJokeViewModel);
        }
        #endregion
    }
}
