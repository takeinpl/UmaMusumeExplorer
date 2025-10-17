using UmamusumeData.Tables;

namespace UmamusumeExplorer.Controls
{
    internal class BgmComboBoxItem
    {
        public RaceBgm? RaceBgm { get; set; }

        public override string ToString()
        {
            return RaceBgm?.Id.ToString() ?? "";
        }
    }
}
