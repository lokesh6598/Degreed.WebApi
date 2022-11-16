using System.Collections.Generic;

namespace Degreed.ICanHazDadJoke
{
    public class SearchDadJokeModel : RandomJokeModel
    {
        public string current_page { get; set; }
        public int limit { get; set; }
        public int next_page { get; set; }
        public int previous_page { get; set; }
        public List<RandomJokeModel> results { get; set; }
        public string search_term { get; set; }
        public int status { get; set; }
        public int total_jokes { get; set; }
        public int total_pages { get; set; }
    }
}
