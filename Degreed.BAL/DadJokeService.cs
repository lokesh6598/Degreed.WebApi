using System;
using System.Collections.Generic;
using Degreed.Core;
using Degreed.ICanHazDadJoke;
using Degreed.Logger;

namespace Degreed.BAL
{
    public class DadJokeService : IDadJokeService
    {
        #region Declarations

        private readonly IDadJokeExternalService dadJokeExternalService;
        private readonly IDegreedLogger degreedLogger;

        #endregion

        #region Constructor
        public DadJokeService(IDadJokeExternalService dadJokeExternalService, IDegreedLogger degreedLogger)
        {
            this.dadJokeExternalService = dadJokeExternalService;
            this.degreedLogger = degreedLogger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method will get the random data from api and api model data will map with BAL model.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public RandomJokeModel GetRandomJokes(string baseUrl)
        {
            try
            {
                var randomJokeModel = new RandomJokeModel();

                var getResponse = dadJokeExternalService.GetRandomJokes(baseUrl);

                if (getResponse.id != null)
                {
                    randomJokeModel.Id = getResponse.id;
                    randomJokeModel.Joke = getResponse.joke;
                }

                return randomJokeModel;
            }
            catch (Exception ex)
            {
                degreedLogger.LogError(ex.Message);
                degreedLogger.LogError(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// This method will consoume the data search word match data and map the api data with BAL model
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="searchWord"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>        
        public SearchDadJokeModel SearchJokes(string baseUrl, string searchWord, int limit, int page)
        {
            try
            {
                var searchDadJokeModel = new SearchDadJokeModel();
                var getResponse = dadJokeExternalService.SearchJokes(baseUrl, searchWord, limit, page);

                if (getResponse.results != null && getResponse.results.Count > 0)
                {
                    searchDadJokeModel = PrepareSearchDadJokeModel(getResponse, searchWord);
                }
                return searchDadJokeModel;
            }
            catch (Exception ex)
            {
                degreedLogger.LogError(ex.Message);
                degreedLogger.LogError(ex.StackTrace);
                throw ex;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// This method will map the api data. It will add highlight the search word on each joke as well
        /// as will find the number of words on each joke.
        /// </summary>
        /// <param name="responseModel"></param>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        private SearchDadJokeModel PrepareSearchDadJokeModel(Degreed.ICanHazDadJoke.SearchDadJokeModel responseModel, string searchWord)
        {
            var searchDadJokeModel = new SearchDadJokeModel();
            var listRandomJokeModel = new List<RandomJokeModel>();

            //Highlight the search word and count the number of words in each joke
            responseModel.results.ForEach(r =>
            {
                var randomJokeModel = new RandomJokeModel();
                randomJokeModel.Id = r.id;
                randomJokeModel.NumberOfWords = !string.IsNullOrEmpty(searchWord) ? r.joke.CountNumberOfWords(): 0;
                randomJokeModel.Joke = !string.IsNullOrEmpty(searchWord) ? r.joke.HighlightSearchWord(searchWord) : r.joke;
                listRandomJokeModel.Add(randomJokeModel);
            });

            //sort the jokes content based on word length
            listRandomJokeModel.Sort((lc, rc) => lc.NumberOfWords.CompareTo(rc.NumberOfWords));

            searchDadJokeModel.CurrentPage = responseModel.current_page;
            searchDadJokeModel.Limit = responseModel.limit;
            searchDadJokeModel.NextPage = responseModel.next_page;
            searchDadJokeModel.PreviousPage = responseModel.previous_page;
            searchDadJokeModel.Result = listRandomJokeModel;
            searchDadJokeModel.SearchTerm = responseModel.search_term;

            return searchDadJokeModel;
        }
        #endregion
    }
}
