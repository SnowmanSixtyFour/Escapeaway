using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Boss
{
    internal class FollowerBoss : Boss
    {
        private Character follower;
        private int currentScreen = 0;

        private Point
            startingPosition = new Point(0, 0),

            size = new Point(80, 64),
            sheetSize = new Point(480, 64);

        private int pixelsToMove = 2;

        public FollowerBoss() : base()
        {
            follower = new Character(Global.devil, startingPosition, sheetSize, size, Color.White);

            follower.CreateAnimation("default", 0, 0);
            follower.CreateAnimation("hurt", 1, 1);
            follower.CreateAnimation("staring", 2, 2);
            follower.CreateAnimation("shocked", 3, 3);
            follower.CreateAnimation("attack", 4, 5);
        }

        /// <summary>
        /// Resets the follower back to its state when endless mode first begins.
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            // Reset Position
            follower.SetLocation(startingPosition);
        }

        /// <summary>
        /// Moves the follower back a room. Useful for when the player reaches the end of a room.
        /// </summary>
        public void MovePositionBack()
        {
            // If Not Already Behind a Room
            if (follower.X >= -size.X - 120)
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
            if (currentScreen != 0) follower.Draw(spriteBatch);
        }
    }
}
