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
using DrawZone.Controls;
using DrawZone.Utils;
using System.Globalization;
using DrawZone.Core;
using System.Windows.Shapes;

namespace DrawZone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing;
        private Point startPoint;
        private CustomShape currentShape;
        private string currentShapeType;
        private ToggleButton activeButton;
        private Brush currentStroke;
        private Brush currentFill;
        private Dictionary<string, ConstructorInfo> constructors;
        private HistoryStack drawingHistory;
        private double CurrentStrokeThickness { get { return GetStrokeThickness(); } 
                                                set { TextBoxStrokeThickness.Text = value.ToString(CultureInfo.InvariantCulture); } }

        public MainWindow()
        {
            InitializeComponent();

            foreach (var (name, color) in CustomCmbBoxItemDefaultValues.COLORS)
            {
                ComboBoxStroke.Items.Add(new CustomComboBoxItem (name, color));
                ComboBoxFill.Items.Add(new CustomComboBoxItem(name, color));
            }

            Assembly assembly = Assembly.GetExecutingAssembly();

            var classNames = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == MainDefaultValues.DEFAULT_SHAPES_NAMESPACE)
            .Select(t => t.Name)
            .ToList();

            foreach (var name in classNames)
            {
                var button = new CustomShapeButton(name, name, PaintZone_MyShapeButtonClick);

                if (name == MainDefaultValues.DEFAULT_SHAPE)
                {
                    button.IsChecked = true;
                    activeButton = button;
                    currentShapeType = name;
                }

                ShapeWrapPanel.Children.Add(button);
            }

            constructors = ShapeFactory.GetShapeConstructors();
            ComboBoxStroke.SelectedItem = ComboBoxStroke.Items.GetItemAt(0);
            ComboBoxFill.SelectedItem = ComboBoxFill.Items.GetItemAt(0);
            currentStroke = ((CustomComboBoxItem)ComboBoxStroke.SelectedItem).Color;
            currentFill = ((CustomComboBoxItem)ComboBoxFill.SelectedItem).Color;
            CurrentStrokeThickness = MainDefaultValues.DEFAULT_STROKE_THICKNESS;
            CustomShape? shape = ShapeFactory.CreateShapeInstance(constructors, currentShapeType, new object[] { startPoint, currentStroke, currentFill, CurrentStrokeThickness });
            currentShape = shape ?? new CustomLine(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
            drawingHistory = new HistoryStack();
            btnRedo.IsEnabled = false;
            btnUndo.IsEnabled = false;
        }

        private double GetStrokeThickness()
        {
            return (double.TryParse(TextBoxStrokeThickness.Text, CultureInfo.InvariantCulture, out var result)) ? result : 10;
        }

        private void ComboBoxStroke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxStroke.SelectedItem is CustomComboBoxItem selectedItem)
                currentStroke = selectedItem.Color;
        }

        private void ComboBoxFill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFill.SelectedItem is CustomComboBoxItem selectedItem)
                currentFill = selectedItem.Color;
        }

        private void PaintZone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentShape.SupportsPolyMode && ((CustomPolyShape)currentShape).IsPolyMode)
            {
                currentShape.ExitPolyMode();
                currentShape.IsDrawed = true;
                drawingHistory.Push(currentShape.GetShape());
                updateButtons();
                updateCanvas();
            }
            else
            {
               
                isDrawing = true;
                startPoint = e.GetPosition(PaintZone);
                CustomShape? shape = ShapeFactory.CreateShapeInstance(constructors, currentShapeType, new object[] {startPoint, currentStroke, currentFill, CurrentStrokeThickness});
                currentShape = shape ?? new CustomLine(startPoint, currentStroke, currentFill, CurrentStrokeThickness);
                currentShape.IsDrawed = false;
                PaintZone.Children.Add(currentShape.GetShape());
            }
        }

        private void PaintZone_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentShape.SupportsPolyMode && ((CustomPolyShape)currentShape).IsPolyMode)
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
                currentShape.IsDrawed = true;
            }
        }

        private void PaintZone_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (currentShape.SupportsPolyMode && isDrawing)
                currentShape.EnterPolyMode();
            else if (isDrawing) {
                if (currentShape.IsDrawed)
                {
                    drawingHistory.Push(currentShape.GetShape());
                    updateButtons();
                    updateCanvas();
                }
                else
                {
                    PaintZone.Children.RemoveAt(PaintZone.Children.Count-1);
                }
            }
            isDrawing = false;
        }

        private void PaintZone_MyShapeButtonClick(object sender, RoutedEventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.IsChecked = false;
            }
            activeButton = (ToggleButton)sender;
            currentShapeType = activeButton.Name.Substring(CustomShapeButtonDefaultValues.BUTTON_NAME_PREFIX.Length);
        }

        private void IncreaseStrokeThickness_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxStrokeThickness.Text, CultureInfo.InvariantCulture, out var value))
            {
                CurrentStrokeThickness = value + 0.5;
            }
        }

        private void DecreaseStrokeThickness_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxStrokeThickness.Text, CultureInfo.InvariantCulture, out var value) && value > 0.5)
            {
                CurrentStrokeThickness = value - 0.5;
            }
        }

        private void TextBoxStrokeThickness_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0) && e.Text != ".";
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            drawingHistory.Undo();
            updateButtons();
            updateCanvas();
            isDrawing = false;
            currentShape.ExitPolyMode();

        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            drawingHistory.Redo();
            updateButtons();
            updateCanvas();
            isDrawing = false;
            currentShape.ExitPolyMode();
        }

        private void updateCanvas()
        {
            PaintZone.Children.Clear();
            List<Shape> shapeList = drawingHistory.GetShapeList();
            shapeList.Reverse();
            foreach (var shape in shapeList)
                PaintZone.Children.Add(shape);
        }

        private void updateButtons()
        {
            btnUndo.IsEnabled = drawingHistory.CanUndo();
            btnRedo.IsEnabled = drawingHistory.CanRedo();
        }
    }
}
