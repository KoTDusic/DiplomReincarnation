using System.Linq;
using ElectronDecanat.Repozitory;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ElectronDecanat.Controllers
{
    [Authorize(Roles = Teacher.DecanatRole)]
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
            return View(
                UnitOfWork.Specialitys.GetAll().Where(speciality => speciality.FacultyId == faculty_id).ToList());
        }

        public ActionResult Subgroups(int speciality_id)
        {
            var speciality = UnitOfWork.Specialitys.Get(speciality_id);
            ViewBag.faculty_id = speciality.FacultyId;
            ViewBag.faculty = speciality.FacultyName;
            ViewBag.speciality_name = speciality.SpecialityName;
            ViewBag.speciality_id = speciality.Id;
            return View(UnitOfWork.Subgroups.GetAll(table =>
                table.LoadWith(subgroup => subgroup.Group)
                    .Where(subgroup => subgroup.Group.SpecialityId == speciality_id)));
            ;
        }

        public ActionResult Studying(int subgroup_id, int speciality_id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            ViewBag.faculty_name = subgroup.FacultyName;
            ViewBag.speciality_name = subgroup.SpecialityName;

            ViewBag.speciality_id = subgroup.Group.SpecialityId;
            ViewBag.coors = subgroup.Course;
            ViewBag.group_number = subgroup.GroupNumber;
            ViewBag.subgroup_number = subgroup.SubGroupNumber;
            var items = UnitOfWork.Studying.GetAll()
                .Where(study => study.SpecialityId == speciality_id && study.SubgroupId == subgroup_id)
                .ToList();
            return View(items);
        }

        public ActionResult DisciplineStudying(int student_id)
        {
            Student student = UnitOfWork.Students.Get(student_id);
            ViewBag.faculty_name = student.FacultyName;
            ViewBag.speciality_name = student.SpecialityName;
            ViewBag.speciality_id = student.Subgroup.Group.SpecialityId;
            ViewBag.subgroup_id = student.Subgroup.Id;
            ViewBag.coors = student.Course;
            ViewBag.group_number = student.GroupNumber;
            ViewBag.subgroup_number = student.SubgroupNumber;
            ViewBag.student_name = student.Fio;
            return View((UnitOfWork.DisciplineStudying as DisciplineStudyingRepository).GetAll(student_id)
                .ToList());
        }
    }
}