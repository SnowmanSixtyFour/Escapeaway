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
using Escapeaway.Source.States.Level;

namespace Escapeaway.Source.States
{
    internal class LevelState : State
    {
        Player player;
        PauseOverlay pauseOverlay;
        HUD hud;

        public int
            currentScreen = 0,
            
            randomScreenColor = 0;
        private Random random;
        private Color screenColor = CustomColor.Red;

        public LevelState()
        {
            // Set Variables
            random = new Random();

            // Initialize Level
            player = new Player(null, new Point(0, 120), Color.White);

            // Visuals
            hud = new HUD();
            pauseOverlay = new PauseOverlay();
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // While Unpaused
            if (!Global.paused)
            {
                player.Update(gameTime);

                if (player.reachedEnd)
                {
                    currentScreen++;
                    randomScreenColor = random.Next(0, 3);

                    if (randomScreenColor == 0) screenColor = CustomColor.Red;
                    else if (randomScreenColor == 1) screenColor = CustomColor.DarkRed;
                    else if (randomScreenColor == 2) screenColor = CustomColor.Brown;

                    player.reachedEnd = false;
                }
            }

            // While Paused
            hud.Update(gameTime, player, currentScreen);

            if (Global.paused)
            {
                // Quit to Title
                if (KeyPress(Keys.X)) SwitchState(main.title);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(screenColor);

            player.Draw(spriteBatch);

            hud.Draw(spriteBatch);
            pauseOverlay.Draw(spriteBatch);
        }
    }
}
