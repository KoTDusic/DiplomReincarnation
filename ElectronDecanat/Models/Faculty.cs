using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace ElectronDecanat.Models
{
    [Table(Name = "Faculties")]
    public class Faculty
    {
        [Column("Id"), PrimaryKey, Identity]
        public int? Id { get; set; }

        [Column(Name = "Username"), NotNull]
        [Display(Name = "Название факультета"), Required(ErrorMessage = "Не заполнено название факультета")]
        public string FacultyName { get; set; }
    }
}