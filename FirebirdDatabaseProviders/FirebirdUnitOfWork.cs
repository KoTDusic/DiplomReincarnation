using Models;

namespace ElectronDecanat.Repozitory
{
    public class FirebirdUnitOfWork : IUnitOfWork
    {
        private FacultiesRepository _faculties;

        public IRepository<Faculty> Faculties => _faculties ?? (_faculties = new FacultiesRepository());
//        private static SpecialityRepozitory specialitys;
//        public static SpecialityRepozitory Specialitys => specialitys ?? (specialitys = new SpecialityRepozitory());
//        private static DisciplineRepozitory disciplines;
//        public static DisciplineRepozitory Disciplines => disciplines ?? (disciplines = new DisciplineRepozitory());
//        private static GroupRepozitory groups;
//        public static GroupRepozitory Groups => groups ?? (groups = new GroupRepozitory());
//        private static SubgroupRepozitory subgroups;
//        public static SubgroupRepozitory Subgroups => subgroups ?? (subgroups = new SubgroupRepozitory());
//        private static StudentRepozitory students;
//        public static StudentRepozitory Students => students ?? (students = new StudentRepozitory());
//        private static WorkRepository works;
//        public static WorkRepository Works => works ?? (works = new WorkRepository());
//        private static TeacherRepozitory teachers;
//        public static TeacherRepozitory Teachers => teachers ?? (teachers = new TeacherRepozitory());
//        private static LabRepozitory labs;
//        public static LabRepozitory Labs => labs ?? (labs = new LabRepozitory());
//        private static LabProgressRepozitory labProgress;
//        public static LabProgressRepozitory LabProgress => labProgress ?? (labProgress = new LabProgressRepozitory());
//        private static RolesRepozitory roles;
//        public static RolesRepozitory Roles => roles ?? (roles = new RolesRepozitory());
//        private static StudingRepizitory studing;
//        public static StudingRepizitory Studing => studing ?? (studing = new StudingRepizitory());
    }
}