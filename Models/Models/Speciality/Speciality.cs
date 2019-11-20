using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "Speciality")]
    public class Speciality : BaseIdModel
    {
        [Column("FacultyId"), NotNull]
        public int FacultyId { get; set; }

        [Column(Name = "SpecialityName"), NotNull]
        [Display(Name = "Название специальности"), Required(ErrorMessage = "Не заполнено название специальности")]
        public string SpecialityName { get; set; }

        [Display(Name = "Факультет")]
        [NotColumn]
        public string FacultyName { get; set; }
    }
}