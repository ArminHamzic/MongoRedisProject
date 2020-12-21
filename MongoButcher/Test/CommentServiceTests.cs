using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using NSubstitute;
using Xunit;

namespace MongoDBDemoApp.Test
{
    public sealed class CommentServiceTests
    {
        [Fact]
        public async Task TestGetPostById()
        {
            var id = new ObjectId();
            var repoMock = Substitute.For<ICommentRepository>();
            repoMock.GetCommentById(Arg.Any<ObjectId>()).Returns(ci => new Comment
            {
                Id = ci.Arg<ObjectId>()
            });

            var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
            var comment = await service.GetCommentById(id);

            await repoMock.Received(1).GetCommentById(Arg.Is(id));
            comment.Should().NotBeNull();
            comment!.Id.Should().Be(id);
        }

        [Fact]
        public async Task TestAddPost()
        {
            var postId = new ObjectId();
            var expectedPost = new Comment
            {
                Id = new ObjectId(),
                Name = "Horst",
                Created = new DateTime(2020, 08, 20, 18, 31, 05),
                Text = "Foo",
                Mail = "Bar@Baz.com",
                PostId = postId
            };
            var repoMock = Substitute.For<ICommentRepository>();
            repoMock.AddComment(Arg.Any<Comment>()).Returns(ci =>
            {
                var c = ci.ArgAt<Comment>(0);
                c.Id = expectedPost.Id;
                return c;
            });
            var dtMock = Substitute.For<IDateTimeProvider>();
            dtMock.Now.ReturnsForAnyArgs(expectedPost.Created);

            var service = new CommentService(dtMock, repoMock);
            var comment = await service.AddComment(new Post {Id = postId}, expectedPost.Name, expectedPost.Mail,
                expectedPost.Text);

            await repoMock.Received(1).AddComment(Arg.Any<Comment>());
            comment.Should().NotBeNull();
            comment.Should().BeEquivalentTo(expectedPost);
        }

        [Fact]
        public async Task TestGetCommentsForPost()
        {
            var postId = new ObjectId();
            Comment[] expectedComments =
            {
                new Comment
                {
                    Id = new ObjectId(),
                    Created = new DateTime(2020, 08, 20, 18, 31, 05),
                    Mail = "foo@bar.com",
                    Name = "Horst",
                    PostId = postId,
                    Text = "baz"
                },
                new Comment
                {
                    Id = new ObjectId(),
                    Created = new DateTime(2020, 08, 20, 18, 38, 02),
                    Mail = "foo2@bar.com",
                    Name = "Sepp",
                    PostId = postId,
                    Text = "foobar"
                }
            };
            var repoMock = Substitute.For<ICommentRepository>();
            repoMock.GetCommentsForPost(Arg.Is(postId)).Returns(expectedComments);

            var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
            IReadOnlyCollection<Comment> comments = await service.GetCommentsForPost(new Post {Id = postId});

            await repoMock.Received(1).GetCommentsForPost(Arg.Is(postId));
            comments.Should().NotBeNullOrEmpty()
                .And.HaveCount(2)
                .And.Contain(expectedComments);
        }
    }
}