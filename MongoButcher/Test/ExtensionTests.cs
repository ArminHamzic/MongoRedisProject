using FluentAssertions;
using MongoDBDemoApp.Core.Util;
using Xunit;

namespace MongoDBDemoApp.Test
{
    public sealed class ExtensionTests
    {
        private const int TRUNCATE_LENGTH = 10;

        [Fact]
        public void TestTruncateNullOrEmptyString()
        {
            string? s1 = null;
            string s2 = string.Empty;
            string s3 = " ";

            s1!.Truncate(TRUNCATE_LENGTH).Should().BeNull();
            s2.Truncate(TRUNCATE_LENGTH).Should().Be(string.Empty);
            s3.Truncate(TRUNCATE_LENGTH).Should().BeEquivalentTo(s3);
        }

        [Fact]
        public void TestTruncateShortString()
        {
            string s1 = "123456789";
            string s2 = "1234567890";

            s1.Truncate(TRUNCATE_LENGTH).Should().BeEquivalentTo(s1);
            s2.Truncate(TRUNCATE_LENGTH).Should().BeEquivalentTo(s2);
        }

        [Fact]
        public void TestTruncateLongString()
        {
            string s = "123456789_10";

            s.Truncate(TRUNCATE_LENGTH).Should().BeEquivalentTo("1234567...");
        }
    }
}