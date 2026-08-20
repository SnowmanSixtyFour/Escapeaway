using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Objects.Level.Projectiles;

namespace Escapeaway.Source.Objects.Level.Boss
{
    internal class DevilBoss : Boss
    {
        // Variables
        private Random random;

        // Sprite
        private Character devil;
        private Point
            size = new Point(80, 80),
            sheetSize = new Point(80, 80);

        // Health Bar
        private BossHealthBar healthBar;

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
        private List<Bullet> animationBullets = new List<Bullet>();
        private List<Bullet> bullets = new List<Bullet>();

        private List<bool> hurtBullets = new List<bool>(); // Keep track of which bullets hurt

        // Random hurt variables for projectile
        private int randomHurt;
        private bool shouldHurt;

        // Timers
        private float
            animateTimer = 0f, createNewAnimation = 250f,
            bulletTimer = 0f, createNewBullet = 1000f,
            waitTimer = 0f, waitMax = 2200f;
        private bool
            startAttack = false, // Start Attack Process
            animateBullets = false, // Animate Bullets being Created
            createBullets = false; // Create Dodgable Bullets

        private int
            currentBullet = 0, // Counter for Interactive Bullets

            // Max Bullet Counts (Start from 1, not 0)
            maxAnimationBullets = 3, // Max Bullets in Animation
            maxBullets = 3; // Max Bullets Before Erasing First Created

        public DevilBoss(int health) : base(health)
        {
            // Initialize Variables
            random = new Random();

            // Set Boss
            devil = new Character(null, new Point(Global.resWidth, fightPosition.Y), size, sheetSize, Color.White);

            // Set Objects
            healthBar = new BossHealthBar("The Devil");
        }

        public override void Reset()
        {
            // Reset Health
            base.Reset();

            // Reset Properties
            devil.SetLocation(new Point(Global.resWidth, fightPosition.Y));
            movedOnscreen = false;

            bullets.Clear();
            animationBullets.Clear();

            startAttack = false;
            animateBullets = false;
            createBullets = false;
        }

        public void Update(GameTime gameTime, Player player)
        {
            // Update Objects

            devil.Update(gameTime);

            healthBar.Update(gameTime, this);

            foreach (var bullet in bullets) bullet.Update(gameTime);
            foreach (var animationBullet in animationBullets) animationBullet.Update(gameTime);

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
                // Start Attack
                if (animationBullets.Count <= 0) startAttack = true;

                // Projectiles
                if (startAttack)
                {
                    // Begin Animation
                    animateBullets = true;

                    // Set Flag to False
                    startAttack = false;
                }
            }

            // Projectiles

            // Animation Bullets
            if (animateBullets)
            {
                // Create New Bullets
                animateTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (animateTimer > createNewAnimation)
                {
                    // Randomize if Bullet should Hurt
                    randomHurt = random.Next(0, 2);
                    if (randomHurt == 0) shouldHurt = false;
                    else shouldHurt = true;

                    hurtBullets.Add(shouldHurt);

                    // Add New Bullet to List
                    animationBullets.Add(new Bullet(new Point(devil.X + 30, fightPosition.Y), shouldHurt, true));
                    currentBullet++;

                    SFX.projectile.Play();

                    // Reset Timer
                    animateTimer = 0f;
                }

                // When Max Bullets in Animation Reached
                if (animationBullets.Count > maxAnimationBullets)
                {
                    // End Animation
                    animateBullets = false;
                    currentBullet = 0;

                    // Start Creating Interactive Bullets
                    createBullets = true;
                }
            }

            // Interactive Bullets
            if (createBullets)
            {
                bulletTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (bulletTimer > createNewBullet)
                {
                    // Set if Bullet should Hurt from Animation
                    bool shouldHurt = hurtBullets[currentBullet];

                    // Create New Bullet
                    bullets.Add(new Bullet(new Point(Global.resWidth, 140), shouldHurt, false));
                    currentBullet++;

                    // Remove Bullet Once Offscreen
                    int index = 0;
                    foreach (var bullet in bullets)
                    {
                        index++;
                        if (bullet.X < -bullet.Width) bullets.RemoveAt(index);
                    }

                    // Stop Creating Bullets at Max Amount
                    if (bullets.Count > maxBullets)
                    {
                        createBullets = false;

                        currentBullet = 0;
                    }

                    // Reset Timer
                    bulletTimer = 0f;
                }
            }

            // Restart Attack
            if (!animateBullets && !createBullets && animationBullets.Count > 0)
            {
                waitTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (waitTimer > waitMax)
                {
                    // Reset Bullets
                    bullets.Clear();
                    animationBullets.Clear();

                    // Reset Hurt Pattern
                    if (hurtBullets.Count > 0) hurtBullets.Clear();

                    // Restart Attack
                    startAttack = true;

                    // Reset Timer
                    waitTimer = 0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Character
            devil.Draw(spriteBatch);

            // Draw Projectiles
            foreach (var bullet in bullets) bullet.Draw(spriteBatch);
            foreach (var animationBullet in animationBullets) animationBullet.Draw(spriteBatch);

            // Draw Health Bar
            if (movedOnscreen) healthBar.Draw(spriteBatch);
        }
    }
}
