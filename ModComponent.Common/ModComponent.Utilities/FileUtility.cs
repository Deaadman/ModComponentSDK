namespace ModComponent.Utilities
{
    public class FileUtility
    {
        public static string SanitizeFileName(string fileName)
        {
            return fileName.Replace(" ", "");
        }

        public static string SanitizeFileName(string fileName, bool lowercase)
        {
            return fileName.Replace(" ", "").ToLower();
        }
    }
}