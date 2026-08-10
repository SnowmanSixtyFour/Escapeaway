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

        // Endless Mode
        public bool endless = false;

        public Main()
        {
            // Initialize States
            title = new TitleState();
            options = new OptionsState();
            story = new StoryState();
            level = new LevelState();

            // Set Current State
            currentState = level;
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // Pausing
            if (canPause) // If Pausing is Possible
            {
                if (KeyPress(Keys.Escape)) // When Escape is Pressed
                {
                    // If the State should quit game
                    if (currentState == title)
                    {
                        // Quit Game
                        Global.quit = true;
                    }
                    // If the State is capable of pausing (gameplay)
                    else if (currentState == level)
                    {
                        // Toggle Pause
                        Global.paused = !Global.paused;
                    }
                }
            }
            
            // Unpause when not in Level
            if (Global.paused && currentState != level) Global.paused = false;

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
    }
}
