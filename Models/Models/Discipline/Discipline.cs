using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table("Discipline")]
    public class Discipline : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public Discipline()
        {
        }

        public Discipline(Speciality speciality)
        {
            SpecialityId = speciality.Id;
            Speciality = speciality;
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // Orm setter
        [Column("SpecialityId")]
        public int SpecialityId { get; set; }

        [Column("DisciplineName")]
        [Display(Name = "Имя дисциплины"), NotNull]
        public string DisciplineName { get; set; }
        
        [NotColumn, Display(Name = "Факультет")]
        public string FacultyName => Speciality?.FacultyName;

        [NotColumn, Display(Name = "Специальность")]
        public string SpecialityName => Speciality?.SpecialityName;
        
        [LinqToDB.Mapping.Association(ThisKey = nameof(SpecialityId), OtherKey = nameof(Id))]
        public Speciality Speciality { get; set; }
    }
}