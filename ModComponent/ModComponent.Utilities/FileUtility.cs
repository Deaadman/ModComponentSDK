namespace ModComponent.Utilities
{
    internal class FileUtility
    {
        internal static string SanitizeFileName(string fileName)
        {
            return fileName.Replace(" ", "");
        }

        internal static string SanitizeFileName(string fileName, bool lowercase)
        {
            return fileName.Replace(" ", "").ToLower();
        }
    }
}