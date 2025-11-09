namespace UmamusumeData.DataDirectories
{
    internal class KakaoDataDirectory : DataDirectory
    {
        private static readonly string kakaogamesDirectory = "C:\kakaogames";
        private static readonly string umamusumeDirectory = Path.Combine(kakaogamesDirectory, "umamusume");
        private static readonly string dataDirectory = Path.Combine(umamusumeDirectory, "client", "Assets")

        public override string Name => "Default (AppData)";

        public override bool Exists()
        {
           if (CheckDirectory())
            {
                DataDirectoryPath = dataDirectory;
                return true;
            }
            return false;
        }
    }
}
