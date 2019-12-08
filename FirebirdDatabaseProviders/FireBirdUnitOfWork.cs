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
        private IRepository<Work> _works;
        private IRepository<Study> _studying;
        private IRepository<DisciplineStudy> _disciplineStudying;
        
        public IRepository<Faculty> Faculties => _faculties ?? (_faculties = new FacultiesRepository());
        public IRepository<Speciality> Specialitys => _specialitys ?? (_specialitys = new SpecialityRepository());
        public IRepository<Discipline> Disciplines => _disciplines ?? (_disciplines = new DisciplineRepository());
        public IRepository<Group> Groups => _groups ?? (_groups = new GroupRepository());
        public IRepository<Subgroup> Subgroups => _subgroups ?? (_subgroups = new SubgroupRepository());
        public IRepository<Student> Students => _students ?? (_students = new StudentRepository());
        public IRepository<Teacher> Teachers => _teachers ?? (_teachers = new TeacherRepository());
        public IRepository<Lab> Labs => _labs ?? (_labs = new LabRepository());
        public IRepository<LabProgress> LabProgress => _labProgress ?? (_labProgress = new LabProgressRepository());
        public IRepository<Work> Works => _works ?? (_works = new WorkRepository());
        public IRepository<Study> Studying => _studying ?? (_studying = new StudyingRepository());
        public IRepository<DisciplineStudy> DisciplineStudying => _disciplineStudying ?? (_disciplineStudying = new DisciplineStudyingRepository());
    }
}