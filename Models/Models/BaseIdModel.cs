using LinqToDB.Mapping;

namespace Models
{
    public class BaseIdModel
    {
        [Column("Id"), PrimaryKey, Identity]
        public int Id { get; set; }
    }
}