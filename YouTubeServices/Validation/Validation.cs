namespace YouTubeServices.Validation
{
    public class Validation : IValidation
    {
        bool IValidation.ValidationForNull(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return true;


            if (string.IsNullOrWhiteSpace(keyword))
                return true;


            return false;
        }
    }
}
