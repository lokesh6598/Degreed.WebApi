using System.Collections.Generic;

namespace Degreed.BAL
{
    public class SearchDadJokeModel : RandomJokeModel
    {
        public string CurrentPage { get; set; }
        public int Limit { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public List<RandomJokeModel> Result { get; set; }
        public string SearchTerm { get; set; }
        public int TotalJokes { get; set; }
    }
}
