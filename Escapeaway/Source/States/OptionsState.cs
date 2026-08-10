using Escapeaway;
using Escapeaway.Source.Graphics;
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
        private List<Text> buttons = new List<Text>();
        private byte
            buttonSelected = 0,
            maxButtons;

        // Temporary Variables
        private int
            newHighscore = 0;

        public OptionsState()
        {
            // Display Text
            header = new Text(Global.defaultFont, "Options", new Vector2((Global.resWidth / 2) - 26, 6), Color.White, 1.0f, false);
            goBack = new Text(Global.defaultFont, "Press [X] to Exit", new Vector2((Global.resWidth / 2) - 120, 204), Color.White, 1.0f, false);

            // Buttons
            buttons.Add(new Text(Global.defaultFont, "Fullscreen", new Vector2(6, 24), CustomColor.LightOrange, 1.0f, false));
            buttons.Add(new Text(Global.defaultFont, "Reset HISCORE", new Vector2(6, 48), Color.White, 1.0f, false));

            maxButtons = Convert.ToByte(buttons.Count - 1);

            UpdateButtonText();
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
                if (buttonSelected < maxButtons) SelectButton(buttons[buttonSelected]);
            }

            // Button Presses
            if (KeyPress(Keys.Z) || KeyPress(Keys.Enter))
            {
                // Toggle Fullscreen
                if (buttonSelected == 0)
                {
                    Global.fullscreen = !Global.fullscreen;
                    Global.fullscreenChanged = true;
                }

                // Reset Highscore
                if (buttonSelected == 1)
                {
                    Global.highscore = newHighscore;
                }

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

        private void SelectButton(Text buttonToHighlight)
        {
            foreach (var button in buttons)
            {
                button.setColor(Color.White);
            }

            buttonToHighlight.setColor(CustomColor.LightOrange);
        }

        private void UpdateButtonText()
        {
            buttons[0].setText("Fullscreen " + (Global.fullscreen ? enabled : disabled));
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            // Draw Display Text
            header.Draw(spriteBatch);
            goBack.Draw(spriteBatch);

            // Draw Buttons
            foreach (var button in buttons) button.Draw(spriteBatch);
        }
    }
}
