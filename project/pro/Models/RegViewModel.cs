using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class RegViewModel
    {
        #region регистрация
        [Display(Name = "Введите имя")]
        [Required(ErrorMessage = "имя не введено")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string Name { get; set; }


        [Display(Name = "Введите фамилию")]
        [Required(ErrorMessage = "фамилия не введена")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string Surname { get; set; }


        [Display(Name = "Введите логин")]
        [Required(ErrorMessage = "логин не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string UserName { get; set; }


        [Display(Name = "Введите пароль")]
        [Required(ErrorMessage = "пароль не введен")]
        [StringLength(30, ErrorMessage = "Пароль не длиннее 30 символов")]
        public string Password { get; set; }


        [Display(Name = "Введите номер телефона")]
        [Required(ErrorMessage = "номер телефона не введен")]
        [StringLength(13, ErrorMessage = "Номер не длиннее 13 цифер")]
        public string PhoneNumber { get; set; }
        #endregion

        public int Admin { get; set; }
    }
}
