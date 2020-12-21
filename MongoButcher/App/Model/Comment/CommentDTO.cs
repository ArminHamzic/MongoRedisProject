using System;

namespace MongoDBDemoApp.Model.Comment
{
    public sealed class CommentDTO
    {
        public string Name { get; set; } = default!;
        public string Mail { get; set; } = default!;
        public string Text { get; set; } = default!;
        public DateTime Created { get; set; }
    }
}