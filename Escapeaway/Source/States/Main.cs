using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway.Source;
using Escapeaway.Source.Graphics;
using Escapeaway.Source.States;

namespace Escapeaway.Source.States
{
    internal class Main : State
    {
        private State currentState;

        private TitleState title;
        private LevelState level;

        public Main()
        {
            // Set States
            title = new TitleState();
            level = new LevelState();

            // Set Current State
            currentState = title;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Pausing
            if (canPause) // If Pausing is Possible
            {
                // Toggle Pause
                if (KeyPress(Keys.Escape))
                {
                    Global.paused = !Global.paused;
                }
            }

            // Pause Game when Inactive
            if (Global.pauseWhenInactive && !Global.active) Global.paused = true;

            // Update Current State
            currentState.Update(gameTime);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Current State
            currentState.OnDraw(spriteBatch);
        }
    }
}