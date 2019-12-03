using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenameTeacher : Teacher
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public RenameTeacher()
        {
        }

        public RenameTeacher(Teacher old)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            OldTeacherName = old.TeacherName;
            Username = old.Username;
            Role = old.Role;
        }

        [Display(Name = "Текущее имя")]
        [NotColumn]
        public string OldTeacherName { get; }
    }
}