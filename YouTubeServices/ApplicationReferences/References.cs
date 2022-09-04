using System.Reflection;

namespace YouTubeServices.ApplicationReferences
{
    public class References
    {
        const string Name = "YouTube Search Application";
        const string Version = "0.0.0.1";
        static readonly string VersionDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("dddd, dd MMMM yyyy hh:mm tt");
        static readonly string LastUpdates = $" {Version}, Last Updated {VersionDate}.";
        const string Year = "2022-2022";
        const string DevelopedBy = "Fahmy";
        const string MainLogo = "/images/youtubeApplication.jpg";
        const string PageLogo = "/images/youtubelogo.jpg";

        public static DateTime Currentdatatime => DateTime.Now;
        public static string ApplicationName => Name;
        public static string ApplicationVersion => LastUpdates;
        public static string ApplicationYear => Year;
        public static string ApplicationDevelopedBy => DevelopedBy;
        
        public static string ApplicationMainLogo => MainLogo;
        public static string ApplicationPageLogo => PageLogo;
    }
}
