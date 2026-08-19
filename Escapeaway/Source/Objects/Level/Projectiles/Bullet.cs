using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Projectiles
{
    internal class Bullet
    {
        // Sprite
        private Character bullet;
        private Point
            startLocation = new Point(0, 0),

            size = new Point(16, 16),
            sheetSize = new Point(16, 16);

        // Properties
        public bool hit = false;
        private bool
            hurts = false, // Can Hurt Player
            movingUp = false; // Move Up (For Created Animation)

        private int pixelsToMove = 2;

        /// <summary>
        /// Creates a projectile for the player to dodge. Made with the room 100 boss in mind.
        /// </summary>
        /// <param name="startLocation">The starting location of the projectile.</param>
        /// <param name="hurts">Whether the projectile should hurt the player on contact.</param>
        /// <param name="movingUp">If the projectile should be moving up or left after creation. (True = Up, False = Left)</param>
        public Bullet(Point startLocation, bool hurts = false, bool movingUp = false)
        {
            // Set Variables
            this.hurts = hurts;
            this.movingUp = movingUp;
            this.startLocation = startLocation;

            // Set Character
            bullet = new Character(null, startLocation, size, sheetSize, Color.Orange);

            // wip
            if (hurts) bullet.SetColor(Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            bullet.Update(gameTime);

            // Move Up
            if (movingUp)
            {
                if (bullet.Y > -size.Y) bullet.Y -= pixelsToMove;
            }
            // Move Left
            else
            {
                if (bullet.X > -size.X) bullet.X -= pixelsToMove;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bullet.Draw(spriteBatch);
        }

        // Getters

        public int X => bullet.X;
        public int Y => bullet.Y;
        public int Width => bullet.Width;
        public int Height => bullet.Height;
    }
}
