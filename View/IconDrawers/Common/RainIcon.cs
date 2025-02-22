﻿using System;
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
    public class RainIcon : IconPainter
    {

        protected virtual Brush COLOR
        {
            get => new SolidColorBrush(Color.FromRgb(197, 226, 247));
        }
        private const int NUM_OF_RAINDROPS = 3;

        protected double x, y;
        protected double height, width;
        protected double rainDropHeight;
        protected double increment;

        private class RainDrop
        {
            public double X1 { get; set; }
            public double Y1 { get; set; }
        }

        private LinkedList<RainDrop> drops = new LinkedList<RainDrop>();

        public RainIcon(Canvas canvas, double x, double y,
            double width, double height, IconPainter? next = null) : base(canvas, next)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;

            rainDropHeight = height / 4;
            double rainDropPosition = width / 2;

            increment = height * 0.03;

            for (int i = 0; i < NUM_OF_RAINDROPS; i++)
            {
                drops.AddLast(new RainDrop
                {
                    X1 = x + i * rainDropPosition,
                    Y1 = y + i * rainDropHeight,
                });
            }
        }

        protected override void MyPaint()
        {
            DrawDrops();    
            
            foreach (RainDrop drop in drops)
            {
                drop.Y1 += increment;

                if (drop.Y1 >= y + height)
                {
                    drop.Y1 = y;
                }
            }
        }

        protected void DrawDrops()
        {
            GeometryGroup lines = new GeometryGroup();

            lines.Children.Add(new RectangleGeometry(new Rect(x * CanvasWidth, y * CanvasHeight, 0, 0)));
            lines.Children.Add(new RectangleGeometry(new Rect((x + width) * CanvasWidth, (y + height + rainDropHeight) * CanvasHeight, 0, 0)));
            
            foreach (var drop in drops)
            {
                lines.Children.Add(DropGeometry(drop.X1, drop.Y1));                           
            }

            Path linesPath = new Path
            {
                Stroke = COLOR,
                Fill = COLOR,
                StrokeThickness = 1,
                Data = lines
            };

            LinearGradientBrush opacityMask = new LinearGradientBrush 
            { 
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1)
            };
         
            opacityMask.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            opacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 0.8));
            opacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));

            linesPath.OpacityMask = opacityMask;

            MyCanvas.Children.Add(linesPath);       
        }

        protected virtual Geometry DropGeometry(double x, double y)
        {
            return new RectangleGeometry {
                    Rect = new Rect
                    {
                        X = x * CanvasWidth,
                        Y = y * CanvasHeight,
                        Width = 5,
                        Height = rainDropHeight * CanvasHeight,
                    },
                    RadiusX = 5,
                    RadiusY = 5,
                };
        }
    }
}
