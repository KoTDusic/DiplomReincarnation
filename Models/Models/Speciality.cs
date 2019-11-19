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

        [LinqToDB.Mapping.Association(ThisKey = nameof(FacultyId), OtherKey = nameof(Faculty.Id))]
        public Faculty FacultyFk { get; set; }
    }
}