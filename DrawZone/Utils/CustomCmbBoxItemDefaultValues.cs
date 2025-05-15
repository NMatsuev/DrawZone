using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawZone.Utils
{
    public static class CustomCmbBoxItemDefaultValues
    {
        public static readonly (string Name, Brush Color)[] COLORS =
        {
            ("Черный", Brushes.Black),
            ("Красный", Brushes.Red),
            ("Зеленый", Brushes.Green),
            ("Синий", Brushes.Blue),
            ("Желтый", Brushes.Yellow),
            ("Оранжевый", Brushes.Orange),
            ("Фиолетовый", Brushes.Purple),
            ("Серый", Brushes.Gray)
        };

        public static readonly Orientation ORIENTATION = Orientation.Horizontal;
        public const int RECTANGLE_WIDTH = 20;
        public const int RECTANGLE_HEIGHT = 20;
        public static readonly Thickness MARGIN = new(5, 0, 5, 0);
    }
}
