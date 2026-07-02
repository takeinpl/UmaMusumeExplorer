using SQLite;

namespace UmamusumeData.Tables
{
    [Table("support_card_group")]
    public class SupportCardGroup
    {
        [Column("id"), NotNull, PrimaryKey]
        public int Id { get; set; }

        [Column("support_card_id"), NotNull]
        public int SupportCardId { get; set; }

        [Column("chara_id"), NotNull]
        public int CharaId { get; set; }

        [Column("outing_max"), NotNull]
        public int OutingMax { get; set; }
    }
}