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
    internal class KakaoClientDataDirectory : DataDirectory
    {
        private static readonly string kakaoGamesDirectory = "C:\kakaogames"
        private static readonly string dataDirectory = Path.Combine(kakaoGamesDirectory, "umamusume", "client", "Assets");

        public override string Name => "Default (Korea)";

        public override bool Exists()
        {
            return CheckDirectory(dataDirectory);
        }
    }
}
