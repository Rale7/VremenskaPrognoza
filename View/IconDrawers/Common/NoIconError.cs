using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    class NoIconError : IconPainter
    {

        public NoIconError(Canvas canvas, IconPainter? next = null) : base(canvas, next)
        {
        }

        protected override void MyPaint()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "ICON NOT AVAILABLE",
                Foreground = Brushes.White,
                FontSize = 20,
            };
            
            textBlock.Measure(new Size(CanvasWidth, CanvasHeight));
            Size textSize = textBlock.DesiredSize;

            Canvas.SetLeft(textBlock, 0.5 * CanvasWidth - textSize.Width * 0.55);
            Canvas.SetTop(textBlock, 0.5 * CanvasHeight);

            MyCanvas.Children.Add(textBlock); 

        }
    }
}
