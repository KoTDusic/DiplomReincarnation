using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "Groups")]
    public class Group : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public Group()
        {
        }
        
        public Group(Speciality speciality)
        {
            SpecialityId = speciality.Id;
            Speciality = speciality;
        }
        
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // Orm setter
        [Column("SpecialityId"), NotNull]
        public int SpecialityId { get; set; }

        [Required]
        [Display(Name = "Год поступления")]
        [Column("CommingYear"), NotNull]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Номер группы")]
        [Column("GroupNumber"), NotNull]
        public int GroupNumber { get; set; }

        [NotColumn, Display(Name = "Курс")]
        public int Coors => DateTime.Today.Year - Year;

        [NotColumn, Display(Name = "Факультет")]
        public string FacultyName => Speciality?.FacultyName;

        [NotColumn, Display(Name = "Специальность")]
        public string SpecialityName => Speciality?.SpecialityName;

        [LinqToDB.Mapping.Association(ThisKey = nameof(SpecialityId), OtherKey = nameof(Id))]
        public Speciality Speciality { get; set; }

//        [NotColumn]
//        [Display(Name = "Количество подгрупп")]
//        public int SubgroupsCount { get; set; }
    }
}