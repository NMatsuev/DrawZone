using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DrawZone.Shapes;

namespace DrawZone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing;
        private MyPoint startPoint;
        private MyShape currentShape;
        private SolidColorBrush currentBrush = Brushes.Red;
        private double currentThickness = 5;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PaintZone_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = new MyPoint(e.GetPosition(PaintZone));
            //currentShape = new MyLine(startPoint, currentBrush, currentThickness);
            //currentShape = new MyRectangle(startPoint, currentBrush, currentThickness);
            //currentShape = new MyEllipse(startPoint, currentBrush, currentThickness);
            currentShape = new MyRightPolygon(startPoint, currentBrush, currentThickness);
            PaintZone.Children.Add(currentShape.GetShape());
        }

        private void PaintZone_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                MyPoint currentPoint = new MyPoint(e.GetPosition(PaintZone));
                currentShape.Draw(currentPoint);
            }
        }

        private void PaintZone_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }
    }
}
