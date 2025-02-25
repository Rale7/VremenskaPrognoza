using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class MistIcon : IconPainter
    {
        private double x;
        private double y;
        private double width;
        private double height;

        private double increment = 0.001;

        public MistIcon(Canvas canvas, double x, double y,
            double width, double height, bool negativeIncrement = false,
            IconPainter? next = null) : base(canvas, next)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            if (negativeIncrement) increment = -increment;
        }

        protected override void MyPaint()
        {
            DrawMist();

            x += increment;

            if (x <= 0.12 || x >= 0.18)
            {
                increment = -increment;
            }
        }

        private void DrawMist()
        {
            double mistX = x * CanvasWidth;
            double mistY = y * CanvasHeight;
            double mistWidth = width * CanvasWidth;
            double mistHeight = height * CanvasHeight;

            DrawRectangle(mistX, mistY, mistWidth, mistHeight, Brushes.White, Brushes.Transparent, 5, 5);
        }
    }
}
