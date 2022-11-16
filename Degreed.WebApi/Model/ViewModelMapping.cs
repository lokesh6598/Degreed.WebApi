using AutoMapper;
using Degreed.BAL;

namespace Degreed.WebApi.Model
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<RandomJokeModel, RandomJokeViewModel>();
            CreateMap<SearchDadJokeModel, SearchDadJokeViewModel>();
        }
    }
}
