using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using DrawZone.Utils;

namespace DrawZone.Controls
{
    public class CustomShapeButton : ToggleButton
    {

        public CustomShapeButton(string iconSource, string name, RoutedEventHandler clickHandler)
        {
            Name = CustomShapeButtonDefaultValues.BUTTON_NAME_PREFIX + name;
            Click += clickHandler;
            MinWidth = CustomShapeButtonDefaultValues.BUTTON_MIN_WIDTH;
            MinHeight = CustomShapeButtonDefaultValues.BUTTON_MIN_HEIGHT;
            Background = CustomShapeButtonDefaultValues.BUTTON_BACKGROUND;
            Margin = CustomShapeButtonDefaultValues.BUTTON_MARGIN;
            Content = CreateIcon(iconSource);
        }

        private Image CreateIcon(string iconSource)
        {
            return new Image
            {
                Width = CustomShapeButtonDefaultValues.ICON_SIZE,
                Height = CustomShapeButtonDefaultValues.ICON_SIZE,
                Source = !string.IsNullOrEmpty(iconSource)
                    ? new BitmapImage(new Uri($"{CustomShapeButtonDefaultValues.ICON_PATH}{iconSource}{CustomShapeButtonDefaultValues.ICON_EXTENSION}", UriKind.Relative))
                    : null
            };
        }
    }
}
