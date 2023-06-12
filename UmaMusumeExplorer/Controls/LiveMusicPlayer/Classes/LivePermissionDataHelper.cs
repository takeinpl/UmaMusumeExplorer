﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmaMusumeData;
using UmaMusumeData.Tables;
using UmaMusumeExplorer.Game;

namespace UmaMusumeExplorer.Controls.LiveMusicPlayer.Classes
{
    static class LivePermissionDataHelper
    {
        public static IEnumerable<LivePermissionData> GetLivePermissionData(int musicId)
        {
            IEnumerable<LivePermissionData> livePermissionData = AssetTables.LivePermissionDatas.Where(lpd => lpd.MusicId == musicId);
            if (!livePermissionData.Any())
            {
                livePermissionData = new List<LivePermissionData>();

                var matches = UmaDataHelper.GetGameAssetDataRows(ga => ga.BaseName.StartsWith($"snd_bgm_live_{musicId}_chara_") && ga.BaseName.EndsWith(".awb"));

                foreach (var audioAsset in matches)
                {
                    int charaId = int.Parse(audioAsset.BaseName.Remove(0, $"snd_bgm_live_{musicId}_chara_".Length)[..4]);

                    (livePermissionData as List<LivePermissionData>).Add(new LivePermissionData() { MusicId = musicId, CharaId = charaId });
                }
            }

            return livePermissionData;
        }
    }
}
