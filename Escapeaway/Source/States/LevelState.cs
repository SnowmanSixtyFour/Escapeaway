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

        public int currentScreen = 0;

        public LevelState()
        {
            // Initialize Level
            player = new Player(null, new Point(0, 120), new Point(20, 40), new Point(20, 40), Color.White);

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
            graphicsDevice.Clear(Global.redSky);

            player.Draw(spriteBatch);

            hud.Draw(spriteBatch);
            pauseOverlay.Draw(spriteBatch);
        }
    }
}
