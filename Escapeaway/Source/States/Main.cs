// The main state of the game.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway.Source;
using Escapeaway.Source.States;

namespace Escapeaway.Source.States
{
    internal class Main : State
    {
        // Current State of Game
        public State currentState;

        // Game States
        public TitleState title;
        public OptionsState options;
        public StoryState story;
        public LevelState level;

        public Main()
        {
            // Initialize States
            title = new TitleState();
            options = new OptionsState();
            story = new StoryState();
            level = new LevelState();

            // Set Current State
            currentState = title;
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // Pausing
            if (canPause) // If Pausing is Possible
            {
                if (KeyPress(Keys.Escape)) // When Escape is Pressed
                {
                    // If the State is capable of pausing
                    if (currentState != title)
                    {
                        // Toggle Pause
                        Global.paused = !Global.paused;
                    }

                    // If the State should instead quit game
                    else
                    {
                        // Quit Game
                        Global.quit = true;
                    }
                }
            }

            // Pause Game when Inactive
            if (Global.pauseWhenInactive && !Global.active) Global.paused = true;

            // Update Current State
            currentState.Update(gameTime, this);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Current State
            currentState.OnDraw(spriteBatch);
        }

        // Quit the Game
        public void ExitGame()
        {
            Global.quit = true;
        }
    }
}
