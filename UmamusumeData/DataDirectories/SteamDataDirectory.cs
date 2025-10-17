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
    internal class SteamJapanDataDirectory : DataDirectory
    {
        private static readonly string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        private static readonly string dataDirectory = Path.Combine(
            programFilesX86, "Steam", "steamapps", "common",
            "UmamusumePrettyDerby_Jpn", "UmamusumePrettyDerby_Jpn_Data", "Persistent");

        public override string Name => "Steam (Japan)";

        public override bool Exists()
        {
            return CheckDirectory(dataDirectory);
        }
    }
}
