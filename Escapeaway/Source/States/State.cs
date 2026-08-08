using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway;
using Escapeaway.Source;
using Escapeaway.Source.Objects;

namespace Escapeaway.Source.States
{
    internal class State
    {
        public GraphicsDeviceManager graphics;
        public GraphicsDevice graphicsDevice;

        // State variables
        public KeyboardState keyboard, previousKeyboard;

        public int screenWidth, screenHeight;
        public Camera cam;

        // Pausing
        public bool canPause = true;

        public State()
        {
            // Set Graphics
            this.graphics = MainGame.publicGraphics;
            this.graphicsDevice = MainGame.publicGraphicsDevice;

            // Set Camera
            cam = new Camera(this.graphicsDevice, Global.resWidth, Global.resHeight);
        }

        public void Update(GameTime gameTime)
        {
            // Update state variables
            screenWidth = graphicsDevice.PresentationParameters.Bounds.Width;
            screenHeight = graphicsDevice.PresentationParameters.Bounds.Height;

            // Set Controls
            keyboard = Keyboard.GetState();

            // Override Update
            OnUpdate(gameTime);

            // Update Controls
            previousKeyboard = keyboard;
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            OnDraw(spriteBatch);
        }

        public virtual void OnDraw(SpriteBatch spriteBatch)
        {
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
