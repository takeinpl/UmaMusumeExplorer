﻿using System.Collections.Generic;
using UmaMusumeData;
using UmaMusumeData.Tables;

namespace UmaMusumeExplorer.Game
{
    static class AssetTables
    {
        private static readonly IEnumerable<GameAsset> audioGameFiles = UmaDataHelper.GetGameAssetDataRows(ga => ga.Name.StartsWith("sound/"));

        private static readonly IEnumerable<AvailableSkillSet> availableSkillSets = UmaDataHelper.GetMasterDatabaseRows<AvailableSkillSet>();

        private static readonly IEnumerable<CardData> cardDatas = UmaDataHelper.GetMasterDatabaseRows<CardData>();
        private static readonly IEnumerable<CardRarityData> cardRarityDatas = UmaDataHelper.GetMasterDatabaseRows<CardRarityData>();
        private static readonly IEnumerable<CharaData> charaDatas = UmaDataHelper.GetMasterDatabaseRows<CharaData>();

        private static readonly IEnumerable<JukeboxMusicData> jukeboxMusicDatas = UmaDataHelper.GetMasterDatabaseRows<JukeboxMusicData>();

        private static readonly IEnumerable<LiveData> liveDatas = UmaDataHelper.GetMasterDatabaseRows<LiveData>();
        private static readonly IEnumerable<LivePermissionData> livePermissionDatas = UmaDataHelper.GetMasterDatabaseRows<LivePermissionData>();

        private static readonly IEnumerable<RaceBgm> raceBgm = UmaDataHelper.GetMasterDatabaseRows<RaceBgm>();
        private static readonly IEnumerable<RaceBgmPattern> raceBgmPatterns = UmaDataHelper.GetMasterDatabaseRows<RaceBgmPattern>();

        private static readonly IEnumerable<SkillSet> skillSets = UmaDataHelper.GetMasterDatabaseRows<SkillSet>();
        private static readonly IEnumerable<SkillData> skillDatas = UmaDataHelper.GetMasterDatabaseRows<SkillData>();

        private static readonly IEnumerable<TextData> charaNameTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 170);
        private static readonly IEnumerable<TextData> charaNameKatakanaTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 182);
        private static readonly IEnumerable<TextData> charaVoiceNameTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 7);
        private static readonly IEnumerable<TextData> charaCostumeNameTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 5);

        private static readonly IEnumerable<TextData> liveNameTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 16);
        private static readonly IEnumerable<TextData> liveInfoTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 17);

        private static readonly IEnumerable<TextData> skillNameTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 47);
        private static readonly IEnumerable<TextData> skillInfoTextDatas = UmaDataHelper.GetMasterDatabaseRows<TextData>(td => td.Category == 48);

        public static IEnumerable<GameAsset> AudioAssets => audioGameFiles;

        public static IEnumerable<AvailableSkillSet> AvailableSkillSets => availableSkillSets;

        public static IEnumerable<CardData> CardDatas => cardDatas;
        public static IEnumerable<CardRarityData> CardRarityDatas => cardRarityDatas;
        public static IEnumerable<CharaData> CharaDatas => charaDatas;

        public static IEnumerable<JukeboxMusicData> JukeboxMusicDatas => jukeboxMusicDatas;

        public static IEnumerable<LiveData> LiveDatas => liveDatas;
        public static IEnumerable<LivePermissionData> LivePermissionDatas => livePermissionDatas;

        public static IEnumerable<RaceBgm> RaceBgm => raceBgm;
        public static IEnumerable<RaceBgmPattern> RaceBgmPatterns => raceBgmPatterns;

        public static IEnumerable<SkillSet> SkillSets => skillSets;
        public static IEnumerable<SkillData> SkillDatas => skillDatas;

        public static IEnumerable<TextData> CharaCostumeNameTextDatas => charaCostumeNameTextDatas;
        public static IEnumerable<TextData> CharaNameKatakanaTextDatas => charaNameKatakanaTextDatas;
        public static IEnumerable<TextData> CharaNameTextDatas => charaNameTextDatas;
        public static IEnumerable<TextData> CharaVoiceNameTextDatas => charaVoiceNameTextDatas;

        public static IEnumerable<TextData> LiveNameTextDatas => liveNameTextDatas;
        public static IEnumerable<TextData> LiveInfoTextDatas => liveInfoTextDatas;

        public static IEnumerable<TextData> SkillNameTextDatas => skillNameTextDatas;
        public static IEnumerable<TextData> SkillInfoTextDatas => skillInfoTextDatas;
    }
}
