
namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class CloudyIcon : IconPainter
    {
        public CloudyIcon(CanvasIconPainter canvasIconPainter) : base(canvasIconPainter)
        {
        }

        public override void Paint()
        {
            canvasIconPainter.RecalculateDimensions();
            canvasIconPainter.DrawCloud(0.3, 0.5, 0.5); 
        }
    }
}