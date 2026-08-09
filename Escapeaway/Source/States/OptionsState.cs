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
        private Text
            fullscreen;
        private byte
            buttonSelected = 0,
            maxButtons = 0;

        public OptionsState()
        {
            // Display Text
            header = new Text(Global.defaultFont, "Options", new Vector2((Global.resWidth / 2) - 26, 6), Color.White, 1.0f, false);
            goBack = new Text(Global.defaultFont, "Press [X] to Exit", new Vector2((Global.resWidth / 2) - 120, 204), Color.White, 1.0f, false);

            // Buttons
            fullscreen = new Text(Global.defaultFont, "Fullscreen", new Vector2(6, 24), Global.selectedColor, 1.0f, false);
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
                if (buttonSelected == 0) SelectButton(fullscreen);
            }

            // Button Presses
            if (KeyPress(Keys.Z) || KeyPress(Keys.Enter))
            {
                if (buttonSelected == 0)
                {
                    // Toggle Fullscreen
                    Global.fullscreen = !Global.fullscreen;
                    Global.fullscreenChanged = true;
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

                    settingsDoc.Save("C:/Users/" + Environment.UserName + "/Documents/My Games/ESCAPEAWAY!/Options.xml", SaveOptions.None);

                    Debug.Print("Saved to Settings.xml.");
                }
                else // If Options.xml does not exist
                {
                    Global.checkAndCreateOptions = true;
                }

                // Go to Title
                SwitchState(main.title);
            }
        }

        private void SelectButton(Text button)
        {
            fullscreen.setColor(Color.White);

            button.setColor(Global.selectedColor);
        }

        private void UpdateButtonText()
        {
            fullscreen.setText("Fullscreen: " + (Global.fullscreen ? enabled : disabled));
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            // Draw Display Text
            header.Draw(spriteBatch);
            goBack.Draw(spriteBatch);

            // Draw Buttons
            fullscreen.Draw(spriteBatch);
        }
    }
}
