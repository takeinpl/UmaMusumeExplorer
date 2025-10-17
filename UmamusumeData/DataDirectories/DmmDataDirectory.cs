using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UmamusumeData.DataDirectories
{
    internal class DmmDataDirectory : DataDirectory
    {
        private static readonly string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string dmmDirectory = Path.Combine(appData, "dmmgameplayer5");
        private static readonly string dmmConfigPath = Path.Combine(dmmDirectory, "dmmgame.cnf");

        public override string Name => "DMM Games";

        public override bool Exists()
        {
            string configJson = File.ReadAllText(dmmConfigPath);

            JsonNode? json = JsonNode.Parse(configJson);
            JsonArray? contents = json?["contents"]?.AsArray();

            if (contents is null) return false;

            foreach (var product in contents)
            {
                if (product?["productId"]?.ToString() != "umamusume") continue;
                string? installDir = product?["detail"]?["path"]?.ToString();
                if (installDir is null) return false;
                string dataDir = Path.Combine(installDir, "umamusume_Data", "Persistent");
                if (CheckDirectory(dataDir))
                {
                    DataDirectoryPath = dataDir;
                    return true;
                }
                else
                    return false;
            }

            return false;
        }
    }
}
