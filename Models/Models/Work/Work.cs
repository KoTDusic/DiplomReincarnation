using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models
{
    [Table(Name = "TeacherWork")]
    public class Work : BaseIdModel
    {
        [Column("SubGroupId"), NotNull] public int SubGroupId { get; set; }

        [LinqToDB.Mapping.Association(ThisKey = nameof(SubGroupId), OtherKey = nameof(Id))]
        public Subgroup Subgroup { get; set; }

        [Column("DisciplineId"), NotNull] public int DisciplineId { get; set; }

        [LinqToDB.Mapping.Association(ThisKey = nameof(DisciplineId), OtherKey = nameof(Id))]
        public Discipline Discipline { get; set; }

        [Column("TeacherId"), NotNull] public int TeacherId { get; set; }

        [LinqToDB.Mapping.Association(ThisKey = nameof(TeacherId), OtherKey = nameof(Id))]
        public Teacher Teacher { get; set; }

        [NotColumn, Display(Name = "Преподаватели")]
        public IEnumerable<SelectListItem> Teachers { get; set; }

        [NotColumn, Display(Name = "Дисциплины")]
        public IEnumerable<SelectListItem> Disciplines { get; set; }

        public string FacultyName => Subgroup?.FacultyName;

        [NotColumn, Display(Name = "Специальность")]
        public string SpecialityName => Subgroup?.SpecialityName ?? Discipline?.SpecialityName;

        [NotColumn, Display(Name = "Дисциплина")]
        public string DisciplineName => Discipline?.DisciplineName;

        [NotColumn, Display(Name = "Имя преподавателя")]
        public string TeacherName => Teacher?.TeacherName;

        [NotColumn, Display(Name = "Номер группы")]
        public int GroupNumber => Subgroup?.GroupNumber ?? -1;

        [NotColumn, Display(Name = "Номер подгруппы")]
        public int SubgroupNumber => Subgroup?.SubGroupNumber ?? -1;

        [NotColumn, Display(Name = "Курс")] public int Coors => Subgroup?.Course ?? -1;
    }
}