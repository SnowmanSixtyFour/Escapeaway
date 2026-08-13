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
using Escapeaway.Source.States.GUI;

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
        private ButtonList buttons;
        private int
            buttonX = 22,
            buttonY = 80;

        public TitleState()
        {
            // Graphics

            logo = new Character(Global.logo, new Point((Global.resWidth / 2) - (logoWidth / 2), logoY), new Point(logoSheetSize, logoHeight), new Point(logoWidth, logoHeight), CustomColor.White);
            logo.CreateAnimation("default", 0, 6);

            highscore = new Text(Global.defaultFont, "", new Vector2(62, logoY + 34), CustomColor.Yellow, 1.0f, false);
            SetHighscore();

            overlay = new StaticSprite(Global.titleOverlay, new Rectangle(0, 0, Global.resWidth, Global.resHeight), CustomColor.White);

            copyright = new Text(Global.defaultFont, copyrightString, new Vector2(62, 196), CustomColor.White, 1.0f, false);
            version = new Text(Global.defaultFont, Global.gameVersion, new Vector2((Global.resWidth / 2) - 120, 204), CustomColor.White, 1.0f, false);

            // Buttons
            buttons = new ButtonList();
            buttons.Add("Start", new Vector2(buttonX, buttonY), CustomColor.White);
            buttons.Add("Endless Mode", new Vector2(buttonX, buttonY + 20), CustomColor.White);
            buttons.Add("Options", new Vector2(buttonX, buttonY + 40), CustomColor.White);
            buttons.Add("Story", new Vector2(buttonX, buttonY + 60), CustomColor.White);
            buttons.Add("Exit", new Vector2(buttonX, buttonY + 80), CustomColor.White);
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
                Global.highscore == 6666666)
            {
                highscore.setColor(CustomColor.Red);
            }

            // Illegal score
            if (Global.highscore < 0 || Global.highscore > Global.maxScore)
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
            buttons.Update(gameTime, this);

            // Button Presses

            // Regular Mode
            if (buttons.ButtonSelected(0, this))
            {
                // Reset Level State
                main.endless = false; // Disable Endless Mode (in case it was on)
                main.level.GoBackToFirstRoom();

                // Go to Level
                SwitchState(main.level);
            }

            // Endless Mode
            else if (buttons.ButtonSelected(1, this))
            {
                main.endless = true; // Enable Endless Mode
                main.level.GoBackToFirstRoom();

                SwitchState(main.level);
            }

            // Options
            else if (buttons.ButtonSelected(2, this)) SwitchState(main.options);

            // Help
            else if (buttons.ButtonSelected(3, this)) SwitchState(main.story);

            // Quit
            else if (buttons.ButtonSelected(4, this)) ExitGame();

            // Animations

            logo.PlayAnimation("default");
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(CustomColor.Black);

            overlay.Draw(spriteBatch);

            logo.Draw(spriteBatch);
            if (Global.highscore != 0) highscore.Draw(spriteBatch);
            copyright.Draw(spriteBatch);
            version.Draw(spriteBatch);
            buttons.Draw(spriteBatch);
        }
    }
}
