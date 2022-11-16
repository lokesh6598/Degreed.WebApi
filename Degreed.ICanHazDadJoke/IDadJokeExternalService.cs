namespace Degreed.ICanHazDadJoke
{
    public interface IDadJokeExternalService
    {
        public RandomJokeModel GetRandomJokes(string baseUrl);
        public SearchDadJokeModel SearchJokes(string baseUrl, string searchWord, int limit, int page);
    }
}
