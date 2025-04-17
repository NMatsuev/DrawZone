using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawZone.Utils;

namespace DrawZone.UI.Controls
{
    public class CustomComboBoxItem : ComboBoxItem
    {
        public Brush Color { get; set; } 

        public CustomComboBoxItem(string name, Brush color)
        {
            Color = color; 
            Content = new StackPanel
            {
                Orientation = CustomCmbBoxItemDefaultValues.ORIENTATION,
                Children =
            {
                new Rectangle
                {
                    Width = CustomCmbBoxItemDefaultValues.RECTANGLE_WIDTH,
                    Height = CustomCmbBoxItemDefaultValues.RECTANGLE_HEIGHT,
                    Fill = color,
                    Margin = CustomCmbBoxItemDefaultValues.MARGIN
                },
                new TextBlock { Text = name }
            }
            };
        }
    }
}
