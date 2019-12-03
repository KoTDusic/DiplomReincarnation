using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models
{
    [Table(Name = "Teacher")]
    public class Teacher : BaseIdModel
    {
        public const string UndefinedRole = "Undefined";
        public const string TeacherRole = "Teacher";
        public const string DecanatRole = "Decanat";
        public const string AdminRole = "Admin";

        public static SelectListItem[] Roles { get; } =
        {
            new SelectListItem("Не задано", UndefinedRole),
            new SelectListItem("Преподаватель", TeacherRole),
            new SelectListItem("Декан", DecanatRole),
            new SelectListItem("Админ", AdminRole)
        };
        
        [Column(Name = "TeacherName"), NotNull]
        [Display(Name = "Имя пользователя"), Required(ErrorMessage = "Не заполнено имя пользователя")]
        public string TeacherName { get; set; }
        
        [Column(Name = "Username"), NotNull]
        [Display(Name = "Логин"), Required(ErrorMessage = "Не заполнен логин")]
        public string Username { get; set; }

        [Display(Name = "Пароль"), Required(ErrorMessage = "Не заполнен пароль")]

        [Column(Name = "Password"), NotNull]
        public string Password { get; set; }

        [Display(Name = "Роль"), Column(Name = "Role"), NotNull]
        public string Role { get; set; }
    }
}