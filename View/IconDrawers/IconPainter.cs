using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VremenskaPrognoza.View.IconDrawers
{
    public abstract class IconPainter
    {
        protected CanvasIconPainter canvasIconPainter;

        protected IconPainter(CanvasIconPainter canvasIconPainter)
        {
            this.canvasIconPainter = canvasIconPainter;
        }

        public abstract void Paint();
    }
}
