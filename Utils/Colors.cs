using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawZone.Utils
{
    public static class Colors
    {
        public static readonly (string Name, Brush Color)[] colors =
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
    }
}
