using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmamusumeData
{
    public enum ManifestEntryKind : int
    {
        Default,
        AssetManifest,
        PlatformManifest,
        RootManifest,
        Master = 10,
        Sound,
        Movie,
        Font
    }
}
