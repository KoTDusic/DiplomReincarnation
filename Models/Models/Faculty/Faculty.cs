using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "Faculty")]
    public class Faculty : BaseIdModel
    {
        [Column(Name = "FacultyName"), NotNull]
        [Display(Name = "Название факультета"), Required(ErrorMessage = "Не заполнено название факультета")]
        public string FacultyName { get; set; }
    }
}