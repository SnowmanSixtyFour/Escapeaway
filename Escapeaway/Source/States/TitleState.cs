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

        private Text version, highscore;

        // Buttons

        private List<Text> buttons = new List<Text>();
        private Text
            start,
            endless,
            options,
            story,
            exit;

        private byte
            buttonSelected = 0,
            maxButtons;
        private int
            buttonX = 22,
            buttonY = 80;

        public TitleState()
        {
            // Graphics

            logo = new Character(Global.logo, new Point((Global.resWidth / 2) - (logoWidth / 2), logoY), new Point(logoSheetSize, logoHeight), new Point(logoWidth, logoHeight), Color.White);
            logo.CreateAnimation("default", 0, 6);

            highscore = new Text(Global.defaultFont, "", new Vector2(62, logoY + 34), CustomColor.Yellow, 1.0f, false);
            SetHighscore();

            overlay = new StaticSprite(Global.titleOverlay, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.White);

            copyright = new Text(Global.defaultFont, copyrightString, new Vector2(62, 196), Color.White, 1.0f, false);
            version = new Text(Global.defaultFont, Global.gameVersion, new Vector2((Global.resWidth / 2) - 120, 204), Color.White, 1.0f, false);

            // Buttons
            buttons.Add(new Text(Global.defaultFont, "Start", new Vector2(buttonX, buttonY), Color.White, 1.0f, true));
            buttons.Add(new Text(Global.defaultFont, "Endless", new Vector2(buttonX, buttonY + 20), Color.White, 1.0f, true));
            buttons.Add(new Text(Global.defaultFont, "Options", new Vector2(buttonX, buttonY + 40), Color.White, 1.0f, true));
            buttons.Add(new Text(Global.defaultFont, "Help", new Vector2(buttonX, buttonY + 60), Color.White, 1.0f, true));
            buttons.Add(new Text(Global.defaultFont, "Exit", new Vector2(buttonX, buttonY + 80), Color.White, 1.0f, true));

            maxButtons = Convert.ToByte(buttons.Count); // Set Num of Max Buttons

            SelectButton(buttons[0]); // Select First Button by Default
        }

        private void SetHighscore()
        {
            highscore.setText("HISCORE " + Global.highscore);

            // Little easter eggs, they're unoptimized but I don't care :)

            // 666
            if (Global.highscore == 6 ||
                Global.highscore == 66 ||
                Global.highscore == 666 ||
                Global.highscore == 6666 ||
                Global.highscore == 66666 ||
                Global.highscore == 666666 ||
                Global.highscore == 6666666 ||
                Global.highscore == 66666666 ||
                Global.highscore == 666666666)
            {
                highscore.setColor(CustomColor.Red);
            }

            // Illegal score
            if (Global.highscore < 0)
            {
                highscore.setText("""
                    THE DEVIL KNOWS
                         A CHEATER!
                    """);
            }
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // Update Selected Button
            if (KeyPress(Keys.Up))
            {
                if (buttonSelected != 0) buttonSelected--;
                else buttonSelected = Convert.ToByte(maxButtons - 1);
            }
            if (KeyPress(Keys.Down))
            {
                if (buttonSelected < Convert.ToByte(maxButtons - 1)) buttonSelected++;
                else buttonSelected = 0;
            }
            if (KeyPress(Keys.Up) || KeyPress(Keys.Down))
            {
                // Set Color of Selected Button (don't if value is past limit)
                if (buttonSelected < maxButtons) SelectButton(buttons[buttonSelected]);
            }

            // Button Presses
            if (KeyPress(Keys.Z) || KeyPress(Keys.Enter))
            {
                // Regular Mode
                if (buttonSelected == 0)
                {
                    // Reset Level State
                    main.level.ResetLevel();
                    main.endless = false; // Disable Endless Mode (in case it was on)

                    // Go to Level
                    SwitchState(main.level);
                }

                // Endless Mode
                else if (buttonSelected == 1)
                {
                    main.level.ResetLevel();
                    main.endless = true; // Enable Endless Mode

                    SwitchState(main.level);
                }

                // Options
                else if (buttonSelected == 2) SwitchState(main.options);

                // Help
                else if (buttonSelected == 3) SwitchState(main.story);

                // Quit
                else if (buttonSelected == 4) ExitGame();
            }

            // Animations
            logo.PlayAnimation("default");
        }

        // Update Colors of Buttons
        private void SelectButton(Text buttonToHighlight)
        {
            // Reset all Button Colors
            foreach(Text button in buttons)
            {
                button.setColor(Color.White);
            }

            // Update Chosen Button to be Selected Color
            buttonToHighlight.setColor(CustomColor.LightOrange);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            overlay.Draw(spriteBatch);

            logo.Draw(spriteBatch);
            if (Global.highscore != 0) highscore.Draw(spriteBatch);
            copyright.Draw(spriteBatch);
            version.Draw(spriteBatch);
            foreach (Text button in buttons) button.Draw(spriteBatch);
        }
    }
}
