using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenamedDiscipline : Discipline
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public RenamedDiscipline()
        {
        }

        public RenamedDiscipline(Discipline old) : base(old.Speciality)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            Speciality = old.Speciality;
            OldDisciplineName = old.DisciplineName;
        }

        [Display(Name = "Текущее название")]
        [NotColumn]
        public string OldDisciplineName { get; }
    }
}