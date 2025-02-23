
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using VremenskaPrognoza.View.IconDrawers.Common;
using VremenskaPrognoza.View.IconDrawers.Day;
using VremenskaPrognoza.View.IconDrawers.Night;

namespace VremenskaPrognoza.View.IconDrawers 
{
    class IconDrawerFactory {
        private Canvas canvas;

        public const int CLEAR_SKY = 113;
        public const int PARTY_CLOUDY = 116;
        public const int CLOUDY = 119;
        public const int OVERCAST = 122;
        public const int MIST = 143;
        public const int PATCHY_RAIN_POSSIBLE = 176;
        public const int PATCHY_SNOW_POSSIBLE = 179;
        public const int PATCHY_SLEET_POSSIBLE = 182;
        public const int PATCHY_FREEZING_DRIZZLE_POSSIBLE = 185;
        public const int THUNDERY_OUTBREAKS_POSSIBLE = 200;
        public const int BLOWING_SNOW = 227;
        public const int BLIZZARD = 230;
        public const int FOG = 248;
        public const int FREEZING_FOG = 260;
        public const int PATCHY_LIGHT_DRIZZLE = 263;
        public const int LIGHT_DRIZZLE = 266;

        private Dictionary<(int, bool), IconPainter> allIcons =
            new Dictionary<(int, bool), IconPainter>();

        public IconDrawerFactory(Canvas canvas) {
            this.canvas = canvas;

            // Sunny day
            allIcons[(CLEAR_SKY, true)] = new SunIcon(canvas: canvas, scale: 0.4, x: 0.5, y: 0.5);

            // Clear night sky (moon and start)
            allIcons[(CLEAR_SKY, false)] = new MoonIcon(
                next: new StarIcon( 
                    next: new StarIcon( 
                        scale: 0.1, x: 0.7, y: 0.45, increment: 0.0005, canvas: canvas
                    ), scale: 0.05, x: 0.6, y: 0.3, increment: -0.00025, canvas: canvas
                ), scale: 0.4, x: 0.75, y: 0.25, canvas: canvas
            );

            // Partly cloudy with sun
            allIcons[(PARTY_CLOUDY, true)] = new CloudIcon(
                next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                scale: 0.3, x: 0.4, y: 0.5, canvas: canvas
            );

            // Partly cloudy with moon
            allIcons[(PARTY_CLOUDY, false)] = new CloudIcon(
                next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                scale: 0.3, x: 0.4, y: 0.5, canvas: canvas
            );

            allIcons[(CLOUDY, true)] = allIcons[(CLOUDY, false)] = 
                new CloudIcon(scale: 0.3, x: 0.5, y: 0.5, canvas: canvas);

            allIcons[(OVERCAST, true)] = allIcons[(OVERCAST, false)] = 
                new CloudIcon(scale: 0.3, x: 0.5, y: 0.5, canvas: canvas);


            allIcons[(MIST, true)] = allIcons[(MIST, false)] = new CloudIcon(
                next: new MistIcon(
                    next: new MistIcon(x: 0.2, y: 0.85, width: 0.65, height: 0.05, canvas: canvas),
                    x: 0.2, y: 0.95, width: 0.65, height: 0.05, canvas: canvas, negativeIncrement: true
                    ),
                scale: 0.3, x: 0.5, y: 0.5, canvas: canvas
                );

            allIcons[(PATCHY_RAIN_POSSIBLE, true)] = new RainIcon(
               next: new CloudIcon(
                   next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(PATCHY_RAIN_POSSIBLE, false)] = new RainIcon(
                next: new CloudIcon(
                    next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                    scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
                );

            allIcons[(PATCHY_SNOW_POSSIBLE, true)] = new SnowIcon(
               next: new CloudIcon(
                   next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(PATCHY_SNOW_POSSIBLE, false)] = new SnowIcon(
                next: new CloudIcon(
                    next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                    scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
                );

            allIcons[(PATCHY_SLEET_POSSIBLE, true)] = new RainIcon(
                next: new SnowIcon(
                    next: new CloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.3, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.35, height: 0.4
                );

            allIcons[(PATCHY_SLEET_POSSIBLE, false)] = new RainIcon(
                next: new SnowIcon(
                    next: new CloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.3, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.35, height: 0.4
                );
           
            allIcons[(PATCHY_FREEZING_DRIZZLE_POSSIBLE, true)] = new FreezingDrizzleRainIcon(
               next: new CloudIcon(
                   next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            
            allIcons[(PATCHY_FREEZING_DRIZZLE_POSSIBLE, false)] = new FreezingDrizzleRainIcon(
                next: new CloudIcon(
                    next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                    scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
                );

            allIcons[(THUNDERY_OUTBREAKS_POSSIBLE, true)] = new ThunderIcon(
                next: new CloudIcon(
                    next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                    canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                ); 

            allIcons[(THUNDERY_OUTBREAKS_POSSIBLE, false)] = new ThunderIcon(
                next: new CloudIcon(
                    next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                    canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                canvas: canvas, x: 0.5, y: 0.5, width: 0.2, height: 0.2
                );

            allIcons[(BLOWING_SNOW, true)] = allIcons[(BLOWING_SNOW, false)] = new SnowIcon(
                next: new CloudIcon(canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
                );

            allIcons[(BLIZZARD, true)] = allIcons[(BLIZZARD, false)] = new SnowIcon(
                next: new SnowIcon(
                    next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.275, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.35, height: 0.4
                );


            allIcons[(FOG, true)] = allIcons[(FOG, false)] = new CloudIcon(
                next: new MistIcon(
                    next: new MistIcon(x: 0.2, y: 0.85, width: 0.65, height: 0.05, canvas: canvas),
                    x: 0.2, y: 0.95, width: 0.65, height: 0.05, canvas: canvas, negativeIncrement: true),
                scale: 0.3, x: 0.5, y: 0.5, canvas: canvas
                );

            allIcons[(FREEZING_FOG, true)] = allIcons[(FREEZING_FOG, false)] = new CloudIcon(
                next: new MistIcon(
                    next: new MistIcon(x: 0.2, y: 0.85, width: 0.65, height: 0.05, canvas: canvas),
                    x: 0.2, y: 0.95, width: 0.65, height: 0.05, canvas: canvas, negativeIncrement: true),
                scale: 0.3, x: 0.5, y: 0.5, canvas: canvas
                );

            allIcons[(LIGHT_DRIZZLE, true)] = allIcons[(LIGHT_DRIZZLE, false)] = 
                allIcons[(PATCHY_LIGHT_DRIZZLE, true)] = allIcons[(PATCHY_LIGHT_DRIZZLE, false)] = new RainIcon(
                next: new RainIcon(
                    next: new RainIcon(
                        next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                        canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.3, offset: 0.2),
                    canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.3, offset: 0.1),
                canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.3
                );



        }

        public IconPainter GetIconPainter(int code, bool isDay) {
            return allIcons[(code, isDay)];
        }

    }
}