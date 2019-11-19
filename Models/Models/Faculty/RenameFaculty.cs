using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RenameFaculty : Faculty
    {
        [Display(Name = "Текущее название")]
        [NotColumn]
        public string OldFacultyName { get; set; }
    }
}