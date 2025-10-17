using UmamusumeData;
using UmamusumeData.Tables;
using UmamusumeExplorer.Assets;

namespace UmamusumeExplorer.Controls
{
    class CostumeComboBoxItem
    {
        public CostumeComboBoxItem(CardData cardData)
        {
            CardData = cardData;
        }

        public CardData CardData { get; }

        public override string ToString()
        {
            return AssetTables.GetText(TextCategory.MasterCardTitleName, CardData.Id);
        }
    }
}
