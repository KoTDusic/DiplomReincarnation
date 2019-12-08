using System;
using System.Text.RegularExpressions;

namespace Models
{
    public class Study:BaseIdModel
    {
        public string StudentName { get; set; }
        public int SpecialityId { get; set; }
        public int StudentId { get; set; }
        public int SubgroupId { get; set; }
        public int Coors { get; set; }
        public int GroupNumber { get; set; }
        public int SubgroupNumber { get; set; }
        public int Completed { get; set; }
        public int NotCompleted => AllLabs - Completed;
        public int AllLabs { get; set; }
        public double Percent => Math.Round(Completed / (double) AllLabs * 100);
    }
}