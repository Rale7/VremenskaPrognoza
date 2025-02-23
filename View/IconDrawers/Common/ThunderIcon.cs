using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class ThunderIcon : IconPainter
    {
        protected double x, y, width, height;
        protected int cnt = 0;

        protected Brush COLOR
        {
            get => new SolidColorBrush(Color.FromRgb(255, 223, 34));
        }

        public ThunderIcon(Canvas canvas, double x, double y, double width, double height, 
            IconPainter? next = null) : base(canvas, next)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        protected override void MyPaint()
        {
            if (cnt >= 40) {
                cnt = 0;
            }

            if (cnt < 30 || (cnt / 2) % 2 == 1)
            {
                DrawThunder(); 
            }
            cnt++;
        }

        protected void DrawThunder()
        {
            Polygon square = new Polygon
            {
                Stroke = COLOR,
                Fill = COLOR,
                Points = new PointCollection
                {
                    new Point(x * CanvasWidth, (y + height * 0.6) * CanvasHeight),
                    new Point((x + width * 0.2) * CanvasWidth, y * CanvasHeight),
                    new Point((x + width * 0.4) * CanvasWidth, y * CanvasHeight),
                    new Point((x + width * 0.2) * CanvasWidth, (y + height * 0.6) * CanvasHeight)
                }
            };

            MyCanvas.Children.Add(square);

            Polygon triangle = new Polygon 
            { 
                Stroke = COLOR,
                Fill = COLOR,
                Points = new PointCollection
                {
                    new Point((x + width * 0.1) * CanvasWidth, (y + height) * CanvasHeight),
                    new Point((x + width * 0.25) * CanvasWidth, (y + height * 0.4) * CanvasHeight),
                    new Point((x + width * 0.5) * CanvasWidth, (y + height * 0.4) * CanvasHeight)
                }
            };

            MyCanvas.Children.Add(triangle);
        }
    }
}
