using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DrawZone.Shapes;
using DrawZone.UI.Controls;
using DrawZone.Factories;
using System;

namespace DrawZone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing;
        private Point startPoint;
        private MyShape currentShape;
        private string currentShapeType;
        private ToggleButton activeButton;
        private Brush currentStroke;
        private Brush currentFill;
        private Dictionary<string, ConstructorInfo> constructors;
        private double CurrentStrokeThickness { get { return double.Parse(TextBoxStrokeThickness.Text); } set{ TextBoxStrokeThickness.Text = value.ToString(); } }

        public MainWindow()
        {
            InitializeComponent();

            foreach (var (name, color) in Utils.Colors.colors)
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

            constructors = ShapeFactory.GetShapeConstructors();
            currentShapeType = "MyLine";
            ComboBoxStroke.SelectedItem = ComboBoxStroke.Items.GetItemAt(0);
            ComboBoxFill.SelectedItem = ComboBoxFill.Items.GetItemAt(0);
            currentStroke = (Brush)((ComboBoxItem)ComboBoxStroke.SelectedItem).Tag;
            currentFill = (Brush)((ComboBoxItem)ComboBoxFill.SelectedItem).Tag;
            CurrentStrokeThickness = 12;
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
            if (isPolyMode(currentShape))
            {
                ((MyPolyShape)currentShape).IsPolyMode = false;
            }
            else
            {
                isDrawing = true;
                startPoint = e.GetPosition(PaintZone);
                MyShape? shape = ShapeFactory.CreateShapeInstance(constructors, currentShapeType, new object[] {startPoint, currentStroke, currentFill, CurrentStrokeThickness});
                currentShape = (shape == null) ? new MyLine(startPoint, currentStroke, currentFill, CurrentStrokeThickness) : shape;
                PaintZone.Children.Add(currentShape.GetShape());

            }
        }

        private void PaintZone_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isPolyMode(currentShape))
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
            if (isPolyDrawing(currentShape))
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
            currentShapeType = activeButton.Name.Substring(3);

        }

        private bool isPolyMode(MyShape shape)
        {
            return (shape is MyPolyShape) && ((MyPolyShape)shape).IsPolyMode;
        }

        private bool isPolyDrawing(MyShape shape)
        {
            return (shape is MyPolyShape) && isDrawing;
        }
    }
}
