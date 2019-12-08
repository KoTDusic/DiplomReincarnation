using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ElectronDecanat.Repozitory;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;


//https://metanit.com/sharp/mvc5/12.4.php роли


namespace ElectronDecanat.Controllers
{
    [Authorize(Roles = Teacher.TeacherRole)]
    public class TeacherController : BaseController
    {
        public TeacherController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IActionResult Labs(int discipline_id ,int subgroup_id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            var discipline = UnitOfWork.Disciplines.Get(discipline_id);
            ViewBag.coors = subgroup.Course;
            ViewBag.group_number = subgroup.GroupNumber;
            ViewBag.subgroup_number = subgroup.SubGroupNumber;
            ViewBag.discipline_name = discipline.DisciplineName;
            var id = GetCurrentUserId();
            IEnumerable<LabProgress> array = UnitOfWork.LabProgress.GetAll()
                .Where(progress => progress.TeacherId == id &&
                                   progress.DisciplineId == discipline_id && 
                                   progress.Student.SubGroupId==subgroup_id)
                .ToList();
            
            var groupedStudents = new Dictionary<string,List<LabProgress>>();
            for (var i = 0; i < array.Count(); i++)
            {
                if (!groupedStudents.ContainsKey(array.ElementAt(i).StudentName))
                {
                    groupedStudents.Add(array.ElementAt(i).StudentName, new List<LabProgress>());
                }
                
                groupedStudents.TryGetValue(array.ElementAt(i).StudentName, out var currentList);
                currentList?.Add(array.ElementAt(i));
            }

            var data = groupedStudents.OrderBy(element => element.Key).ToList();
            return View(data);
        }
        public IActionResult LabsList()
        {
            var test = UnitOfWork.Works.GetAll(works =>
                works.LoadWith(work => work.Discipline.Speciality)
                .Where(work => work.TeacherId == GetCurrentUserId()))
                .Select(work => work.Discipline).Distinct().ToList();
            return View(test);
        }

        public IActionResult LabsOnDisciplineList(int discipline_id)
        {
            var name = GetCurrentUserId().ToString();
            ViewBag.discipline_id = discipline_id;
            return View(UnitOfWork.Labs.GetAll(labs => labs
                .Where(lab => lab.DisciplineId == discipline_id)));
        }

        public ActionResult AddLab(int discipline_id)
        {
            var discipline = UnitOfWork.Disciplines.Get(discipline_id);
            var lab = new Lab
            {
                Discipline = discipline,
                DisciplineId = discipline.Id
            };
            return View(lab);
        }
        [HttpPost]
        public IActionResult AddLab(Lab item)
        {
            try
            {
                UnitOfWork.Labs.Create(item);
                return RedirectToAction("LabsOnDisciplineList", new { discipline_id = item.DisciplineId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось добавить лабораторную");
                return View(item);
            }
        }
        public IActionResult DeleteLab(int id)
        {
            var lab = UnitOfWork.Labs.Get(id);
            return View(lab);
        }
        [HttpPost]
        public IActionResult DeleteLab(Lab item)
        {
            try
            {
                UnitOfWork.Labs.Delete(item);
                return RedirectToAction("LabsOnDisciplineList", new { discipline_id = item.DisciplineId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить лабораторную");
                return View(item);
            }
        }
        public IActionResult EditLab(int id)
        {
            var lab = new RenameLab(UnitOfWork.Labs.Get(id));
            return View(lab);
        }
        [HttpPost]
        public IActionResult EditLab(RenameLab item)
        {
            try
            {
                UnitOfWork.Labs.Update(item);
                return RedirectToAction("LabsOnDisciplineList", new { discipline_id = item.DisciplineId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось изменить лабораторную");
                return View(item);
            }
        }
        public IActionResult ChangeLabStatus(int student_id, int lab_id)
        {
            var teacher = UnitOfWork.Teachers.Get(GetCurrentUserId());
            var lab = UnitOfWork.Labs.Get(lab_id);
            var student = UnitOfWork.Students.Get(student_id);
            var labProgress = UnitOfWork.LabProgress
                .GetAll()
                .FirstOrDefault(progress => progress.StudentId == student_id
                                            && progress.LabId == lab_id
                                            && progress.TeacherId == teacher.Id);
            labProgress = labProgress ?? new LabProgress
            {
                Teacher = teacher,
                TeacherId = teacher.Id,
                Student = student,
                StudentId = student.Id,
                Lab = lab,
                DisciplineId = lab.DisciplineId
            };
            
                return View(labProgress);
        }

        [HttpPost]
        public IActionResult ChangeLabStatus(LabProgress item)
        {
            var labProgress = UnitOfWork.LabProgress.Get(item.Id);
            if (labProgress != null)
            {
                UnitOfWork.LabProgress.Update(item);
            }
            else
            {
                UnitOfWork.LabProgress.Create(item);
            }

            return RedirectToAction("Labs", new
            {
                discipline_id = item.DisciplineId,
                subgroup_id = item.Student.SubGroupId
            });
        }

        public IActionResult Index()
        {
            var id = GetCurrentUserId();
            var items = UnitOfWork.Works.GetAll(
                works => works
                    .LoadWith(work => work.Teacher)
                    .LoadWith(work => work.Discipline.Speciality)
                    .LoadWith(work => work.Subgroup.Group)
                    .Where(work => work.TeacherId == id));
            return View(items);
        }    

        private int GetCurrentUserId()
        {
            return Convert.ToInt32(HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}