using System;
using System.Linq;
using ElectronDecanat.Repozitory;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace ElectronDecanat.Controllers
{
    [Authorize(Roles = Teacher.AdminRole)]
    public class AdminController : BaseController
    {
        public AdminController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region FACULTY

        public ActionResult Faculties()
        {
            return View(UnitOfWork.Faculties.GetAll().ToList());
        }

        public ActionResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFaculty(Faculty faculty)
        {
            try
            {
                UnitOfWork.Faculties.Create(faculty);
                return RedirectToAction("Faculties");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось добавить факультет");
                return View(faculty);
            }
        }

        public ActionResult EditFaculty(int id)
        {
            var oldFaculty = UnitOfWork.Faculties.Get(id);
            var faculty = new RenameFaculty(oldFaculty);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult EditFaculty(RenameFaculty faculty)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                UnitOfWork.Faculties.Update(faculty);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось обновить факультет");
                return View(faculty);
            }

            return RedirectToAction("Faculties");
        }

        public ActionResult DeleteFaculty(int id)
        {
            var oldFaculty = UnitOfWork.Faculties.Get(id);
            return View(oldFaculty);
        }

        [HttpPost]
        public ActionResult DeleteFaculty(Faculty faculty)
        {
            try
            {
                UnitOfWork.Faculties.Delete(faculty);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить факультет");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Faculties");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region SPECIALITIS

        public ActionResult Specialitys(int faculty_id)
        {
            ViewBag.faculty_id = faculty_id;
            ViewBag.faculty = UnitOfWork.Faculties.Get(faculty_id).FacultyName;
            var specialitys = UnitOfWork.Specialitys.GetAll(
                table => table.Where(speciality => speciality.FacultyId == faculty_id));
            return View(specialitys);
        }

        public ActionResult AddSpeciality(int faculty_id)
        {
            var faculty = UnitOfWork.Faculties.Get(faculty_id);
            var speciality = new Speciality(faculty);
            return View(speciality);
        }

        [HttpPost]
        public ActionResult AddSpeciality(Speciality speciality)
        {
            try
            {
                UnitOfWork.Specialitys.Create(speciality);
                return RedirectToAction("Specialitys", new {faculty_id = speciality.FacultyId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "не удалось добавить специальность");
                return View(speciality);
            }
        }

        public ActionResult EditSpeciality(int id)
        {
            var speciality = UnitOfWork.Specialitys.Get(id);
            var model = new RenamedSpeciality(speciality);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSpeciality(RenamedSpeciality speciality)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                UnitOfWork.Specialitys.Update(speciality);
                return RedirectToAction("Specialitys", new {faculty_id = speciality.FacultyId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "не удалось обновить специальность");
                return View(speciality);
            }
        }

        public ActionResult DeleteSpeciality(int id)
        {
            var speciality = UnitOfWork.Specialitys.Get(id);
            return View(speciality);
        }

        [HttpPost]
        public ActionResult DeleteSpeciality(Speciality speciality)
        {
            try
            {
                UnitOfWork.Specialitys.Delete(speciality);
                return RedirectToAction("Specialitys", new {faculty_id = speciality.FacultyId});
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "не удалось удалить специальность");
                return View(speciality);
            }
        }

        #endregion

        #region DISCIPLINES

        public ActionResult Disciplines(int speciality_id)
        {
            var speciality = UnitOfWork.Specialitys.Get(speciality_id);
            ViewBag.faculty_id = speciality.FacultyId;
            ViewBag.speciality_id = speciality_id;
            ViewBag.faculty = speciality.FacultyName;
            ViewBag.speciality_name = speciality.SpecialityName;
            return View(UnitOfWork.Disciplines.GetAll(items => items
                .Where(discipline => discipline.SpecialityId == speciality_id)));
        }

        public ActionResult AddDiscipline(int speciality_id)
        {
            var speciality = UnitOfWork.Specialitys.Get(speciality_id);
            var discipline = new Discipline(speciality);
            return View(discipline);
        }

        [HttpPost]
        public ActionResult AddDiscipline(Discipline discipline)
        {
            try
            {
                UnitOfWork.Disciplines.Create(discipline);
                return RedirectToAction("Disciplines", new {speciality_id = discipline.SpecialityId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось создать дисциплину");
                return View(discipline);
            }
        }

        public ActionResult EditDiscipline(int id)
        {
            var discipline = new RenamedDiscipline(UnitOfWork.Disciplines.Get(id));
            return View(discipline);
        }

        [HttpPost]
        public ActionResult EditDiscipline(RenamedDiscipline discipline)
        {
            try
            {
                UnitOfWork.Disciplines.Update(discipline);
                return RedirectToAction("Disciplines", new {speciality_id = discipline.SpecialityId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось обновить дисциплину");
                return View(discipline);
            }
        }

        public ActionResult DeleteDiscipline(int id)
        {
            var discipline = UnitOfWork.Disciplines.Get(id);
            return View(discipline);
        }

        [HttpPost]
        public ActionResult DeleteDiscipline(Discipline discipline)
        {
            try
            {
                UnitOfWork.Disciplines.Delete(discipline);
                return RedirectToAction("Disciplines", new {speciality_id = discipline.SpecialityId});
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить дисциплину");
                return View(discipline);
            }
        }

        #endregion

        #region GROUPS

        public ActionResult Groups(int speciality_id)
        {
            var speciality = UnitOfWork.Specialitys.Get(speciality_id);
            ViewBag.faculty_id = speciality.FacultyId;
            ViewBag.faculty = speciality.FacultyName;
            ViewBag.speciality_name = speciality.SpecialityName;
            ViewBag.speciality_id = speciality.Id;
            return View(UnitOfWork.Groups.GetAll().Where(group => group.SpecialityId == speciality_id).ToList());
        }

        public ActionResult AddGroup(int speciality_id)
        {
            var speciality = UnitOfWork.Specialitys.Get(speciality_id);
            var group = new Group(speciality);
            return View(group);
        }

        [HttpPost]
        public ActionResult AddGroup(Group group)
        {
            try
            {
                UnitOfWork.Groups.Create(group);
                return RedirectToAction("Groups", new {speciality_id = group.SpecialityId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось добавить группу");
                return View(group);
            }
        }

        public ActionResult DeleteGroup(int id)
        {
            return View(UnitOfWork.Groups.Get(id));
        }

        [HttpPost]
        public ActionResult DeleteGroup(Group group)
        {
            try
            {
                UnitOfWork.Groups.Delete(group);
                return RedirectToAction("Groups", new {speciality_id = group.SpecialityId});
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить группу");
                return View(group);
            }
        }

        #endregion

        #region SUBGROUPS

        public ActionResult Subgroups(int group_id)
        {
            var group = UnitOfWork.Groups.Get(group_id);
            ViewBag.speciality_id = group.SpecialityId;
            ViewBag.group_id = group_id;
            ViewBag.faculty = group.FacultyName;
            ViewBag.speciality_name = group.SpecialityName;
            ViewBag.coors = group.Coors;
            ViewBag.group_number = group.GroupNumber;
            var subgroups = UnitOfWork.Subgroups.GetAll().Where(subgroup => subgroup.GroupId == group_id).ToList();
            return View(subgroups);
        }

        public ActionResult AddSubgroup(int group_id)
        {
            var group = UnitOfWork.Groups.Get(group_id);
            var subgroup = new Subgroup(group);
            return View(subgroup);
        }

        [HttpPost]
        public ActionResult AddSubgroup(Subgroup subgroup)
        {
            try
            {
                UnitOfWork.Subgroups.Create(subgroup);
                return RedirectToAction("Subgroups", new {group_id = subgroup.GroupId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось создать подгруппу");
                return View(subgroup);
            }
        }

        public ActionResult DeleteSubgroup(int id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(id);
            return View(subgroup);
        }

        [HttpPost]
        public ActionResult DeleteSubgroup(Subgroup subgroup)
        {
            try
            {
                UnitOfWork.Subgroups.Delete(subgroup);
                return RedirectToAction("Subgroups", new {group_id = subgroup.GroupId});
            }
            catch (Exception)
            {
                ModelState.AddModelError("subgroup_number", "Не удалось удалить подгруппу");
                return View(subgroup);
            }
        }

        #endregion

        #region STUDENTS

        public ActionResult Students(int subgroup_id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            ViewBag.group_id = subgroup.GroupId;
            ViewBag.subgroup_id = subgroup_id;
            ViewBag.faculty = subgroup.FacultyName;
            ViewBag.speciality_name = subgroup.SpecialityName;
            ViewBag.coors = subgroup.Course;
            ViewBag.group_number = subgroup.GroupNumber;
            ViewBag.subgroup_number = subgroup.SubGroupNumber;
            var students = UnitOfWork.Students.GetAll().Where(student => student.SubGroupId == subgroup_id).ToList();
            return View(students);
        }

        public ActionResult AddStudent(int subgroup_id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            var student = new Student(subgroup);
            return View(student);
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            try
            {
                UnitOfWork.Students.Create(student);
                return RedirectToAction("Students", new {subgroup_id = student.SubGroupId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось добавить студента");
                return View(student);
            }
        }

        public ActionResult EditStudent(int id)
        {
            var student = new RenameStudent(UnitOfWork.Students.Get(id));
            return View(student);
        }

        [HttpPost]
        public ActionResult EditStudent(RenameStudent student)
        {
            try
            {
                UnitOfWork.Students.Update(student);
                return RedirectToAction("Students", new {subgroup_id = student.SubGroupId});
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось отредактировать студента");
                return View(student);
            }
        }

        public ActionResult DeleteStudent(int id)
        {
            var student = UnitOfWork.Students.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            try
            {
                UnitOfWork.Students.Delete(student);
                return RedirectToAction("Students", new {subgroup_id = student.SubGroupId});
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить студента");
                return View(student);
            }
        }

        #endregion

        #region WORKS

        public ActionResult Works(int subgroup_id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            ViewBag.faculty = subgroup.FacultyName;
            ViewBag.speciality_name = subgroup.SpecialityName;
            ViewBag.coors = subgroup.Course;
            ViewBag.group_number = subgroup.GroupNumber;
            ViewBag.subgroup_number = subgroup.SubGroupNumber;
            ViewBag.subgroup_id = subgroup.Id;
            ViewBag.group_id = subgroup.GroupId;
            var items = UnitOfWork.Works.GetAll(works => works
                .LoadWith(t => t.Teacher)
                .LoadWith(t2 => t2.Discipline)
                .Where(work => work.SubGroupId == subgroup_id));
            return View(items);
        }

        public ActionResult AddWork(int subgroup_id)
        {
            var subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
            var workload = new Work();
            workload.SubGroupId = subgroup.Id;
            workload.Subgroup = subgroup;
            workload.Teachers = UnitOfWork.Teachers.GetAll(table =>
                    table.Where(teacher => teacher.Role == Teacher.TeacherRole))
                .Select(teacher =>
                    new SelectListItem(teacher.TeacherName,
                        teacher.Id.ToString()));

            workload.Disciplines = UnitOfWork.Disciplines.GetAll(table =>
                    table.Where(discipline => discipline.SpecialityId == subgroup.Group.SpecialityId))
                .Select(discipline => new SelectListItem(discipline.DisciplineName, discipline.Id.ToString()));
            return View(workload);
        }

        [HttpPost]
        public ActionResult AddWork(Work work)
        {
            try
            {
                if (UnitOfWork.Works.GetAll(works => works.Where(w =>
                        w.TeacherId == work.TeacherId && w.DisciplineId == work.DisciplineId &&
                        w.SubGroupId == work.SubGroupId)).FirstOrDefault() != null)
                {
                    ModelState.AddModelError(string.Empty, "такая работа уже добавлена");
                    return AddWork(work.SubGroupId);
                }

                UnitOfWork.Works.Create(work);
                return RedirectToAction("Works", new {subgroup_id = work.SubGroupId});
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "не удалось добавить работу");
                return AddWork(work.SubGroupId);
            }
        }

        public ActionResult DeleteWork(int id)
        {
            var work = UnitOfWork.Works.Get(id);
            return View(work);
        }

        [HttpPost]
        public ActionResult DeleteWork(Work work)
        {
            try
            {
                UnitOfWork.Works.Delete(work);
                return RedirectToAction("Works", new {subgroup_id = work.SubGroupId});
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "не удалось удалить работу");
                return View(work);
            }
        }

        #endregion

        #region TEACHERS

        public ActionResult Teachers()
        {
            var teachers = UnitOfWork.Teachers.GetAll(
                table => table.Where(teacher => teacher.Role == Teacher.TeacherRole));
            return View(teachers);
        }

        public ActionResult EditTeacher(int id)
        {
            var teacher = new RenameTeacher(UnitOfWork.Teachers.Get(id));
            return View(teacher);
        }

        [HttpPost]
        public ActionResult EditTeacher(RenameTeacher teacher)
        {
            try
            {
                var editedTeacher = UnitOfWork.Teachers.Get(teacher.Id);
                editedTeacher.TeacherName = teacher.TeacherName;
                UnitOfWork.Teachers.Update(editedTeacher);
                return RedirectToAction("Teachers");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "не удалось обновить преподавателя");
                return View(teacher);
            }
        }

        public ActionResult DeleteTeacher(int id)
        {
            var teacher = UnitOfWork.Teachers.Get(id);
            return View(teacher);
        }

        [HttpPost]
        public ActionResult DeleteTeacher(Teacher teacher)
        {
            try
            {
                UnitOfWork.Teachers.Delete(teacher);
                return RedirectToAction("Teachers");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "не удалось удалить преподавателя");
                return View(teacher);
            }
        }

        #endregion

        #region ROLES

        public ActionResult Users()
        {
            return View(UnitOfWork.Teachers.GetAll(table =>
                table.Where(teacher => teacher.Role != Teacher.AdminRole)));
        }

        public ActionResult SetRole(int id)
        {
            var user = UnitOfWork.Teachers.Get(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult SetRole(Teacher user)
        {
            try
            {
                var current = UnitOfWork.Teachers.Get(user.Id);
                current.Role = user.Role;
                //обновляем только роль, что бы не затереть остальные поля у пользователя
                UnitOfWork.Teachers.Update(current);
                return RedirectToAction("Users");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось поменять роль");
                return View();
            }
        }

        #endregion
    }
}