using System;
using System.Collections.Generic;
using System.Linq;
using ElectronDecanat.Repozitory;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ElectronDecanat.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Student
        public IActionResult Index(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ViewBag.querry = name;
                var searchResult = UnitOfWork.Students.GetAll(
                    students => students.LoadWith(student => student.Subgroup.Group.Speciality.Faculty)
                        .Where(student => student.Fio.Contains(name)));
                return View(searchResult);
            }
            else
            {
                ViewBag.querry = "";
                return View(UnitOfWork.Students.GetAll(
                    students => students.LoadWith(student => student.Subgroup.Group.Speciality.Faculty)));
            }
        }

        public IActionResult Student(int id, string quarry)
        {
            ViewBag.quarry = quarry;
            var list = UnitOfWork.LabProgress.GetAll(
                p => p.LoadWith(progress => progress.Discipline)
                    .Where(progress => progress.StudentId == id)
                    .OrderBy(progress => progress.Discipline.DisciplineName));
            if (list.Count != 0)
            {
                ViewBag.FIO = list.ElementAt(0).StudentName;
//                ViewBag.coors = list.ElementAt(0).coors;
//                ViewBag.group_number = list.ElementAt(0).group_number;
//                ViewBag.subgroop_number = list.ElementAt(0).subgroop_number;
            }

            return View(list);
        }
    }
}