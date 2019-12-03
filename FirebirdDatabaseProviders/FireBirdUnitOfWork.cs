using Models;

namespace ElectronDecanat.Repozitory
{
    public class FireBirdUnitOfWork : IUnitOfWork
    {
        private IRepository<Faculty> _faculties;
        private IRepository<Speciality> _specialitys;
        private IRepository<Discipline> _disciplines;
        private IRepository<Teacher> _teachers;
        private IRepository<Group> _groups;
        private IRepository<Student> _students;
        private IRepository<Subgroup> _subgroups;
        private IRepository<Lab> _labs;
        private IRepository<LabProgress> _labProgress;
        
        public IRepository<Faculty> Faculties => _faculties ?? (_faculties = new FacultiesRepository());
        public IRepository<Speciality> Specialitys => _specialitys ?? (_specialitys = new SpecialityRepozitory());
        public IRepository<Discipline> Disciplines => _disciplines ?? (_disciplines = new DisciplineRepozitory());
        public IRepository<Group> Groups => _groups ?? (_groups = new GroupRepository());
        public IRepository<Subgroup> Subgroups => _subgroups ?? (_subgroups = new SubgroupRepository());
        public IRepository<Student> Students => _students ?? (_students = new StudentRepository());
        public IRepository<Teacher> Teachers => _teachers ?? (_teachers = new TeacherRepository());
        public IRepository<Lab> Labs => _labs ?? (_labs = new LabRepository());
        public IRepository<LabProgress> LabProgress => _labProgress ?? (_labProgress = new LabProgressRepository());
//        private static WorkRepository works;
//        public static WorkRepository Works => works ?? (works = new WorkRepository());


//        private static RolesRepozitory roles;
//        public static RolesRepozitory Roles => roles ?? (roles = new RolesRepozitory());
//        private static StudingRepizitory studing;
//        public static StudingRepizitory Studing => studing ?? (studing = new StudingRepizitory());
    }
}