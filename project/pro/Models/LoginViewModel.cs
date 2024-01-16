using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Введите логин")]
        [Required(ErrorMessage = "логин не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string AuthUserName { get; set; }


        [Display(Name = "Введите пароль")]
        [Required(ErrorMessage = "пароль не введен")]
        [StringLength(30, ErrorMessage = "Пароль не длиннее 30 символов")]
        public string AuthPassword { get; set; }
    }
}
