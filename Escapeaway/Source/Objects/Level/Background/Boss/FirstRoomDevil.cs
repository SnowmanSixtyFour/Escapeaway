using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Background.Boss
{
    internal class FirstRoomDevil
    {
        private Character devil;
        private bool visible = true;
        private Point
            size = new Point(100, 100),
            sheetSize = new Point(100, 100);

        // Movement
        private bool movingUp = false;
        private float
            yVelocity = 0,
            gravity = 0.2f, maxMovementSpeed = 1.5f,

            maxUpHeight = 34, maxDownHeight = 38;

        public FirstRoomDevil()
        {
            devil = new Character(null, new Point(100, 36), sheetSize, size, Color.White);
        }

        public void Hide()
        {
            visible = false;
        }

        public void Show()
        {
            visible = true;
        }

        public void Update(GameTime gameTime)
        {
            devil.Update(gameTime);

            // Flying Movement

            if (devil.Y < maxUpHeight) movingUp = false;
            if (devil.Y > maxDownHeight) movingUp = true;

            devil.Y += Convert.ToInt32(yVelocity);

            if (movingUp)
            {
                if (yVelocity > -maxMovementSpeed) yVelocity -= gravity;
                else yVelocity = -maxMovementSpeed;
            }
            if (!movingUp)
            {
                if (yVelocity < maxMovementSpeed) yVelocity += gravity;
                else yVelocity = maxMovementSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible) devil.Draw(spriteBatch);
        }
    }
}
