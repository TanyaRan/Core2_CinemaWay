namespace CinemaWay.Services.Storyteller
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStoryService
    {
        Task<IEnumerable<ListStoriesModel>> All(int page = 1);

        Task<int> Total();

        Task<StoryDetailsModel> ById(int id);

        Task Create(
            string title,
            string content,
            string imageUrl,
            ReviewOf review,
            string authorId);
    }
}
