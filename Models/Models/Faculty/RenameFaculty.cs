﻿using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenameFaculty : Faculty
    {
        public RenameFaculty()
        {
        }

        public RenameFaculty(Faculty old)
        {
            if (old == null)
            {
                throw new ArgumentException(nameof(old));
            }

            Id = old.Id;
            OldFacultyName = old.FacultyName;
        }

        [Display(Name = "Текущее название")]
        [NotColumn]
        public string OldFacultyName { get; }
    }
}