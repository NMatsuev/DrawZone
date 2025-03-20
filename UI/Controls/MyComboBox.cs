using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace DrawZone.UI.Controls
{
    public static class MyComboBoxItem
    {
        public static void AddColorItem(ComboBox combobox, string name, Brush color)
        {
            combobox.Items.Add(new ComboBoxItem
            {
                Content = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Fill = color,
                            Margin = new Thickness(5, 0, 5, 0)
                        },
                        new TextBlock { Text = name }
                    }
                },
                Tag = color
            });
        }
    }
}
