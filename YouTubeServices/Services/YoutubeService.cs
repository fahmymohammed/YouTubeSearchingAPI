using Microsoft.Extensions.Configuration;
using System.Text.Json;
using YouTubeServices.Validation;
using YouTubeServices.ViewModels;

namespace YouTubeServices.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IValidation _validation;

        public YoutubeService(HttpClient httpClient, IConfiguration configuration, IValidation validation)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _validation = validation;
        }

        public async Task<(IEnumerable<YoutubeVideos> videoslist, bool status, string message)> GetYouTubeVideos(string keywords, int limits = 5)
        {
            //I was planning to change the limits from user input, However i cancelled
            //I can work on this Project for long time to make it more elegant and more safe
            //I Wish i could do the bounce, i wanna to do the bounce but i have to do some personal Tasks


            //Preparing for youtube call
            //it can be one line or even inline directly with GetAsync function
            //can stored in appsettings.json or in the Program.cs services section

            var applicationyoutubeBaseUrl = _configuration["Youtubesearch:YouTubeBaseUrl"];
            var applicationyoutubekey = _configuration["Youtubesearch:Key"];

            if (_validation.ValidationForNull(applicationyoutubeBaseUrl))
                return (new List<YoutubeVideos>(), false, "Error fetching data...");

            if (_validation.ValidationForNull(applicationyoutubekey))
                return (new List<YoutubeVideos>(), false, "Error fetching data...");



            var youtubefullUrl = $"{applicationyoutubeBaseUrl}&q={keywords}&key={applicationyoutubekey}";

            //call the endpoit
            var clientResponse = await _httpClient.GetAsync(youtubefullUrl);
            try
            {
                clientResponse.EnsureSuccessStatusCode();

                //call the Deserialize the Json Object To c#
                using var responseReadStream = await clientResponse.Content.ReadAsStreamAsync();
                var responseOBJ = await JsonSerializer.DeserializeAsync<YoutubeVideosResponse>(responseReadStream);

                //Validate the Result
                if (responseOBJ?.items.Count() > 0)
                {
                    //Populate the Data
                    var callresult = responseOBJ.items.Select(async y => new YoutubeVideos
                    {
                        Title = y.snippet.title,
                        Description = y.snippet.description,
                        VideoId = y.id.videoId,
                        PublishedAt = y.snippet.publishedAt,
                        Thumbnail = y.snippet.thumbnails.high.url,
                        Tags = await Task.FromResult(GetYouTubeVideosTags(y.id.videoId).Result.ToList()),
                    });

                    return (callresult, true, "successfuly");
                }

                return (new List<YoutubeVideos>(), false, "No matching videos...");
            }
            catch (HttpRequestException)
            {
                return (new List<YoutubeVideos>(), false, "Error fetching data...");
            }

        }

        private async Task<IEnumerable<YoutubeTags>> GetYouTubeVideosTags(string videoid)
        {
            var applicationyoutubeBaseUrl = _configuration["Youtubesearch:YouTubeBaseUrl"];
            var applicationyoutubekey = _configuration["Youtubesearch:Key"];

            if (_validation.ValidationForNull(applicationyoutubeBaseUrl))
                return (new List<YoutubeTags>());

            if (_validation.ValidationForNull(applicationyoutubekey))
                return (new List<YoutubeTags>());

            if (_validation.ValidationForNull(videoid))
                return (new List<YoutubeTags>());



            var youtubefullUrl = $"{applicationyoutubeBaseUrl}&key={applicationyoutubekey}&id={videoid}";

            //call the endpoit
            var clientResponse = await _httpClient.GetAsync(youtubefullUrl);
            try
            {
                clientResponse.EnsureSuccessStatusCode();

                //call the Deserialize the Json Object To c#
                using var responseReadStream = await clientResponse.Content.ReadAsStreamAsync();
                var responseOBJ = await JsonSerializer.DeserializeAsync<YoutubeTagsResponse>(responseReadStream);

                //Validate the Result
                if (responseOBJ?.items.Count() > 0)
                {
                    //Populate the Data
                    var callresult = responseOBJ.items.Select(y => new YoutubeTags
                    {
                        Tags = y.snippet.tags,
                    });

                    return (callresult);
                }

                return (new List<YoutubeTags>());
            }
            catch (HttpRequestException)
            {
                return (new List<YoutubeTags>());
            }

        }
    }
}
