using System.Collections.Generic;

namespace Degreed.WebApi.Model
{
    public class SearchDadJokeViewModel
    {
        public string CurrentPage { get; set; }
        public int Limit { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }        
        public List<RandomJokeViewModel> Result { get; set; }
        public string SearchTerm { get; set; }        
        public int TotalJokes { get; set; }
    }    
}
