using Degreed.Core;
using Newtonsoft.Json;
using System;

namespace Degreed.ICanHazDadJoke
{
    public class DadJokeExternalService : IDadJokeExternalService
    {
        #region Public Methods

        /// <summary>
        /// This method will get the random jokes from the external api
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public RandomJokeModel GetRandomJokes(string baseUrl)
        {
            var getResponse = HttpClientService.ConsumeHttpGetServiceAsync(baseUrl, "application/json");
            return JsonConvert.DeserializeObject<RandomJokeModel>(Convert.ToString(getResponse));
        }

        /// <summary>
        /// This method will get the search word matching jokes and if the search word is empty will get 
        /// all the jokes
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="searchWord"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public SearchDadJokeModel SearchJokes(string baseUrl, string searchWord, int limit, int page)
        {
            string searchQueryString = string.Empty;

            if (!string.IsNullOrEmpty(searchWord))
            {
                baseUrl = baseUrl + string.Format("search?term={0}&limit={1}&page={2}", searchWord, limit, page);                
            }
            else
            {
                baseUrl = baseUrl + "search";
            }
            var getResponse = HttpClientService.ConsumeHttpGetServiceAsync(baseUrl, "application/json");

            return JsonConvert.DeserializeObject<SearchDadJokeModel>(Convert.ToString(getResponse));
        }
        #endregion
    }
}
