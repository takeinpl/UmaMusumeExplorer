using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmamusumeData;
using UmamusumeData.Tables;
using UmamusumeExplorer.Assets;

namespace UmamusumeExplorer.Utility
{
    internal static class TextHelpers
    {
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
