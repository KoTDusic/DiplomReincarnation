using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace ElectronDecanat.Models
{
    [Table(Name = "Accounts")]
    public class User
    {
        [Column(Name = "Id"), PrimaryKey, Identity]
        public int? Id { get; set; }

        [Column(Name = "Username"), NotNull]
        [Display(Name = "Имя пользователя"), Required(ErrorMessage = "Не заполнено имя пользователя")]
        public string Username { get; set; }

        [Display(Name = "Пароль"), Required(ErrorMessage = "Не заполнен пароль")]

        [Column(Name = "Password"), NotNull]
        public string Password { get; set; }
    }
}