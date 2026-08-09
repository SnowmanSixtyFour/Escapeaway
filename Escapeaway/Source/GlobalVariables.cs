using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Escapeaway.Source
{
    internal class Global
    {
        // Properties

        public static bool quit = false;

        public static string windowName = "ESCAPEAWAY!";
        public static int
            resWidth = 256,
            resHeight = 224;

        public static int
            displayWidth = 768,
            displayHeight = 672;

        public static string gameVersion = "1.0.0";

        public static bool pauseWhenInactive = true;

        // Game

        public static bool active = true;
        public static bool paused = false;

        // Assets

        public static Texture2D
            noImg,

            logo, titleOverlay;
        public static SpriteFont
            defaultFont;

        public static Color
            selectedColor = new Color(248, 120, 88);

        public static void LoadAssets(ContentManager Content)
        {
            // Images
            noImg = Content.Load<Texture2D>("Assets/Images/pixel");

            logo = Content.Load<Texture2D>("Assets/Images/Title/logo");
            titleOverlay = Content.Load<Texture2D>("Assets/Images/Title/titleOverlay");

            // Fonts
            defaultFont = Content.Load<SpriteFont>("Assets/Fonts/retroFont");
        }
    }
}
