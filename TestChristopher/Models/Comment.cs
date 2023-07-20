namespace TestChristopher.Models
{
    public class Comment
    {
        public int Id { get; init; }
        public string Content { get; init; }

        public IPost PostInfo { get; init; }

        public Comment(int id, string content, int postId, string postContent)
        {
            Id = id;
            Content = content;

            PostInfo = new Post(postId, postContent);
        }

        private struct Post : IPost
        {
            public int Id { get; init; }
            public string Content { get; init; }

            public Post(int id, string content)
            {
                Id = id;
                Content = content;
            }
        }
    }
}
