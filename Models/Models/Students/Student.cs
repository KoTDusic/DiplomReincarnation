using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "Students")]
    public class Student : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public Student()
        {
        }

        public Student(Subgroup subgroup)
        {
            SubGroupId = subgroup.Id;
            Subgroup = subgroup;
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // Orm setter
        [Column("SubGroupId"), NotNull] public int SubGroupId { get; set; }

        [Column(Name = "StudentName"), NotNull]
        [Display(Name = "Фио студента"), Required(ErrorMessage = "Не заполнено Фио студента")]
        public string Fio { get; set; }

        [NotColumn, Display(Name = "Факультет")]
        public string FacultyName => Subgroup?.FacultyName;

        [NotColumn, Display(Name = "Специальность")]
        public string SpecialityName => Subgroup?.SpecialityName;

        [NotColumn, Display(Name = "Номер группы")]
        public int GroupNumber => Subgroup?.GroupNumber ?? -1;

        [NotColumn, Display(Name = "Номер подгруппы")]
        public int SubgroupNumber => Subgroup?.SubGroupNumber ?? -1;

        [NotColumn, Display(Name = "Курс")] public int Course => Subgroup?.Course ?? -1;

        [LinqToDB.Mapping.Association(ThisKey = nameof(SubGroupId), OtherKey = nameof(Id))]
        public Subgroup Subgroup { get; set; }
    }
}