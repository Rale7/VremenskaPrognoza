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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using VremenskaPrognoza.APICalling;
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    /// <summary>
    /// Interaction logic for HumidityControl.xaml
    /// </summary>
    public partial class HumidityControl : UserControl
    {
        private double width;
        private double height;

        private double percentage = 0.5;

        private ResponseViewModel rvm;       

        public HumidityControl()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalRealtimeAction += DrawRaindrop;
        }

        private void RecalculateDimensions()
        {
            rainDrop.Children.Clear();

            try
            {
                percentage = rvm.RealtimeResponse.CurrentWeather.Humidity / 100;
            } catch (NullReferenceException)
            {
                percentage = 0;
            }

            width = rainDrop.ActualWidth;
            height = rainDrop.ActualHeight;
        }

        private void DrawRectangle(double y, double height, Brush brush)
        {
            Rectangle rect = new Rectangle()
            {
                Width = width,
                Height = height,
                Fill = brush
            };

            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, y);

            rainDrop.Children.Add(rect);
        }        

        private void DrawPercentage(double percentage)
        {
            double chordLength = 0.375 * width;                       

            double shapeHeight = (height / 2 - height / 6) +
                height * 0.41;                
                

            double height1 = (1 - percentage)  * shapeHeight;
            double height2 = percentage * shapeHeight;            
            double startPoint = height / 6;

            DrawRectangle(startPoint, height1, 
                new SolidColorBrush(Color.FromRgb(96, 106, 113)));
            DrawRectangle(startPoint + height1, height2, 
                Brushes.White);
        }

        public Polygon DrawTriangle()
        {
            Point[] points = new Point[]
            {
                new Point(width / 2, height / 6),
                new Point(0.3125 * width, 0.5 * height),
                new Point(0.6875 * width, 0.5 * height)
            };

            Polygon polygon = new Polygon()
            {
                Points = new PointCollection(points),                
            };            

            rainDrop.Children.Add(polygon);

            return polygon;
        }

        private PathGeometry DrawCircle()
        {            

            Point startPoint = new Point(0.3125 * width, height * 0.5);

            Point endPoint = new Point(0.6875 * width, height * 0.5);

            PathFigure pathFigure = new PathFigure()
            {
                StartPoint = startPoint,
            };

            ArcSegment arcSegment = new ArcSegment()
            {
                Point = endPoint,
                Size = new Size(width * 0.22, height * 0.22),
                SweepDirection = SweepDirection.Counterclockwise,
                IsLargeArc = true
            };

            pathFigure.Segments.Add(arcSegment);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            Path path = new Path()
            {
                Data = pathGeometry,
            };

            rainDrop.Children.Add(path);

            return pathGeometry;
        }

        private void DrawText(double percentage)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = $"{percentage * 100}%",
                Foreground = Brushes.Black,
                FontSize = 30
            };

            textBlock.Measure(new Size(width, height));
            Size textSize = textBlock.DesiredSize;

            Canvas.SetTop(textBlock, 0.65 * height - textSize.Height);
            Canvas.SetLeft(textBlock, width / 2 - textSize.Width / 2);

            rainDrop.Children.Add(textBlock);
        }

        public void DrawRaindrop()
        {
            RecalculateDimensions();
            DrawPercentage(percentage);            
            PathGeometry pathGeometry = DrawCircle();
            Polygon polygon = DrawTriangle();
            ClipWaterdrop(polygon, pathGeometry);
            DrawText(percentage);
        }

        private void ClipWaterdrop(Polygon polygon, PathGeometry pathGeometry)
        {
            GeometryGroup clipGroup = new GeometryGroup();            

            StreamGeometry polygonGeometry = new StreamGeometry();
            using (StreamGeometryContext ctx = polygonGeometry.Open())
            {                
                ctx.BeginFigure(polygon.Points[0], true, true);
                ctx.LineTo(polygon.Points[1], true, false);
                ctx.LineTo(polygon.Points[2], true, false);
            }
            
            clipGroup.Children.Add(polygonGeometry);            

            clipGroup.Children.Add(pathGeometry);

            rainDrop.Clip = new CombinedGeometry(
                GeometryCombineMode.Intersect,
                new RectangleGeometry(new Rect(0, 0, width, height)),
                clipGroup
            );
        } 

        private void rainDrop_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawRaindrop();
        }
    }
}
