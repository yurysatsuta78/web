using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class EditNewsModel
    {
        public int Id { get; set; }

        [Display(Name = "Введите новый заголовок новости")]
        [Required(ErrorMessage = "заголовок не введен")]
        [StringLength(100, ErrorMessage = "Длинна не более 100 символов")]
        public string Title { get; set; }

        [Display(Name = "Введите новый текст новости")]
        [Required(ErrorMessage = "новость не должна быть пустой")]
        [StringLength(1000, ErrorMessage = "Длинна не более 1000 символов")]
        public string Description { get; set; }
    }
}
