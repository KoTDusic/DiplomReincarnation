using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenamedSpeciality : Speciality
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public RenamedSpeciality()
        {
        }

        public RenamedSpeciality(Speciality old) : base(old.Faculty)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            OldSpecialityName = old.SpecialityName;
            Faculty = old.Faculty;
        }

        [Display(Name = "Текущее название")]
        [NotColumn]
        public string OldSpecialityName { get; }
    }
}