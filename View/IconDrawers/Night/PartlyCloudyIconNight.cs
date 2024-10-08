
namespace VremenskaPrognoza.View.IconDrawers.Day 
{
    public class PartialCloudyIconNight : IconPainter
    {
        public PartialCloudyIconNight(CanvasIconPainter canvasIconPainter) : base(canvasIconPainter)
        {
        }

        public override void Paint()
        {
            canvasIconPainter.RecalculateDimensions();
            canvasIconPainter.DrawMoonIcon(0.28, 0.85, 0.15, 45);
            canvasIconPainter.DrawCloud(0.3, 0.4, 0.5);
        }
    }
}