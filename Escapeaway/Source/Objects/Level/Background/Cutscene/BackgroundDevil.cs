using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Background.Cutscene
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

            startingPosition;

        public BackgroundDevil()
        {
            // Set Starting Position
            startingPosition = 0 - size.X;

            // Set Sprite
            devil = new Character(null, new Point(startingPosition, Global.resHeight / 2 - size.Y / 2 - yOffset), sheetSize, size, Color.White);
        }

        public void Reset()
        {
            // Set Position
            devil.X = startingPosition;
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
