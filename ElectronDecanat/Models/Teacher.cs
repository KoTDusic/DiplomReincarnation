using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace ElectronDecanat.Models
{
    [Table(Name = "Teachers")]
    public class Teacher
    {
        [Column(Name = "Id"), PrimaryKey, Identity]
        public int? Id { get; set; }

        [Column(Name = "Username"), NotNull]
        [Display(Name = "Имя преподавателя"), Required(ErrorMessage = "Не заполнено имя преподавателя")]
        public string TeacherName { get; set; }
    }
}
