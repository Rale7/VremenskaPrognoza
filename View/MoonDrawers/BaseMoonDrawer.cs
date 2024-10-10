using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace VremenskaPrognoza.View.MoonDrawers
{
    public abstract class BaseMoonDrawer
    {

        protected Canvas myCanvas;

        protected double canvasWidth;
        protected double canvasHeight;

        protected BaseMoonDrawer(Canvas canvas)
        {
            this.myCanvas = canvas;
        }

        protected void RecalculatePoints()
        {
            canvasWidth = myCanvas.ActualWidth;
            canvasHeight = myCanvas.ActualHeight;
        }

        public abstract void DrawMoonType(double percentage);

        protected void DrawLeftHalfCircle(int tickness, Brush fill, Brush stroke, double scale = 1.0)
        {
            DrawHalfCircle(tickness, fill, stroke, scale, SweepDirection.Counterclockwise);
        }
        protected void DrawRightHalfCircle(int tickness, Brush fill, Brush stroke, double scale = 1.0)
        {
            DrawHalfCircle(tickness, fill, stroke, scale, SweepDirection.Clockwise);
        }

        private void DrawHalfCircle(int tickness, Brush fill, Brush stroke, double scale, SweepDirection direction)
        {            

            Point startPoint = new Point(canvasWidth / 2, canvasHeight / 4);
            Point endPoint = new Point(canvasWidth / 2, 3 * canvasHeight / 4);

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = startPoint,
            };

            ArcSegment arcSegment = new ArcSegment
            {
                Point = endPoint,
                SweepDirection = direction,
                Size = new Size(canvasHeight * scale / 4, canvasHeight / 4)
            };


            pathFigure.Segments.Add(arcSegment);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            Path path = new Path
            {
                Stroke = stroke,
                StrokeThickness = tickness,
                Data = pathGeometry,
                Fill = fill
            };

            myCanvas.Children.Add(path);
        }

        protected void DrawLine(Brush color)
        {
            Point startPoint = new Point(canvasWidth / 2, canvasHeight / 4);
            Point endPoint = new Point(canvasWidth / 2, 3 * canvasHeight / 4);

            Line line = new Line
            {
                X1 = startPoint.X,
                X2 = endPoint.X,
                Y1 = startPoint.Y,
                Y2 = endPoint.Y,
                Stroke = color
            };

            myCanvas.Children.Add(line);
        }

        public enum MoonDrawerIndex { FIRST, SECOND, THIRD, FOURTH }
        
        public static MoonDrawerIndex PhaseIndex(double percentage, String phase)
        {
            if (percentage < 50)
            {
                if (phase == "New Moon" || phase == "Waxing Crescent" || phase == "First Quarter")
                {
                    return MoonDrawerIndex.FIRST;
                }
                else
                {
                    return MoonDrawerIndex.FOURTH;
                }
            }
            else if (percentage > 50)
            {
                if (phase == "First Quarter" || phase == "Waxing Gibbous" || phase == "Full Moon")
                {
                    return MoonDrawerIndex.SECOND;
                } else
                {
                    return MoonDrawerIndex.THIRD;
                }
            } else
            {
                if (phase == "First Quarter")
                {
                    return MoonDrawerIndex.SECOND;
                } else
                {
                    return MoonDrawerIndex.FOURTH;
                }
            }
        }

    }
}
