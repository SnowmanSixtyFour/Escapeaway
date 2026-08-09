using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Escapeaway;
using Escapeaway.Source;
using Escapeaway.Source.Objects;
using Escapeaway.Source.States;

namespace Escapeaway.Source.States
{
    internal class State
    {
        public GraphicsDeviceManager graphics = MainGame.publicGraphics;
        public GraphicsDevice graphicsDevice = MainGame.publicGraphicsDevice;

        // State variables
        public Main main;
        public KeyboardState keyboard, previousKeyboard;

        public int screenWidth, screenHeight;
        public Camera cam;

        // Pausing
        public bool canPause = true;

        public State()
        {
            // Audio
            MediaPlayer.IsRepeating = musicLoop;

            // Set Camera
            cam = new Camera(this.graphicsDevice, Global.resWidth, Global.resHeight);
        }

        // Audio
        public bool musicLoop = true;

        public void Update(GameTime gameTime, Main main)
        {
            // Update state variables
            this.main = main;
            screenWidth = graphicsDevice.PresentationParameters.Bounds.Width;
            screenHeight = graphicsDevice.PresentationParameters.Bounds.Height;

            // Set Controls
            keyboard = Keyboard.GetState();

            // Override Update
            OnUpdate(gameTime, main);

            // Update Controls
            previousKeyboard = keyboard;
        }

        public virtual void OnUpdate(GameTime gameTime, Main main)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Run OnDraw Method
            OnDraw(spriteBatch);
        }

        /// <summary>
        /// The method to override when drawing in the state.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch used in Draw.</param>
        public virtual void OnDraw(SpriteBatch spriteBatch)
        {
        }

        public virtual void OnReset()
        {
        }

        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        // State Management

        /// <summary>
        /// Switches the current state to another.
        /// </summary>
        /// <param name="newState">The new state to switch to.</param>
        public void SwitchState(State newState)
        {
            // Reset Current State
            OnReset();

            // Switch to New State
            main.currentState = newState;
        }

        /// <summary>
        /// Switches the current state to another.
        /// </summary>
        /// <param name="newState">The new state to switch to.</param>
        /// <param name="entry">The entry condition for the new state.</param>
        public void SwitchState(State newState, bool entry)
        {
            // Switch to New State IF Condition is Met
            if (entry) SwitchState(newState);
        }

        // Controls

        public bool KeyPress(Keys key)
        {
            if (keyboard.IsKeyUp(key) && previousKeyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }
    }
}
