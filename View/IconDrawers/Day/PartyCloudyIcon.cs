
namespace VremenskaPrognoza.View.IconDrawers.Day 
{
    public class PartialCloudyIcon : IconPainter
    {
        private double outPart = 0.29;
        private double increment = 0.0005;

        public PartialCloudyIcon(CanvasIconPainter canvasIconPainter) : base(canvasIconPainter)
        {
        }

        public override void Paint()
        {
            canvasIconPainter.RecalculateDimensions();
            canvasIconPainter.DrawSunIcon(0.28, outPart, 0.6, 0.4);
            canvasIconPainter.DrawCloud(0.3, 0.4, 0.5);

            outPart += increment;

            if (outPart >= 0.30 || outPart <= 0.29)
            {
                increment = -increment;
            }
        }
    }
}