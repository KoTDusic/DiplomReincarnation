using LinqToDB.Mapping;

namespace Models
{
    [Table("LabsProgress")]
    public class LabProgress : BaseIdModel
    {
        [Column("DisciplineId")]
        public int DisciplineId { get; set; }
        [Column("TeacherId")]
        public int TeacherId { get; set; }
        [Column("LabId")]
        public int LabId { get; set; }
        [Column("StudentId")]
        public int StudentId { get; set; }
        [Column("LabProgress")]
        public string LabStatus { get; set; }
    }
}