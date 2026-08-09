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

        private StaticSprite overlay;

        // Yes, I made the copyright symbol a hashtag. Deal with it
        private Text copyright;
        private String copyrightString =
            """
                    # Snowman64 2026
            Made for BOSS BASH JAM 4
            """;

        private Text version;

        // Buttons

        private Text
            start,
            options,
            story,
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

            overlay = new StaticSprite(Global.titleOverlay, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.White);

            copyright = new Text(Global.defaultFont, copyrightString, new Vector2(62, 196), Color.White, 1.0f, false);

            version = new Text(Global.defaultFont, Global.gameVersion, new Vector2(6, 204), Color.White, 1.0f, false);

            // Buttons

            start = new Text(Global.defaultFont, "Start", new Vector2(buttonX, buttonY), Color.White, 1.0f, true);
            options = new Text(Global.defaultFont, "Options", new Vector2(buttonX, buttonY + 20), Color.White, 1.0f, true);
            story = new Text(Global.defaultFont, "Story", new Vector2(buttonX, buttonY + 40), Color.White, 1.0f, true);
            exit = new Text(Global.defaultFont, "Exit", new Vector2(buttonX, buttonY + 60), Color.White, 1.0f, true);

            SelectButton(start);
        }

        public override void OnUpdate(GameTime gameTime, Main main)
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
                else if (buttonSelected == 2) SelectButton(story);
                else if (buttonSelected == 3) SelectButton(exit);
            }

            // Button Presses
            if (KeyPress(Keys.Z) || KeyPress(Keys.Enter))
            {
                if (buttonSelected == 0) SwitchState(main.level);
                else if (buttonSelected == 1) SwitchState(main.options);
                else if (buttonSelected == 2) SwitchState(main.story);
                else if (buttonSelected == 3) ExitGame();
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
            story.setColor(Color.White);
            exit.setColor(Color.White);

            // Update Chosen Button to be Selected Color
            button.setColor(Global.selectedColor);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            overlay.Draw(spriteBatch);

            logo.Draw(spriteBatch);
            copyright.Draw(spriteBatch);
            version.Draw(spriteBatch);

            start.Draw(spriteBatch);
            options.Draw(spriteBatch);
            story.Draw(spriteBatch);
            exit.Draw(spriteBatch);
        }
    }
}
