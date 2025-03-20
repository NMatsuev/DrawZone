using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DrawZone.Controls
{
    class MyShapeButton
    {
        private const double imageWidth = 24;
        private const double imageHeight = 24;
        private const double minWidth = 25;
        private const double minHeight = 25;
        private Brush background = Brushes.White;
        private ToggleButton button;

        public MyShapeButton(string imgSource, string name, RoutedEventHandler clickHandler) 
        {
            button = new ToggleButton();
            button.MinWidth = minWidth; 
            button.MinHeight = minHeight;
            button.Name = "btn"+name;
            button.Background = background;
            button.Margin = new Thickness(4, 4, 4, 4);
            button.Click += clickHandler;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("Icons/"+imgSource+".png", UriKind.Relative));
            image.Width = imageWidth;
            image.Height = imageHeight;
            button.Content = image;
        }

        public ToggleButton Make()
        {
            return button;
        }
    }
}
