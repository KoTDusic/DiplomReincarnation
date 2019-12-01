using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "Teacher")]
    public class Teacher : BaseIdModel
    {
        [Column(Name = "TeacherName"), NotNull]
        [Display(Name = "Имя преподавателя"), Required(ErrorMessage = "Не заполнено имя преподавателя")]
        public string TeacherName { get; set; }
    }
}