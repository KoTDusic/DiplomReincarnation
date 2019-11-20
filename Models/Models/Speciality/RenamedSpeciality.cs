using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenamedSpeciality : Speciality
    {
        public RenamedSpeciality()
        {
        }

        public RenamedSpeciality(Speciality old)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            OldSpecialityName = old.SpecialityName;
            FacultyName = old.FacultyName;
            FacultyId = old.FacultyId;
        }

        [Display(Name = "Текущее название")]
        [NotColumn]
        public string OldSpecialityName { get; }
    }
}