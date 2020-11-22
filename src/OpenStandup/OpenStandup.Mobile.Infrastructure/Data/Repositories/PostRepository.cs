using OpenStandup.Mobile.Infrastructure.Data.Model;
using System;
using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;
using OpenStandup.Core.Interfaces.Data.Repositories;


namespace OpenStandup.Mobile.Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDb _appDb;

        public PostRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<bool> InsertOrReplace(Post post)
        {
            return await _appDb.AsyncDb.InsertOrReplaceAsync(new PostData
            {
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                StatusId = post.Status.Value,
                Text = post.Text,
                Image = post.Image
            }).ConfigureAwait(false) == 1;
        }
    }
}