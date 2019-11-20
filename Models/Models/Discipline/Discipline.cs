using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table("Discipline")]
    public class Discipline : BaseIdModel
    {
        [Column("SpecialityId")] 
        public int SpecialityId { get; set; }
        
        [Column("DisciplineName")]
        [Display(Name = "Имя дисциплины"), NotNull]
        public string DisciplineName { get; set; }
        
        [NotColumn]
        [Display(Name = "Факультет")]
        public string FacultyName { get; set; }
        
        [NotColumn]
        [Display(Name = "Специальность")]
        public string SpecialityName { get; set; }
    }
}