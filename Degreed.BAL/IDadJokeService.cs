namespace Degreed.BAL
{
    public interface IDadJokeService
    {
        public RandomJokeModel GetRandomJokes(string baseUrl);
        public SearchDadJokeModel SearchJokes(string baseUrl,string searchWord, int limit, int page);
    }
}
