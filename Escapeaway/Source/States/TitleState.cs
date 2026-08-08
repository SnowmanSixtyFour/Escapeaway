using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway;
using Escapeaway.Source.Graphics;
using Escapeaway.Source.Objects;

namespace Escapeaway.Source.States
{
    internal class TitleState : State
    {
        // Graphics

        private Character logo;
        private int
            logoSheetSize = 1190,

            logoWidth = 170,
            logoHeight = 27,

            logoY = 24;

        // Yes, I made the copyright symbol a hashtag. Deal with it
        private Text copyright;
        private String copyrightString =
            """
                    # Snowman64 2026
            Made for BOSS BASH JAM 4
            """;

        // Buttons

        private Text
            start,
            options,
            credits,
            exit;

        private byte
            buttonSelected = 0,
            maxButtons = 3;
        private int
            buttonX = 22,
            buttonY = 80;

        public TitleState()
        {
            // Graphics

            logo = new Character(Global.logo, new Point((Global.resWidth / 2) - (logoWidth / 2), logoY), new Point(logoSheetSize, logoHeight), new Point(logoWidth, logoHeight), Color.White);
            logo.CreateAnimation("default", 0, 6);

            copyright = new Text(Global.defaultFont, copyrightString, new Vector2(56, 196), Color.White, 1.0f, false);

            // Buttons

            start = new Text(Global.defaultFont, "Start", new Vector2(buttonX, buttonY), Color.White, 1.0f, true);
            options = new Text(Global.defaultFont, "Options", new Vector2(buttonX, buttonY + 20), Color.White, 1.0f, true);
            credits = new Text(Global.defaultFont, "Credits", new Vector2(buttonX, buttonY + 40), Color.White, 1.0f, true);
            exit = new Text(Global.defaultFont, "Exit", new Vector2(buttonX, buttonY + 60), Color.White, 1.0f, true);

            SelectButton(start);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Update Selected Button

            if (KeyPress(Keys.Up))
            {
                if (buttonSelected != 0) buttonSelected--;
                else buttonSelected = maxButtons;
            }
            if (KeyPress(Keys.Down))
            {
                if (buttonSelected < maxButtons) buttonSelected++;
                else buttonSelected = 0;
            }
            if (KeyPress(Keys.Up) || KeyPress(Keys.Down))
            {
                if (buttonSelected == 0) SelectButton(start);
                else if (buttonSelected == 1) SelectButton(options);
                else if (buttonSelected == 2) SelectButton(credits);
                else if (buttonSelected == 3) SelectButton(exit);
            }

            // Button Press

            if (KeyPress(Keys.Enter))
            {
                if (buttonSelected == 3) Global.quit = true;
            }

            // Animations

            logo.PlayAnimation("default");
        }

        // Update Colors of Buttons
        private void SelectButton(Text button)
        {
            // Reset all Button Colors
            start.setColor(Color.White);
            options.setColor(Color.White);
            credits.setColor(Color.White);
            exit.setColor(Color.White);

            // Update Chosen Button to be Selected Color
            button.setColor(Global.selectedColor);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            logo.Draw(spriteBatch);
            copyright.Draw(spriteBatch);

            start.Draw(spriteBatch);
            options.Draw(spriteBatch);
            credits.Draw(spriteBatch);
            exit.Draw(spriteBatch);
        }
    }
}
