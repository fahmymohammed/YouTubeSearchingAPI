using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeServices.ViewModels;

namespace YouTubeServices.Services
{
    public interface IYoutubeService
    {
        Task<(IEnumerable<YoutubeVideos> videoslist,bool status, string message)> GetYouTubeVideos(string keywords, int limits = 5);
    }
}
