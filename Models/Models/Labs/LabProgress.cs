﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models
{
    [Table("LabsProgress")]
    public class LabProgress : BaseIdModel
    {
        // ReSharper disable once UnusedMember.Global
        // OrmCtor
        public LabProgress()
        {
        }
        [Column("DisciplineId")]
        public int DisciplineId { get; set; }
        [Column("TeacherId")]
        public int TeacherId { get; set; }
        [Column("LabId")]
        public int LabId { get; set; }
        [Column("StudentId")]
        public int StudentId { get; set; }
        [Column("LabProgress")]
        public LabStatus LabStatus { get; set; }
        
        [Display(Name = "Имя студента")]
        [NotColumn]
        public string StudentName => Student?.Fio;
        
        [Display(Name = "Имя преподавателя")]
        [NotColumn]
        public string TeacherName => Teacher?.TeacherName;
        
        [Display(Name = "Название дисциплины")]
        [NotColumn]
        public string DisciplineName  => Discipline?.DisciplineName;

        [Display(Name = "Название лабораторной")]
        [NotColumn]
        public string LabName => Lab?.LabName;

        [LinqToDB.Mapping.Association(ThisKey = nameof(DisciplineId), OtherKey = nameof(Id))]
        public Discipline Discipline { get; set; }
        
        [LinqToDB.Mapping.Association(ThisKey = nameof(LabId), OtherKey = nameof(Id))]
        public Lab Lab { get; set; }
        
        [LinqToDB.Mapping.Association(ThisKey = nameof(TeacherId), OtherKey = nameof(Id))]
        public Teacher Teacher { get; set; }
        
        [LinqToDB.Mapping.Association(ThisKey = nameof(StudentId), OtherKey = nameof(Id))]
        public Student Student { get; set; }
        public static IEnumerable<SelectListItem> Statuses { get; } = GetAllStatus();

        private static IEnumerable<SelectListItem> GetAllStatus()
        {
            var selectList = new List<SelectListItem>
            {
                new SelectListItem {Value = "Сдано", Text = "Сдано"},
                new SelectListItem {Value = "Не сдано", Text = "Не сдано"},
                new SelectListItem {Value = "Проверяется", Text = "Проверяется"}
            };
            return selectList;
        }
    }
}