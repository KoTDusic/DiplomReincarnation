using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "Speciality")]
    public class Speciality : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public Speciality()
        {
        }

        public Speciality(Faculty faculty)
        {
            FacultyId = faculty.Id;
            Faculty = faculty;
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // Orm setter
        [Column("FacultyId"), NotNull]
        public int FacultyId { get; set; }

        [Column(Name = "SpecialityName"), NotNull]
        [Display(Name = "Название специальности"), Required(ErrorMessage = "Не заполнено название специальности")]
        public string SpecialityName { get; set; }

        [Display(Name = "Факультет"), NotColumn]
        public string FacultyName => Faculty?.FacultyName;

//        [Display(Name = "Количество групп")]
//        [NotColumn]
//        public int GroupCount { get; set; }
        
        [LinqToDB.Mapping.Association(ThisKey = nameof(FacultyId), OtherKey = nameof(Id))]
        public Faculty Faculty { get; set; }
    }
}