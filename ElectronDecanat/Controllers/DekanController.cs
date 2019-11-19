using ElectronDecanat.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronDecanat.Controllers
{
    [Authorize(Roles = UserType.Decanat)]
    public class DekanController : Controller
    {
        public ActionResult Index()
        {
            //return View(UnitOfWork.Faculties.GetAll());
            return View();
        }
        public ActionResult Specialitis(int faculty_id)
        {
            //ViewBag.faculty_id = faculty_id;
            //ViewBag.faculty = UnitOfWork.Faculties.Get(faculty_id).Name;
            //return View(UnitOfWork.Specialitys.GetAll("where \"Код_факультета\"=" + faculty_id));
            return View();
        }
        public ActionResult Subgroups(int speciality_id)
        {
            //Speciality speciality = UnitOfWork.Specialitys.Get(speciality_id);
            //ViewBag.faculty_id = speciality.faculty_code;
            //ViewBag.faculty = speciality.faculty_name;
            //ViewBag.speciality_name = speciality.speciality_name;
            //ViewBag.speciality_number = speciality.speciality_number;
            //ViewBag.speciality_id = speciality.id;
            //return View(UnitOfWork.Subgroups.GetAll("where \"Код_специальности\"=" + speciality_id));
            return View();
        }
        public ActionResult Studing(int subgroup_id, int speciality_id)
        {
            //Subgroup subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            //ViewBag.faculty_name = subgroup.faculty_name;
            //ViewBag.speciality_name = subgroup.speciality_name;
            //ViewBag.speciality_number = subgroup.speciality_number;
            //ViewBag.speciality_id = subgroup.speciality_id;
            //ViewBag.coors = subgroup.coors;
            //ViewBag.group_number = subgroup.group_number;
            //ViewBag.subgroup_number = subgroup.subgroup_number;
            //return View(UnitOfWork.Studing.GetAll("where \"Код_специальности\"=" + speciality_id + " and \"Код_подгруппы\"=" + subgroup_id));
            return View();
        }
        public ActionResult DisciplineStuding(int student_id)
        {
            //Student student = UnitOfWork.Students.Get(student_id);
            //ViewBag.faculty_name = student.faculty_name;
            //ViewBag.speciality_name = student.speciality_name;
            //ViewBag.speciality_number = student.speciality_number;
            //ViewBag.speciality_id = student.speciality_id;
            //ViewBag.subgroup_id = student.subgroup_id;
            //ViewBag.coors = student.coors;
            //ViewBag.group_number = student.group_number;
            //ViewBag.subgroup_number = student.subgroup_number;
            //ViewBag.student_name = student.FIO;
            //return View(UnitOfWork.Studing.GetDisciplines("where \"Код_студента\"=" + student_id));
            return View();
        }
	}
}