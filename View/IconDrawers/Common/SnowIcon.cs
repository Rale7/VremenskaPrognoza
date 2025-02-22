using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class SnowIcon : DropIcon
    {
        private int NUM_OF_LINES = 3;

        private double SNOWFLAKE_SECONDARY_START = 0.6;
        private double SNOWFLAKE_SECONDARY_SIZE = 0.4;
        private double SNOWFLAKE_SECONDARY_ANGLE = Math.PI / 4;

        protected override Brush COLOR => Brushes.White;

        public SnowIcon(Canvas canvas, double x, double y, double width, double height, IconPainter? next = null) : base(canvas, x, y, width, height, next)
        {
        }

        protected override Geometry DropGeometry(double x, double y)
        {
            double snowflakeRadius = Math.Min(height, width) * 0.1 * Math.Max(CanvasHeight, CanvasWidth);

            GeometryGroup snowflake = new GeometryGroup();
            
            double angle = ((double)Math.PI * 2) / NUM_OF_LINES;
            for (int i = 0; i < NUM_OF_LINES; i++)
            {          
                // main snowflake line
                double x1 = x * CanvasWidth + snowflakeRadius * Math.Cos(angle * i);
                double y1 = y * CanvasHeight + snowflakeRadius * Math.Sin(angle * i);
                double x2 = x * CanvasWidth - snowflakeRadius * Math.Cos(angle * i);
                double y2 = y * CanvasHeight - snowflakeRadius * Math.Sin(angle * i);
                
                snowflake.Children.Add(new LineGeometry {
                    StartPoint = new Point(x1, y1),
                    EndPoint = new Point(x2, y2),  
                });
                
                // secondary snowflake lines on right side
                x1 = x * CanvasWidth + snowflakeRadius * Math.Cos(angle * i) * SNOWFLAKE_SECONDARY_START;
                y1 = y * CanvasHeight + snowflakeRadius * Math.Sin(angle * i) * SNOWFLAKE_SECONDARY_START;
                x2 = x1 + snowflakeRadius * Math.Cos(angle * i + SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;
                y2 = y1 + snowflakeRadius * Math.Sin(angle * i + SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;

                snowflake.Children.Add(new LineGeometry {
                    StartPoint = new Point(x1, y1),
                    EndPoint = new Point(x2, y2),  
                });

                x2 = x1 + snowflakeRadius * Math.Cos(angle * i - SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;
                y2 = y1 + snowflakeRadius * Math.Sin(angle * i - SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;

                snowflake.Children.Add(new LineGeometry {
                    StartPoint = new Point(x1, y1),
                    EndPoint = new Point(x2, y2),  
                });
                
                // secondary snowflake lines on left side
                x1 = x * CanvasWidth - snowflakeRadius * Math.Cos(angle * i) * SNOWFLAKE_SECONDARY_START;
                y1 = y * CanvasHeight - snowflakeRadius * Math.Sin(angle * i) * SNOWFLAKE_SECONDARY_START;
                x2 = x1 - snowflakeRadius * Math.Cos(angle * i + SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;
                y2 = y1 - snowflakeRadius * Math.Sin(angle * i + SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;

                snowflake.Children.Add(new LineGeometry {
                    StartPoint = new Point(x1, y1),
                    EndPoint = new Point(x2, y2),  
                });

                x2 = x1 - snowflakeRadius * Math.Cos(angle * i - SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;
                y2 = y1 - snowflakeRadius * Math.Sin(angle * i - SNOWFLAKE_SECONDARY_ANGLE) * SNOWFLAKE_SECONDARY_SIZE;

                snowflake.Children.Add(new LineGeometry {
                    StartPoint = new Point(x1, y1),
                    EndPoint = new Point(x2, y2),  
                });


            }

            return snowflake;
        }

    }
}
