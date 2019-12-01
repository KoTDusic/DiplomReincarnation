using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table("Labs")]
    public class Lab : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public Lab()
        {
        }
        public int DisciplineId { get; set; }

        [Display(Name = "Лабораторная")] 
        public string LabName { get; set; }
        [NotColumn]
        [Display(Name = "Специальность")]
        public string SpecialityName => Discipline?.SpecialityName;

        [NotColumn]
        [Display(Name = "Дисциплина")]
        public string DisciplineName => Discipline?.DisciplineName;
        
        [LinqToDB.Mapping.Association(ThisKey = nameof(DisciplineId), OtherKey = nameof(Id))]
        public Discipline Discipline { get; set; }
    }
}