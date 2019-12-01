using System;
using System.Linq;
using ElectronDecanat.Auth;
using ElectronDecanat.Repozitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ElectronDecanat.Controllers
{
    [Authorize(Roles = UserType.Admin)]
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
                return RedirectToAction("Specialitys", new { faculty_id=speciality.FacultyId });
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
                return RedirectToAction("Specialitys", new { faculty_id = speciality.FacultyId });
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
                return RedirectToAction("Disciplines", new { speciality_id = discipline.SpecialityId });
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
                return RedirectToAction("Disciplines", new { speciality_id = discipline.SpecialityId });
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
            ViewBag.faculty=speciality.FacultyName;
            ViewBag.speciality_name=speciality.SpecialityName;
            ViewBag.speciality_id = speciality.Id;
            return View(UnitOfWork.Groups.GetAll().Where(group => group.SpecialityId==speciality_id).ToList());
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
                return RedirectToAction("Groups", new { speciality_id = group.SpecialityId });
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
                return RedirectToAction("Groups", new { speciality_id = group.SpecialityId });
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
            ViewBag.coors=group.Coors;
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
                return RedirectToAction("Subgroups", new { group_id = subgroup.GroupId });
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
                return RedirectToAction("Subgroups", new { group_id = subgroup.GroupId });
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
                return RedirectToAction("Students", new { subgroup_id = student.SubGroupId });
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
                return RedirectToAction("Students", new { subgroup_id = student.SubGroupId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Не удалось отредактировать студента");
                return View(student);
            }
        }
        public ActionResult DeleteStudent(int id)
        {
            Student student = UnitOfWork.Students.Get(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            try
            {
                UnitOfWork.Students.Delete(student);
                return RedirectToAction("Students", new { subgroup_id = student.SubGroupId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить студента");
                return View(student);
            }
        }
        #endregion
        //#region WORKS
        //public ActionResult Works(int subgroup_id)
        //{
        //    Subgroup subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
        //    ViewBag.speciality_name = subgroup.speciality_name;
        //    ViewBag.coors = subgroup.coors;
        //    ViewBag.group_number = subgroup.group_number;
        //    ViewBag.subgroup_number = subgroup.subgroup_number;
        //    ViewBag.subgroup_id = subgroup.id;
        //    ViewBag.group_id = subgroup.group_id;
        //    return View(UnitOfWork.Works.GetAll("where \"���_���������\"=" + subgroup_id));
        //}
        //public ActionResult AddWork(int subgroup_id)
        //{
        //    Subgroup subgroup = UnitOfWork.Subgroups.Get(subgroup_id);
        //    NewWork workload=new NewWork();
        //    workload.work = new Work
        //    {
        //        faculty_name = subgroup.faculty_name,
        //        speciality_name = subgroup.speciality_name,
        //        subgroup_id=subgroup.id,
        //        group_number=subgroup.group_number,
        //        subgroup_number=subgroup.subgroup_number,
        //        coors=subgroup.coors
        //    };
        //    workload.teachers = UnitOfWork.Works.GetTeachers();
        //    workload.disciplines = UnitOfWork.Works.GetDisciplinesFromSpecialityId(subgroup.speciality_id);
        //    return View(workload);
        //}
        //[HttpPost]
        //public ActionResult AddWork(Work work)
        //{
        //    try
        //    {
        //        UnitOfWork.Works.Create(work);
        //        return RedirectToAction("Works", new { subgroup_id = work.subgroup_id });
        //    }
        //    catch(Exception e)
        //    {
        //        ModelState.AddModelError("error", ParseOracleError(e.Message));
        //        NewWork workload = new NewWork();
        //        workload.work = work;
        //        workload.teachers = UnitOfWork.Works.GetTeachers();
        //        workload.disciplines = UnitOfWork.Works.GetDisciplinesFromSpecialityId(work.speciality_id);
        //        return View(workload);
        //    }

        //}
        //public ActionResult DeleteWork(int id)
        //{
        //    Work work = UnitOfWork.Works.Get(id);
        //    return View(work);
        //}
        //[HttpPost]
        //public ActionResult DeleteWork(Work work)
        //{
        //    try
        //    {
        //        UnitOfWork.Works.Delete(work.id);
        //        return RedirectToAction("Works", new { subgroup_id = work.subgroup_id });
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("error", ParseOracleError(e.Message));
        //        return View(work);
        //    }
        //}
        //#endregion
        #region TEACHERS
        public ActionResult Teachers()
        {
            return View(UnitOfWork.Teachers.GetAll());
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
                UnitOfWork.Teachers.Update(teacher);
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
        //#region ROLES
        public ActionResult Users()
        {
            return View(UnitOfWork.Users.GetAll());
        }
        //public ActionResult Roles(string id)
        //{
        //    UserRole roles = UnitOfWork.Roles.Get(UserManager, id);
        //    return View(roles);
        //}
        //public ActionResult DeleteRole(string id,string role)
        //{
        //    UnitOfWork.Roles.Delete(UserManager, id, role);
        //    return RedirectToAction("Roles", new { id = id });
        //}
        //public ActionResult AddRole(string id)
        //{
        //    NewUserRole user = UnitOfWork.Roles.Get(UserManager, id);
        //    user.roles_list = UnitOfWork.Roles.GetAllRoles();
        //    return View(user);
        //}
        //[HttpPost]
        //public ActionResult AddRole(NewUserRole user)
        //{
        //    try
        //    {
        //        UnitOfWork.Roles.Add(UserManager, user.id,user.new_role);
        //        return RedirectToAction("Roles", new { id=user.id});
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("new_role", "������ ���������� ����");
        //        return View(user);
        //    }
        //}
        //#endregion
    }
}