﻿using ElectronDecanat.Repozitory;
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
        // GET: Teacher
//        public IActionResult Index()
//        {
//            //return View(UnitOfWork.Works.GetAll("where \"Код_преподавателя\"='"+RegistrationUser.Identity.GetUserId()+"'"));
//            return View();
//        }
        public IActionResult Labs(int discipline_id ,int subgroup_id)
        {
            //Subgroup subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            //Discipline discipline = UnitOfWork.Disciplines.Get(discipline_id);
            //ViewBag.coors = subgroup.coors;
            //ViewBag.group_number = subgroup.group_number;
            //ViewBag.subgroup_number = subgroup.subgroup_number;
            //ViewBag.discipline_name = discipline.discipline_name;
            //IEnumerable<LabProgress> array = UnitOfWork.LabProgress.GetAll("WHERE \"Код_преподавателя\" = '" + RegistrationUser.Identity.GetUserId() + "' AND \"Код_дисциплины\"=" + discipline_id + " and \"Код_подгруппы\"=" + subgroup_id);
            //Dictionary<string,List<LabProgress>> grouped_students = new Dictionary<string,List<LabProgress>>();
            //List<LabProgress> current_list;
            //for(int i=0;i<array.Count();i++)
            //{
            //    if(!grouped_students.ContainsKey(array.ElementAt(i).student_name))
            //        grouped_students.Add(array.ElementAt(i).student_name, new List<LabProgress>());
            //    grouped_students.TryGetValue(array.ElementAt(i).student_name, out current_list);
            //    current_list.Add(array.ElementAt(i));
            //}
            //var data = grouped_students.OrderBy(element => element.Key).ToList();
            //return View(grouped_students.OrderBy(element => element.Key).ToList());
            return View();
        }
        public IActionResult LabsList()
        {
            //List<Discipline> disciplines = UnitOfWork.Disciplines.getTeacherDisciplines(RegistrationUser.Identity.GetUserId());
            //return View(disciplines);
            return View();
        }
        public IActionResult LabsOnDisciplineList(int discipline_id)
        {
            //string name = RegistrationUser.Identity.GetUserId();
            //ViewBag.discipline_id = discipline_id;
            //return View(UnitOfWork.Labs.GetAll("where \"Код_дисциплины\" =" + discipline_id));
            return View();
        }
        public ActionResult AddLab(int discipline_id)
        {
            //Discipline discipline = UnitOfWork.Disciplines.Get(discipline_id);
            //Lab lab = new Lab()
            //{
            //    discipline = discipline.discipline_name,
            //    discipline_id = discipline.id,
            //    speciality = discipline.speciality_name
            //};
            //return View(lab);
            return View();
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
        public IActionResult ChangeLabStatus(int id)
        {
            var lab_progress = UnitOfWork.LabProgress.Get(id);
            return View(lab_progress);
        }
        [HttpPost]
        public IActionResult ChangeLabStatus(LabProgress item)
        {
            UnitOfWork.LabProgress.Update(item);
            throw new System.NotImplementedException();
//            return RedirectToAction("Labs", new { discipline_id = item.DisciplineId, subgroup_id=item.subgroop_id });
        }

        public IActionResult Index()
        {
            throw new System.NotImplementedException();
        }
    }
}