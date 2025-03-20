using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DrawZone.Shapes;
using DrawZone.UI.Controls;

namespace DrawZone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum MyShapeType { stLine, stRectangle, stEllipse, stPolygon, stPolyline, stRegularPolygon }
        private bool isDrawing;
        private Point startPoint;
        private MyShape currentShape;
        private MyShapeType currentShapeType;
        private ToggleButton activeButton;
        private Brush currentStroke;
        private Brush currentFill;
        private double CurrentStrokeThickness { get { return double.Parse(TextBoxStrokeThickness.Text); } }

        public MainWindow()
        {
            InitializeComponent();

            foreach (var (name, color) in UI.Colors.colors)
            {
                MyComboBoxItem.AddColorItem(ComboBoxStroke, name, color);
                MyComboBoxItem.AddColorItem(ComboBoxFill, name, color);
            }

            Assembly assembly = Assembly.GetExecutingAssembly();

            var classNames = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == "DrawZone.Shapes")
            .Select(t => t.Name)
            .ToList();

            foreach (var name in classNames)
            {
                ShapeWrapPanel.Children.Add(new Controls.MyShapeButton(name, name, PaintZone_MyShapeButtonClick).Make());
            }

        }

        private void ComboBoxStroke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxStroke.SelectedItem is ComboBoxItem selectedItem)
                currentStroke = (Brush)selectedItem.Tag;
        }

        private void ComboBoxFill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFill.SelectedItem is ComboBoxItem selectedItem)
                currentFill = (Brush)selectedItem.Tag;
        }

        private void PaintZone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (activeButton == null || currentFill == null || currentStroke == null) return;
            if (currentShape is MyPolyShape && ((MyPolyShape)currentShape).IsPolyMode)
            {
                ((MyPolyShape)currentShape).IsPolyMode = false;
            }
            else
            {
                isDrawing = true;
                startPoint = e.GetPosition(PaintZone);
                currentShape = ShapeFactory(currentShapeType);
                PaintZone.Children.Add(currentShape.GetShape());

            }
        }

        private void PaintZone_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((currentShape is MyPolyShape) && ((MyPolyShape)currentShape).IsPolyMode)
            {
                Point currentPoint = e.GetPosition(PaintZone);
                currentShape.Draw(currentPoint);
            }
        }

        private void PaintZone_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point currentPoint = e.GetPosition(PaintZone);
                currentShape.Draw(currentPoint);
            }
        }

        private void PaintZone_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (currentShape is MyPolyShape && isDrawing)
                ((MyPolyShape)currentShape).IsPolyMode = true;
            isDrawing = false;
        }

        private void PaintZone_MyShapeButtonClick(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.IsChecked = false;
            }
            activeButton = (ToggleButton)sender;
            switch (activeButton.Name)
            {
                case "btnMyLine": currentShapeType = MyShapeType.stLine; break;
                case "btnMyRectangle": currentShapeType = MyShapeType.stRectangle; break;
                case "btnMyEllipse": currentShapeType = MyShapeType.stEllipse; break;
                case "btnMyPolyline": currentShapeType = MyShapeType.stPolyline; break;
                case "btnMyPolygon": currentShapeType = MyShapeType.stPolygon; break;
                case "btnMyRegularPolygon": currentShapeType = MyShapeType.stRegularPolygon; break;

            }
        }

        private MyShape ShapeFactory(MyShapeType shapeType)
        {
            switch (shapeType)
            {
                case MyShapeType.stLine: return new MyLine(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                case MyShapeType.stRectangle: return new MyRectangle(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                case MyShapeType.stEllipse: return new MyEllipse(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                case MyShapeType.stPolyline: return new MyPolyline(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                case MyShapeType.stPolygon: return new MyPolygon(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                case MyShapeType.stRegularPolygon: return new MyRegularPolygon(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                default: return new MyLine(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
            }

        }
    }
}
