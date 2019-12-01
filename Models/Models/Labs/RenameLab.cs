using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenameLab : Lab
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public RenameLab()
        {
        }

        public RenameLab(Lab old)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            DisciplineId = old.DisciplineId;
            OldLabName = old.LabName;
        }

        [Display(Name = "Текущее имя")]
        [NotColumn]
        public string OldLabName { get; }
    }
}