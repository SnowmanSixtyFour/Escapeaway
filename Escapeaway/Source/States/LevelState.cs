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

        public LevelState()
        {
            // Initialize Level
            player = new Player(null, new Point(0, 120), new Point(20, 40), new Point(20, 40), Color.White);

            // Visuals
            pauseOverlay = new PauseOverlay();
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // While Unpaused
            if (!Global.paused)
            {
                player.Update(gameTime);
            }

            // While Paused
            pauseOverlay.Update(gameTime, main);

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

            pauseOverlay.Draw(spriteBatch);
        }
    }
}
