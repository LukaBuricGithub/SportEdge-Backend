namespace SportEdge.API.Configuration
{
    /// <summary>
    /// Model used in appsettings.js to determine folder path for saved images and API path.
    /// </summary>
    public class ImageSettings
    {
        public string ImageFolderPath { get; set; }
        public string ImageRequestPath { get; set; }
    }
}
