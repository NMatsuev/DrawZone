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
        private ToggleButton button;

        public MyShapeButton(string imgSource, string name, RoutedEventHandler clickHandler) 
        {
            button = new ToggleButton();
            button.MinWidth = 25; 
            button.MinHeight = 25;
            button.Name = "btn"+name;
            button.Background = Brushes.White;
            button.Margin = new Thickness(4, 4, 4, 4);
            button.Click += clickHandler;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("UI/Icons/"+imgSource+".png", UriKind.Relative));
            image.Width = 24;
            image.Height = 24;
            button.Content = image;
        }

        public ToggleButton Make()
        {
            return button;
        }
    }
}
