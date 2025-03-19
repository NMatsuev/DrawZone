using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DrawZone.Shapes;

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
        private SolidColorBrush currentBrush = Brushes.Red;
        private double currentThickness = 5;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PaintZone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentShape is MyPolyShape && ((MyPolyShape)currentShape).IsPolyMode)
            {
                ((MyPolyShape)currentShape).IsPolyMode = false;
            }
            else
            {
                isDrawing = true;
                startPoint = e.GetPosition(PaintZone);
                //currentShape = new MyLine(startPoint, currentBrush, currentThickness);
                //currentShape = new MyRectangle(startPoint, currentBrush, currentThickness);
                //currentShape = new MyEllipse(startPoint, currentBrush, currentThickness);
                //currentShape = new MyRegularPolygon(startPoint, currentBrush, currentThickness);
                //currentShape = new MyPolyline(startPoint, currentBrush, currentThickness);
                currentShape = new MyPolygon(startPoint, currentBrush, currentThickness);
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
    }
}
