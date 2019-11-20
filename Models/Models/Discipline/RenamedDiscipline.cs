using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenamedDiscipline : Discipline
    {
        public RenamedDiscipline()
        {
        }

        public RenamedDiscipline(Discipline old)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            SpecialityId = old.SpecialityId;
            OldDisciplineName = old.DisciplineName;
        }
        
        [Display(Name = "Текущее название")]
        [NotColumn]
        public string OldDisciplineName { get; }
    }
}