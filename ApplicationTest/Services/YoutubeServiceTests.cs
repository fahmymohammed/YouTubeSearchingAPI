using Xunit;

namespace YouTubeServices.Services.Tests
{

    public class YoutubeServiceTests
    {
        private readonly IYoutubeService _sut;

        public YoutubeServiceTests(IYoutubeService sut)
        {
            _sut = sut;
        }

        [Fact(DisplayName = "GetYouTubeVideosTest", Skip = "Something Wrong with This Unit Test")]
        public void GetYouTubeVideosTest()
        {
            //Arrange
            var youtubeVideos = _sut.GetYouTubeVideos("Hello", 5).Result.videoslist.ToList();
            var youtubeVideosTitle = _sut.GetYouTubeVideos("Hello", 5).Result.videoslist.FirstOrDefault()?.Title.ToLower().Contains("hello".ToLower());

            //ACT

            //Assert
            Assert.IsTrue(youtubeVideos.Count() == 6);
            Assert.IsTrue(youtubeVideosTitle);


        }

    }
}