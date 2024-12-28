using UnityEngine;

namespace PairUp
{

    public static class CozyColors
    {
        // Struct for background colors
        public struct BackgroundColors
        {
            public static readonly Color SoftCreamBackgroundColor = HexToColor("#F5F3E5");
            public static readonly Color CozyBrown = HexToColor("#8C6E4D");
        }

        // Struct for the standard cozy palette
        public struct CozyPalette
        {
            public static readonly Color WarmBeige = HexToColor("#D9C9A4");
            public static readonly Color SoftPeach = HexToColor("#F8D1B6");
            public static readonly Color DustyRose = HexToColor("#D3A1A1");
            public static readonly Color WarmTaupe = HexToColor("#9E8A7B");
            public static readonly Color SageGreen = HexToColor("#A4B79B");
            public static readonly Color MutedLavender = HexToColor("#B6A5C2");
            public static readonly Color SoftYellow = HexToColor("#F4E1A1");
            public static readonly Color LightOlive = HexToColor("#A9B76B");
        }

        public struct UIColors
        {
            public static readonly Color CozyGreen = HexToColor("#A7C7A2"); // Timer progress color (green)
            public static readonly Color CozyRed = HexToColor("#D77C72"); // Timer progress color (red)
            public static readonly Color SoftCoral = HexToColor("#F5A79B"); // Win/lose color or alert color

            // New colors to transition from green to red
            public static readonly Color CozyLightGreen = HexToColor("#A7D4A9"); // Light Green (start of transition)
            public static readonly Color CozyLime = HexToColor("#A9E56A"); // Lime Green (mid transition)
            public static readonly Color CozyYellowGreen = HexToColor("#A9D05A"); // Yellow-Green (mid transition)
            public static readonly Color CozyOrange = HexToColor("#D6A15B"); // Orange (near red transition)
            public static readonly Color CozySalmon = HexToColor("#D79B83"); // Salmon (near red transition)
        }

        // Helper method to convert Hex to Color
        private static Color HexToColor(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            byte r = (byte)(System.Convert.ToByte(hex.Substring(0, 2), 16));
            byte g = (byte)(System.Convert.ToByte(hex.Substring(2, 2), 16));
            byte b = (byte)(System.Convert.ToByte(hex.Substring(4, 2), 16));
            return new Color32(r, g, b, 255);
        }
    }
}