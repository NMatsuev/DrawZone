using System.Windows;
using System.Windows.Media;

namespace DrawZone.Utils
{
    public static class CustomShapeButtonDefaultValues
    {
        public const double ICON_SIZE = 24.0;
        public const string ICON_PATH = "UI/Icons/";
        public const string ICON_EXTENSION = ".png";
        public const string BUTTON_NAME_PREFIX = "btn";
        public const int BUTTON_MIN_WIDTH = 25;
        public const int BUTTON_MIN_HEIGHT = 25;
        public static readonly Brush BUTTON_BACKGROUND = Brushes.White;
        public static readonly Thickness BUTTON_MARGIN = new(4);
    }
}
