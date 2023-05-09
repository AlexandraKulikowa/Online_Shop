using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class SizeViewModel
    {
        [Required(ErrorMessage = "Введите ширину товара")]
        [Range(6, 200, ErrorMessage = "Введите корректное значение")]
        public int Width { get; set; }

        [Required(ErrorMessage = "Введите высоту товара")]
        [Range(6, 200, ErrorMessage = "Введите корректное значение")]
        public int Height { get; set; }

        public bool IsFrame { get; set; }
        public SizeViewModel() { }
        public SizeViewModel(int width, int height, bool isframe)
        {
            Width = width;
            Height = height;
            IsFrame = isframe;
        }
        public override string ToString()
        {
            if (IsFrame)
                return $"{Width} * {Height}, с рамкой";

            return $"{Width} * {Height}, без рамки";
        }
    }
}