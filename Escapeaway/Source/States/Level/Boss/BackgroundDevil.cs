using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Objects;

namespace Escapeaway.Source.States.Level.Boss
{
    internal class BackgroundDevil
    {
        private Character devil;
        private Point
            size = new Point(20, 20),
            sheetSize = new Point(20, 20);
        private int
            yOffset = 25,
            pixelsToMove = 4,

            startingPosition = 0;

        public BackgroundDevil()
        {
            // Set Starting Position
            this.startingPosition = -Global.resWidth;

            // Set Sprite
            devil = new Character(null, new Point(startingPosition, (Global.resHeight / 2) - (this.size.Y / 2) - yOffset), sheetSize, size, Color.White);
        }

        public void Reset()
        {
            // Set Position
            devil.X = -Global.resWidth;
        }

        public void Update(GameTime gameTime)
        {
            devil.Update(gameTime);

            // Move Devil Across Background
            if (devil.X < Global.resWidth) devil.X += pixelsToMove;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            devil.Draw(spriteBatch);
        }
    }
}
