using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Display(Name = "Введите марку")]
        [Required(ErrorMessage = "марка не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string Brand { get; set; }

        [Display(Name = "Введите модель")]
        [Required(ErrorMessage = "модель не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string Model { get; set; }

        [Display(Name = "Введите год выпуска")]
        [Required(ErrorMessage = "год выпуска не введен")]
        [StringLength(5, ErrorMessage = "Длинна не более 5 символов")]
        public string Year { get; set; }

        [Display(Name = "Введите цену")]
        [Required(ErrorMessage = "цена не введена")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string Price { get; set; }

        [Display(Name = "Введите объём двигателя")]
        [Required(ErrorMessage = "объём двигателя не введен")]
        [StringLength(10, ErrorMessage = "Длинна не более 10 символов")]
        public string EngineVolume { get; set; }

        [Display(Name = "Введите тип двигателя")]
        [Required(ErrorMessage = "тип двигателя не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string EngineType { get; set; }

        [Display(Name = "Введите тип привода")]
        [Required(ErrorMessage = "тип привода не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string DriveUnit { get; set; }

        [Display(Name = "Введите тип КПП")]
        [Required(ErrorMessage = "тип КПП не введен")]
        [StringLength(20, ErrorMessage = "Длинна не более 20 символов")]
        public string Transmission { get; set; }

        [Display(Name = "Загрузите изображение")]
        public IFormFile Image { get; set; }
    }
}
