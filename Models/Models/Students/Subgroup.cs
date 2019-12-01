using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    [Table(Name = "SubGroups")]
    public class Subgroup : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public Subgroup()
        {
        }

        public Subgroup(Group group)
        {
            GroupId = group.Id;
            Group = group;
        }

        [Required, Display(Name = "Подгруппа"), Column("SubGroupNumber"), NotNull]
        public int SubGroupNumber { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // Orm setter
        [Column("GroupId"), NotNull] public int GroupId { get; set; }

        [NotColumn, Display(Name = "Факультет")]
        public string FacultyName => Group?.FacultyName;

        [NotColumn, Display(Name = "Специальность")]
        public string SpecialityName => Group?.SpecialityName;

        [NotColumn, Display(Name = "Номер группы")]
        public int GroupNumber => Group?.GroupNumber ?? -1;

        [NotColumn, Display(Name = "Курс")] public int Course => Group?.Coors ?? -1;

        [LinqToDB.Mapping.Association(ThisKey = nameof(GroupId), OtherKey = nameof(Id))]
        public Group Group { get; set; }

//        [Display(Name = "Количество человек")]
//        [NotColumn]
//        public int PeoplesCount { get; set; }
    }
}