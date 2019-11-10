﻿using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace ElectronDecanat.Models
{
    [Table(Name = "Specialties")]
    public class Speciality
    {
        [Column(Name = "Id"), PrimaryKey, Identity]
        public int? Id { get; set; }

        [Column("FacultyId"), NotNull]
        public int FacultyId { get; set; }

        [Column(Name = "Username"), NotNull]
        [Display(Name = "Название специальности"), Required(ErrorMessage = "Не заполнено название специальности")]
        public string SpecialityName { get; set; }

        [LinqToDB.Mapping.Association(ThisKey = nameof(FacultyId), OtherKey = nameof(Faculty.Id))]
        public Faculty FacultyFk { get; set; }


    }
}