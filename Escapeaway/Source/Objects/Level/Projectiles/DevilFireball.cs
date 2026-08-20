using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Projectiles
{
    internal class DevilFireball
    {
        // Sprite
        private Character bullet;
        private Point
            startLocation = new Point(0, 0),

            size = new Point(12, 12),
            sheetSize = new Point(12, 12);

        // Properties
        public bool hit = false;
        public bool
            hurts = false, // Can Hurt Player
            moving = true, // If Bullet can Move
            parry = false, gone = false; // Parried by Player
        private bool
            movingUp = false; // Move Up (For Created Animation)

        private int pixelsToMove = 2;

        /// <summary>
        /// Creates a projectile for the player to dodge. Made with the room 100 boss in mind.
        /// </summary>
        /// <param name="startLocation">The starting location of the projectile.</param>
        /// <param name="hurts">Whether the projectile should hurt the player on contact.</param>
        /// <param name="movingUp">If the projectile should be moving up or left after creation. (True = Up, False = Left)</param>
        public DevilFireball(Point startLocation, bool hurts = false, bool movingUp = false)
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

            // Movement
            if (moving)
            {
                // Up
                if (movingUp)
                {
                    if (bullet.Y > -size.Y)
                    {
                        bullet.X -= pixelsToMove;
                        bullet.Y -= pixelsToMove;
                    }
                }
                // Left
                else
                {
                    if (bullet.X > -size.X) bullet.X -= pixelsToMove;
                }
            }

            // Parry
            if (parry)
            {
                bullet.X += (pixelsToMove * 2);
                bullet.Y -= pixelsToMove;
            }

            // Disappear
            if (gone)
            {
                parry = false;
                moving = false;

                bullet.X = Global.resWidth;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bullet.Draw(spriteBatch);
        }

        public Character sprite => this.bullet;
    }
}
