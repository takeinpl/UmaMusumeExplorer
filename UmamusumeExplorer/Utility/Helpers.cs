using Microsoft.VisualBasic.Devices;
using System.Text;
using UmamusumeData;
using UmamusumeData.Enums;
using UmamusumeData.Tables;
using UmamusumeExplorer.Assets;

namespace UmamusumeExplorer.Utility
{
    internal static class Helpers
    {
        public static IEnumerable<LivePermissionData> GetLivePermissionData(int musicId)
        {
            List<LivePermissionData> livePermissionData = AssetTables.LivePermissionDatas.Where(lpd => lpd.MusicId == musicId).ToList();

            var matches = UmaDataHelper.GetManifestEntries(ga => ga.BaseName.StartsWith($"snd_bgm_live_{musicId}_chara_") && ga.BaseName.EndsWith(".awb"));
            foreach (var audioAsset in matches)
            {
                int charaId = int.Parse(audioAsset.BaseName.Remove(0, $"snd_bgm_live_{musicId}_chara_".Length)[..4]);

                if (livePermissionData is not List<LivePermissionData> list) continue;
                list.Add(new LivePermissionData() { MusicId = musicId, CharaId = charaId });
            }

            return livePermissionData;
        }

        public static string GetCharaName(int id, bool includeId = false, bool includeEnglishName = false)
        {
            StringBuilder charaNameBuilder = new();

            if (includeId) charaNameBuilder.Append($"{id:d4}: ");
            string charaName = AssetTables.TextDatas.First(td => td.Index == id && td.Category == (int)TextCategory.MasterCharaName).Text;
            charaNameBuilder.Append(charaName);
            if (includeEnglishName)
            {
                string? englishName = AssetTables.TextDatas.FirstOrDefault(td => td.Index == id && td.Category == (int)TextCategory.CharaName_En)?.Text;
                if (englishName is not null)
                {
                    charaNameBuilder.Append($" ({englishName})");
                }
            }

            return charaNameBuilder.ToString();
        }
    }
}
