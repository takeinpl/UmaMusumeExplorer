namespace UmamusumeData.DataDirectories
{
    internal class DefaultDataDirectory : DataDirectory
    {
        private static readonly string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string dataDirectory = Path.Combine(appData + "Low", "Cygames", "umamusume");

        public override string Name => "Default (AppData)";

        public override bool Exists()
        {
            bool exists = CheckDirectory(dataDirectory);

            if (exists)
                DataDirectoryPath = dataDirectory;

            return exists;
        }
    }
}
