using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenameStudent : Student
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public RenameStudent()
        {
        }

        public RenameStudent(Student old) : base(old.Subgroup)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            OldFio = old.Fio;
        }

        [Display(Name = "Текущее ФИО")]
        [NotColumn]
        public string OldFio { get; }
    }
}