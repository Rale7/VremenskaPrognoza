
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows.Controls;
using VremenskaPrognoza.View.IconDrawers.Common;
using VremenskaPrognoza.View.IconDrawers.Day;
using VremenskaPrognoza.View.IconDrawers.Night;

namespace VremenskaPrognoza.View.IconDrawers 
{
    class IconDrawerFactory {
        private Canvas canvas;

        public const int CLEAR_SKY = 1000;
        public const int PARTY_CLOUDY = 1003;
        public const int CLOUDY = 1006;
        public const int OVERCAST = 1009;
        public const int MIST = 1030;
        public const int PATCHY_RAIN_POSSIBLE = 1063;
        public const int PATCHY_SNOW_POSSIBLE = 1066;
        public const int PATCHY_SLEET_POSSIBLE = 1069;
        public const int PATCHY_FREEZING_DRIZZLE_POSSIBLE = 1072;
        public const int THUNDERY_OUTBREAKS_POSSIBLE = 1087;
        public const int BLOWING_SNOW = 1114;
        public const int BLIZZARD = 1117;
        public const int FOG = 1135;
        public const int FREEZING_FOG = 1147;
        public const int PATCHY_LIGHT_DRIZZLE = 1150;
        public const int LIGHT_DRIZZLE = 1153;
        public const int FREEZING_DRIZZLE = 1168;
        public const int HEAVY_FREEZING_DRIZZLE = 1171;
        public const int PATCHY_LIGHT_RAIN = 1180;
        public const int LIGHT_RAIN = 1183;
        public const int MODERATE_RAIN_AT_TIMES = 1186;
        public const int MODERATE_RAIN = 1189;
        public const int HEAVY_RAIN_AT_TIMES = 1192;
        public const int HEAVY_RAIN = 1195;
        public const int LIGHT_FREEZING_RAIN = 1198;
        public const int MODERATE_OR_HEAVY_FREEZING_RAIN = 1201;
        public const int LIGHT_SLEET = 1204;
        public const int MODERATE_OR_HEAVY_SLEET = 1207;
        public const int PATCHY_LIGHT_SNOW = 1210;
        public const int LIGHT_SNOW = 1213;
        public const int PATCHY_MODERATE_SNOW = 1216;
        public const int MODERATE_SNOW = 1219;
        public const int PATCHY_HEAVY_SNOW = 1222;
        public const int HEAVY_SNOW = 1225;
        public const int ICE_PELLETS = 1237;
        public const int LIGHT_RAIN_SHOWER = 1240;
        public const int MODERATE_OR_HEAVY_RAIN_SHOWER = 1243;
        public const int TORENTIAL_RAIN_SHOWER = 1246;
        public const int LIGHT_SLEET_SHOWERS = 1249;
        public const int MODERATE_OR_HEAVY_SLEET_SHOWERS = 1252;
        public const int LIGHT_SNOW_SHOWER = 1255;
        public const int MODERATE_OR_HEAVY_SNOW_SHOWERS = 1258;
        public const int LIGHT_SHOWER_OF_ICE_PELLETS = 1261;
        public const int MODERATE_OR_HEAVY_SHOWERS_OF_ICE_PELLETS = 1264;
        public const int PATCHY_LIGHT_RAIN_WITH_THUNDER = 1273;
        public const int MODERATE_OR_HEAVY_RAIN_WITH_THUNDER = 1276;
        public const int PATCHY_LIGHT_SNOW_WITH_THUNDER = 1279;
        public const int MODERATE_OR_HEAVY_SNOW_WITH_THUNDER = 1282;

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

            allIcons[(MODERATE_RAIN_AT_TIMES, true)] = allIcons[(LIGHT_RAIN_SHOWER, true)] =
            allIcons[(PATCHY_LIGHT_RAIN, true)] = allIcons[(PATCHY_RAIN_POSSIBLE, true)] = new RainIcon(
               next: new CloudIcon(
                   next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_RAIN_AT_TIMES, false)] = allIcons[(LIGHT_RAIN_SHOWER,  false)] =
            allIcons[(PATCHY_LIGHT_RAIN, false)] = allIcons[(PATCHY_RAIN_POSSIBLE, false)] = new RainIcon(
                next: new CloudIcon(
                    next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                    scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
                );
            allIcons[(PATCHY_MODERATE_SNOW, true)] = allIcons[(LIGHT_SNOW_SHOWER, true)] =
            allIcons[(PATCHY_LIGHT_SNOW, true)] = allIcons[(PATCHY_SNOW_POSSIBLE, true)] = new SnowIcon(
               next: new CloudIcon(
                   next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(PATCHY_MODERATE_SNOW, false)] = allIcons[(LIGHT_SNOW_SHOWER, false)] =
            allIcons[(PATCHY_LIGHT_SNOW, false)] = allIcons[(PATCHY_SNOW_POSSIBLE, false)] = new SnowIcon(
                next: new CloudIcon(
                    next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                    scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
                );

            allIcons[(LIGHT_SLEET_SHOWERS, true)] =
            allIcons[(PATCHY_SLEET_POSSIBLE, true)] = new RainIcon(
                next: new SnowIcon(
                    next: new CloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.27, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.17, y: 0.7, width: 0.35, height: 0.4
                );

            allIcons[(LIGHT_SLEET_SHOWERS, false)] =
            allIcons[(PATCHY_SLEET_POSSIBLE, false)] = new RainIcon(
                next: new SnowIcon(
                    next: new CloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.27, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.17, y: 0.7, width: 0.35, height: 0.4
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
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
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

            allIcons[(FREEZING_DRIZZLE, true)] = allIcons[(FREEZING_DRIZZLE, false)] = new FreezingDrizzleRainIcon(
                next: new FreezingDrizzleRainIcon(
                    next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.3, offset: 0.2),
                canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.3
                );

            allIcons[(HEAVY_FREEZING_DRIZZLE, true)] = allIcons[(HEAVY_FREEZING_DRIZZLE, false)] = new FreezingDrizzleRainIcon(
                next: new FreezingDrizzleRainIcon(
                    next: new FreezingDrizzleRainIcon(
                        next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                        canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.3, offset: 0.2),
                    canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.3, offset: 0.1),
                canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.3
                );

            allIcons[(MODERATE_RAIN_AT_TIMES, true)] = allIcons[(MODERATE_RAIN_AT_TIMES, false)] =
            allIcons[(LIGHT_RAIN, true)] = allIcons[(LIGHT_RAIN, false)] = new RainIcon(
               next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_OR_HEAVY_RAIN_SHOWER, true)] =
            allIcons[(HEAVY_RAIN_AT_TIMES, true)] = new RainIcon(
               next: new RainIcon(
                    next: new CloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_OR_HEAVY_RAIN_SHOWER, false)] =
            allIcons[(HEAVY_RAIN_AT_TIMES, false)] = new RainIcon(
               next: new RainIcon(
                    next: new CloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(HEAVY_RAIN, true)] = allIcons[(HEAVY_RAIN, false)] = new RainIcon(
               next: new RainIcon(
                    next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );


            allIcons[(LIGHT_FREEZING_RAIN, true)] = allIcons[(LIGHT_FREEZING_RAIN, false)] =  new FreezingDrizzleRainIcon(
               next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_OR_HEAVY_FREEZING_RAIN, true)] = 
                allIcons[(MODERATE_OR_HEAVY_FREEZING_RAIN, false)] = new FreezingDrizzleRainIcon(
               next: new FreezingDrizzleRainIcon(
                    next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(LIGHT_SLEET, true)] = allIcons[(LIGHT_SLEET, false)] = new RainIcon(
                next: new SnowIcon(
                    next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.27, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.17, y: 0.7, width: 0.35, height: 0.4
            );

            allIcons[(MODERATE_OR_HEAVY_SLEET, true)] =
                allIcons[(MODERATE_OR_HEAVY_SLEET, false)] = new RainIcon(
                next: new SnowIcon(
                    next: new RainIcon(
                        next: new SnowIcon(
                            next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                            canvas: canvas, x: 0.46, y: 0.7, width: 0.2, height: 0.4, offset: 0.2),
                         canvas: canvas, x: 0.41, y: 0.7, width: 0.2, height: 0.4),
                    canvas: canvas, x: 0.21, y: 0.7, width: 0.2, height: 0.4, offset: 0.15),
                canvas: canvas, x: 0.16, y: 0.7, width: 0.2, height: 0.4
            );

            allIcons[(MODERATE_SNOW, true)] = allIcons[(MODERATE_SNOW, false)] =
            allIcons[(LIGHT_SNOW, true)] = allIcons[(LIGHT_SNOW, false)] = new SnowIcon(
               next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(PATCHY_HEAVY_SNOW, true)] = new SnowIcon(
               next: new SnowIcon(
                    next: new CloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );


            allIcons[(PATCHY_HEAVY_SNOW, false)] = new SnowIcon(
               next: new SnowIcon(
                    next: new CloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(HEAVY_SNOW, false)] = allIcons[(HEAVY_SNOW, true)] = new SnowIcon(
               next: new SnowIcon(
                    next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(ICE_PELLETS, true)] = allIcons[(ICE_PELLETS, false)] = new IcePelletIcon(
               next: new CloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(TORENTIAL_RAIN_SHOWER, true)] = new RainIcon(
               next: new RainIcon(
                    next: new DarkCloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(TORENTIAL_RAIN_SHOWER, false)] = new RainIcon(
               next: new RainIcon(
                    next: new DarkCloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_OR_HEAVY_SLEET_SHOWERS, true)] = new RainIcon(
                next: new SnowIcon(
                    next: new RainIcon(
                        next: new SnowIcon(
                            next: new CloudIcon(
                                next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                                scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                            canvas: canvas, x: 0.46, y: 0.7, width: 0.2, height: 0.4, offset: 0.2),
                         canvas: canvas, x: 0.41, y: 0.7, width: 0.2, height: 0.4),
                    canvas: canvas, x: 0.21, y: 0.7, width: 0.2, height: 0.4, offset: 0.15),
                canvas: canvas, x: 0.16, y: 0.7, width: 0.2, height: 0.4
            );

            allIcons[(MODERATE_OR_HEAVY_SLEET_SHOWERS, false)] = new RainIcon(
                next: new SnowIcon(
                    next: new RainIcon(
                        next: new SnowIcon(
                            next: new CloudIcon(
                                next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                                scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                            canvas: canvas, x: 0.46, y: 0.7, width: 0.2, height: 0.4, offset: 0.2),
                         canvas: canvas, x: 0.41, y: 0.7, width: 0.2, height: 0.4),
                    canvas: canvas, x: 0.21, y: 0.7, width: 0.2, height: 0.4, offset: 0.15),
                canvas: canvas, x: 0.16, y: 0.7, width: 0.2, height: 0.4
            );

            allIcons[(MODERATE_OR_HEAVY_SNOW_SHOWERS, true)] = new SnowIcon(
                next: new SnowIcon(
                    next: new CloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.275, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.35, height: 0.4
                );

            allIcons[(MODERATE_OR_HEAVY_SNOW_SHOWERS, false)] = new SnowIcon(
                next: new SnowIcon(
                    next: new CloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.275, y: 0.7, width: 0.35, height: 0.4, offset: 0.2),
                canvas: canvas, x: 0.2, y: 0.7, width: 0.35, height: 0.4
                );

            allIcons[(LIGHT_SHOWER_OF_ICE_PELLETS, true)] = new IcePelletIcon(
               next: new CloudIcon(
                   next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(LIGHT_SHOWER_OF_ICE_PELLETS, false)] = new IcePelletIcon(
               next: new CloudIcon(
                   next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                   scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
               canvas: canvas, x: 0.2, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_OR_HEAVY_SHOWERS_OF_ICE_PELLETS, true)] = new IcePelletIcon(
               next: new IcePelletIcon(
                    next: new CloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(MODERATE_OR_HEAVY_SHOWERS_OF_ICE_PELLETS, false)] = new IcePelletIcon(
               next: new IcePelletIcon(
                    next: new CloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                    canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
               canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4
               );

            allIcons[(PATCHY_LIGHT_RAIN_WITH_THUNDER, true)] = new ThunderIcon(
                next: new RainIcon(
                    next: new DarkCloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                    canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                ); 

            allIcons[(PATCHY_LIGHT_RAIN_WITH_THUNDER, false)] = new ThunderIcon(
                next: new RainIcon( 
                    next: new DarkCloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                    canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                );

            allIcons[(MODERATE_OR_HEAVY_RAIN_WITH_THUNDER, false)] = allIcons[(MODERATE_OR_HEAVY_RAIN_WITH_THUNDER, true)] =
                new ThunderIcon(
                    next: new RainIcon(
                       next: new RainIcon(
                            next: new DarkCloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                            canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
                       canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                );

            allIcons[(PATCHY_LIGHT_SNOW_WITH_THUNDER, true)] = new ThunderIcon(
                next: new SnowIcon(
                    next: new DarkCloudIcon(
                        next: new SunIcon(scale: 0.28, x: 0.6, y: 0.4, canvas: canvas),
                        canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                    canvas: canvas, x: 0.22, y: 0.7, width: 0.4, height: 0.4),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                ); 

            allIcons[(PATCHY_LIGHT_SNOW_WITH_THUNDER, false)] = new ThunderIcon(
                next: new SnowIcon( 
                    next: new DarkCloudIcon(
                        next: new MoonIcon(scale: 0.28, x: 0.85, y: 0.15, canvas: canvas),
                        canvas: canvas, scale: 0.3, x: 0.4, y: 0.5),
                    canvas: canvas, x: 0.22, y: 0.7, width: 0.4, height: 0.4),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                );

            allIcons[(MODERATE_OR_HEAVY_SNOW_WITH_THUNDER, false)] = allIcons[(MODERATE_OR_HEAVY_RAIN_WITH_THUNDER, true)] =
                new ThunderIcon(
                    next: new SnowIcon(
                       next: new SnowIcon(
                            next: new DarkCloudIcon(scale: 0.3, x: 0.4, y: 0.5, canvas: canvas),
                            canvas: canvas, x: 0.15, y: 0.7, width: 0.4, height: 0.4, offset: 0.2),
                       canvas: canvas, x: 0.25, y: 0.7, width: 0.4, height: 0.4),
                canvas: canvas, x: 0.4, y: 0.6, width: 0.4, height: 0.4
                );



        }

        public IconPainter GetIconPainter(int code, bool isDay) {
            return allIcons[(code, isDay)];
        }

    }
}