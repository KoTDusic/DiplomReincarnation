using System.Linq;
using ElectronDecanat.Auth;
using ElectronDecanat.Repozitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronDecanat.Controllers
{
    [Authorize(Roles = UserType.Decanat)]
    public class DekanController : BaseController
    {
    
        public DekanController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public ActionResult Index()
        {
            return View(UnitOfWork.Faculties.GetAll());
        }
        public ActionResult Specialitis(int faculty_id)
        {
            ViewBag.faculty_id = faculty_id;
            ViewBag.faculty = UnitOfWork.Faculties.Get(faculty_id).FacultyName;
            return View(UnitOfWork.Specialitys.GetAll().Where(speciality => speciality.FacultyId==faculty_id).ToList());
        }
        public ActionResult Subgroups(int speciality_id)
        {
            var speciality = UnitOfWork.Specialitys.Get(speciality_id);
//            ViewBag.faculty_id = speciality.FacultyId;
//            ViewBag.faculty = speciality.FacultyName;
//            ViewBag.speciality_name = speciality.SpecialityName;
//            ViewBag.speciality_id = speciality.Id;
//            return View(UnitOfWork.Subgroups.GetAll().Where(subgroup => subgroup.))("where \"Код_специальности\"=" + speciality_id));
            throw new System.NotImplementedException();
        }
        public ActionResult Studing(int subgroup_id, int speciality_id)
        {
            //Subgroup subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            //ViewBag.faculty_name = subgroup.faculty_name;
            //ViewBag.speciality_name = subgroup.speciality_name;

            //ViewBag.speciality_id = subgroup.speciality_id;
            //ViewBag.coors = subgroup.coors;
            //ViewBag.group_number = subgroup.group_number;
            //ViewBag.subgroup_number = subgroup.subgroup_number;
            //return View(UnitOfWork.Studing.GetAll("where \"Код_специальности\"=" + speciality_id + " and \"Код_подгруппы\"=" + subgroup_id));
            throw new System.NotImplementedException();
        }
        public ActionResult DisciplineStuding(int student_id)
        {
            //Student student = UnitOfWork.Students.Get(student_id);
            //ViewBag.faculty_name = student.faculty_name;
            //ViewBag.speciality_name = student.speciality_name;
            //ViewBag.speciality_id = student.speciality_id;
            //ViewBag.subgroup_id = student.subgroup_id;
            //ViewBag.coors = student.coors;
            //ViewBag.group_number = student.group_number;
            //ViewBag.subgroup_number = student.subgroup_number;
            //ViewBag.student_name = student.FIO;
            //return View(UnitOfWork.Studing.GetDisciplines("where \"Код_студента\"=" + student_id));
            throw new System.NotImplementedException();
        }
    }
}