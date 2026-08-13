using Escapeaway;
using Escapeaway.Source.Graphics;
using Escapeaway.Source.States.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Escapeaway.Source.States
{
    internal class OptionsState : State
    {
        // Strings
        private String
            enabled = "On",
            disabled = "Off";

        // Display Text
        private Text
            header,
            goBack;

        // Buttons
        private ButtonList buttons;

        // Temporary Variables
        private int
            newHighscore = 0;

        public OptionsState()
        {
            // Display Text
            header = new Text(Global.defaultFont, "Options", new Vector2((Global.resWidth / 2) - 26, 6), CustomColor.White, 1.0f, false);
            goBack = new Text(Global.defaultFont, "Press [X] to Exit", new Vector2((Global.resWidth / 2) - 120, 204), CustomColor.White, 1.0f, false);

            // Buttons
            buttons = new ButtonList();
            buttons.Add("Fullscreen", new Vector2(6, 24), CustomColor.White, false);
            buttons.Add("Reset HISCORE", new Vector2(6, 48), CustomColor.White);

            UpdateButtonText();
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // Update Buttons
            buttons.Update(gameTime, this);

            // Button Presses
            if (KeyPress(Keys.Z) || KeyPress(Keys.Enter))
            {
                // Toggle Fullscreen
                if (buttons.ButtonSelected(0, this))
                {
                    Global.fullscreen = !Global.fullscreen;
                    Global.fullscreenChanged = true;
                }

                // Reset Highscore
                if (buttons.ButtonSelected(1, this))
                {
                    Global.highscore = newHighscore;
                }

                // Accept SFX
                SFX.intro.Play();

                UpdateButtonText();
            }

            // Exit Options
            if (KeyPress(Keys.X) || KeyPress(Keys.Escape))
            {
                // Write to Options.xml

                if (File.Exists("C:/Users/" + Environment.UserName + "/Documents/My Games/ESCAPEAWAY!/Options.xml"))
                {
                    XDocument settingsDoc = XDocument.Load("C:/Users/" + Environment.UserName + "/Documents/My Games/ESCAPEAWAY!/Options.xml");

                    settingsDoc.Descendants("Fullscreen").First().Value = Convert.ToString(Global.fullscreen);
                    if (Global.highscore == this.newHighscore) settingsDoc.Descendants("Highscore").First().Value = Convert.ToString(this.newHighscore);

                    settingsDoc.Save("C:/Users/" + Environment.UserName + "/Documents/My Games/ESCAPEAWAY!/Options.xml", SaveOptions.None);

                    Debug.Print("Saved to Options.xml.");
                }
                else // If Options.xml does not exist
                {
                    Global.checkAndCreateOptions = true;
                }

                // Go to Title
                SwitchState(main.title);
            }
        }

        private void UpdateButtonText()
        {
            buttons.GetButton(0).setText("Fullscreen " + (Global.fullscreen ? enabled : disabled));
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(CustomColor.Black);

            // Draw Display Text
            header.Draw(spriteBatch);
            goBack.Draw(spriteBatch);

            // Draw Buttons
            buttons.Draw(spriteBatch);
        }
    }
}
