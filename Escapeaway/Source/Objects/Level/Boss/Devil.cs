using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Objects.Level.Projectiles;

namespace Escapeaway.Source.Objects.Level.Boss
{
    internal class Devil
    {
        // Sprite
        private Character devil;
        private Point
            size = new Point(80, 80),
            sheetSize = new Point(80, 80);

        // Movement
        private bool movingUp = false;
        private float
            yVelocity = 0,
            gravity = 0.2f, maxMovementSpeed = 1.5f,

            maxUpHeight = 30, maxDownHeight = 34;

        // Properties
        private Point
            fightPosition = new Point(Global.resWidth - 100, 32);
        private bool
            movedOnscreen = false;

        // Projectiles
        private List<Bullet> bullets = new List<Bullet>();

        private float bulletTimer = 0f, createNewBullet = 1000f; // Timer
        private bool
            createBullets = false; // Create Bullets

        public Devil()
        {
            // Set Character
            devil = new Character(null, new Point(Global.resWidth, fightPosition.Y), size, sheetSize, Color.White);
        }

        public void Update(GameTime gameTime, Player player)
        {
            // Update Devil
            devil.Update(gameTime);
            foreach (var bullet in bullets) bullet.Update(gameTime);

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

            // If Player is in Center of Screen
            
            if (player.centered)
            {
                // Intro to Boss Fight
                if (!movedOnscreen)
                {
                    if (devil.X > fightPosition.X)
                    {
                        devil.X--;
                    }
                    else
                    {
                        movedOnscreen = true;

                        devil.X = fightPosition.X;
                    }
                }
            }

            // Set Variables after Onscreen
            if (movedOnscreen)
            {
                // Projectiles
                if (!createBullets) createBullets = true;
            }

            // Projectiles

            if (createBullets)
            {
                bulletTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (bulletTimer > createNewBullet)
                {
                    bullets.Add(new Bullet(true));

                    bulletTimer = 0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Character
            devil.Draw(spriteBatch);

            // Draw Projectiles
            foreach (var bullet in bullets) bullet.Draw(spriteBatch);
        }
    }
}
