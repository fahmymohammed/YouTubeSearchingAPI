namespace YouTubeServices.ViewModels
{
    public class YoutubeVideos
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string VideoId { get; set; }
        public DateTime PublishedAt { get; set; }

        public string Thumbnail { get; set; }

        public List<YoutubeTags> Tags { get; set; }


    }
}
