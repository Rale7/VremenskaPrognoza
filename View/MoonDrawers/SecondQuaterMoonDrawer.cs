﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.MoonDrawers
{
    public class SecondQuaterMoonDrawer : BaseMoonDrawer
    {
        public SecondQuaterMoonDrawer(Canvas canvas) : base(canvas) { }

        public override void DrawMoonType(double percentage)
        {
            RecalculatePoints();

            double scale = (percentage - 50) / 50;

            DrawRightHalfCircle(1, Brushes.White, Brushes.White);
            DrawLeftHalfCircle(1, Brushes.Black, Brushes.Black);
            DrawLeftHalfCircle(1, Brushes.White, Brushes.White, scale);
            DrawLine(Brushes.White);
        }
    }
}
