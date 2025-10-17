namespace UmamusumeData.DataDirectories
{
    public abstract class DataDirectory
    {
        public abstract string Name { get; }

        public string DataDirectoryPath { get; protected set; } = "";

        public static bool CheckDirectory(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath)) return false;
            if (!Directory.Exists(Path.Combine(basePath, "dat"))) return false;
            if (!File.Exists(Path.Combine(basePath, "meta"))) return false;
            if (!File.Exists(Path.Combine(basePath, "master", "master.mdb"))) return false;

            return true;
        }

        public abstract bool Exists();
    }
}
