namespace OnlineShopWebApp.Models
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsFrame { get; set; }
        public Size() { }
        public Size(int width, int height, bool isframe)
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