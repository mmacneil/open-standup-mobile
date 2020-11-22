using OpenStandup.Common;

namespace OpenStandup.Core.Domain.Entities
{
    public class Post
    {
        public PostStatus Status { get; }
        public string Text { get; }
        public byte?[] Image { get; }

        public Post(PostStatus status, string text, byte?[] image)
        {
            Status = status;
            Text = text;
            Image = image;
        }
    }
}
