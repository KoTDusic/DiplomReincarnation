using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Models
{
    public class RegistrationUser : User
    {
        [Display(Name = "Повторите пароль"), Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [NotColumn]
        public string RepeatPassword { get; set; }
    }
}
