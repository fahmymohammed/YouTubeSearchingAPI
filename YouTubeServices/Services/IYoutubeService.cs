using YouTubeServices.ViewModels;

namespace YouTubeServices.Services
{
    public interface IYoutubeService
    {
        Task<(IEnumerable<YoutubeVideos> videoslist, bool status, string message)> GetYouTubeVideos(string keywords, int limits = 5);
    }
}
