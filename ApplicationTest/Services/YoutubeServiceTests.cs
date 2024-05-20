using FakeItEasy;
using Xunit;

namespace YouTubeServices.Services.Tests
{

    [TestClass()]
    public class YoutubeServiceTests
    {
        private readonly IYoutubeService _sut;

        public YoutubeServiceTests(IYoutubeService sut)
        {
            _sut = A.Fake<IYoutubeService>();
        }

        [Fact(DisplayName = "GetYouTubeVideosTest", Skip = "Something Wrong with This Unit Test")]
        [TestMethod()]
        public void GetYouTubeVideosTest()
        {
            //Arrange
            var youtubeVideos = _sut.GetYouTubeVideos("Hello", 5).Result.videoslist.ToList();
            var youtubeVideosTitle = _sut.GetYouTubeVideos("Hello", 5).Result.videoslist.FirstOrDefault()?.Title.ToLower().Contains("hello".ToLower());

            //ACT

            //Assert
            Assert.IsTrue(youtubeVideos.Count() == 5);
            Assert.IsTrue(youtubeVideosTitle);


        }
    }
}