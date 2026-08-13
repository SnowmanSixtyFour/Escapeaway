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
    internal class EndlessFollower
    {
        private Character follower;
        private int currentScreen = 0;

        private Point
            startingPosition = new Point(0, 0),

            size = new Point(40, 40),
            sheetSize = new Point(40, 40);

        private int pixelsToMove = 2;

        public EndlessFollower()
        {
            follower = new Character(null, startingPosition, sheetSize, size, Color.White);
        }

        /// <summary>
        /// Resets the follower back to its state when endless mode first begins.
        /// </summary>
        public void Reset()
        {
            follower.X = startingPosition.X;
            follower.Y = startingPosition.Y;
        }

        /// <summary>
        /// Moves the follower back a room. Useful for when the player reaches the end of a room.
        /// </summary>
        public void MovePositionBack()
        {
            // If Not Already Behind a Room
            if (follower.X >= -this.size.X - 120)
            {
                // Move Follower Behind a Room
                follower.X -= Global.resWidth;
            }
        }

        public void Update(GameTime gameTime, Player player, int currentScreen)
        {
            // Set Screen Variable
            this.currentScreen = currentScreen;

            // Move Follower
            if (this.currentScreen != 0)
            {
                follower.Update(gameTime);

                // Move Towards Player

                if (player.X > follower.X) follower.X += pixelsToMove;
                else if (player.X < follower.X) follower.X -= pixelsToMove;

                if (player.Y > follower.Y) follower.Y += pixelsToMove;
                else if (player.Y < follower.Y) follower.Y -= pixelsToMove;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Follower
            if (this.currentScreen != 0) follower.Draw(spriteBatch);
        }
    }
}
