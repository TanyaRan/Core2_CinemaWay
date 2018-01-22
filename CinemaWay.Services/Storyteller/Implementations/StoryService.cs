namespace CinemaWay.Services.Storyteller.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServicesConstants;

    public class StoryService : IStoryService
    {
        private readonly CinemaWayDbContext db;

        public StoryService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ListStoriesModel>> All(int page = 1)
            => await this.db
            .Stories
            .OrderByDescending(a => a.PublishDate)
            .Skip((page - 1) * StoriesPageSize)
            .Take(StoriesPageSize)
            .ProjectTo<ListStoriesModel>()
            .ToListAsync();

        public async Task<int> Total()
            => await this.db.Stories.CountAsync();

        public async Task<StoryDetailsModel> ById(int id)
            => await this.db
            .Stories
            .Where(s => s.Id == id)
            .ProjectTo<StoryDetailsModel>()
            .FirstOrDefaultAsync();

        public async Task Create(string title, string content, string imageUrl, ReviewOf review, string authorId)
        {
            var story = new Story
            {
                Title = title,
                Content = content,
                ImageUrl = imageUrl,
                ReviewOf = review,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(story);

            await this.db.SaveChangesAsync();
        }
    }
}
