using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ElectronDecanat.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public IActionResult Index(string name)
        {
            //if (!String.IsNullOrWhiteSpace(name))
            //{
            //    ViewBag.querry = name;
            //    return View(UnitOfWork.Students.GetAll("where \"ФИО\" LIKE '%" + name + "%'"));
            //}
            //else
            //{
            //    ViewBag.querry = "";
            //    return View(new List<Student>()); 
            //}
            return View();
        }
        public IActionResult Student(int id, string quarry)
        {
            //ViewBag.quarry = quarry;
            //IEnumerable<LabProgress> list = UnitOfWork.LabProgress.GetAll("where \"Код_студента\"=" + id + " order by \"Наименование_дисциплины\"");
            //if(list.Count()!=0)
            //{
            //    ViewBag.FIO = list.ElementAt(0).student_name;
            //    ViewBag.coors = list.ElementAt(0).coors;
            //    ViewBag.group_number = list.ElementAt(0).group_number;
            //    ViewBag.subgroop_number = list.ElementAt(0).subgroop_number;
            //}
            //return View(list);
            return View();
        }
    }
}