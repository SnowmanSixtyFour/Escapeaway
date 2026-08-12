using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Escapeaway.Source.Objects;
using Escapeaway.Source.States.Level.Particles;
using Escapeaway.Source.States.Level.Rooms;

namespace Escapeaway.Source.States.Level
{
    internal class Player : Character
    {
        // Game Variables
        public int
            lives = 3,
            score = 0;
        private Point startingPosition;

        // Screen
        private bool
            moving = false,
            blocked = false,
            cantSlide = true;
        public bool reachedEnd = false;

        // Properties
        private RoomLayout room;
        public static Point
            size = new Point(20, 40),
            sheetSize = new Point(20, 40),

            slidingSize = new Point(40, 20);

        private int
            // Default Variables
            runSpeed = 0,
            yVelocity = 0,

            // Running Speeds
            defaultRunSpeed = 3,
            slowRunSpeed = 1,

            // Jumping
            jumpIncrement = 12,
            maxJumpHeight = 65,
            gravity = 1,

            // Sliding
            slideCounter = 0,
            slideStart = 60;

        // Conditions
        private bool
            slowingDown = false,
            jumping = false,
            sliding = false;

        // Timers
        private float
            // SFX
            footstepSfxTimer = 0f, slowDownSfxTimer,

            // Frames until SFX should Play
            framesToPlayFootstepSfx = 180f, framesToPlaySlowDownSfx = 40f;

        // Particles
        private List<DustParticle> dustParticles = new List<DustParticle>();
        private int dustParticleLimit = 4;

        public Player(Texture2D spriteSheet, Point location, Color color) : base(spriteSheet, location, size, sheetSize, color)
        {
            this.startingPosition = location;
        }

        public void SetRoom(RoomLayout newRoom)
        {
            this.room = newRoom;
        }

        private void NewDustParticle()
        {
            // Delete Previous Particles
            if (dustParticles.Count > dustParticleLimit) dustParticles.RemoveAt(0);

            // Create New Particle
            dustParticles.Add(new DustParticle(this));
        }

        /// <summary>
        /// Resets the player to its starting state. Good for level resets.
        /// </summary>
        public void Reset()
        {
            // Stop Moving (wait for player input)
            moving = false;

            // Reset Position
            this.X = 6;
            this.Y = startingPosition.Y;

            // Reset Values
            cantSlide = true;

            this.Width = size.X;
            this.Height = size.Y;
        }

        private void LostLife()
        {
            Reset();

            // If score is above 1, cut it in half after death
            if (score > 1) score /= 2;

            // If score is THAT low, set to 0
            else score = 0;
            
            // Take a life
            if (lives > 0) lives--;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Prevent an illegal score
            if (score < 0) score = 0;

            // Intro
            if (!moving)
            {
                if (KeyPress(Keys.Right))
                {
                    moving = true;
                    cantSlide = false;
                }
            }

            // Movement
            if (moving)
            {
                // Lost a Life
                if (this.Y > Global.resHeight)
                {
                    LostLife();
                }

                // Jump
                if (!sliding && KeyHold(Keys.Z) && !jumping)
                {
                    yVelocity = -jumpIncrement;

                    jumping = true;

                    SFX.jump.Play();
                }

                // Gravity
                if (jumping)
                {
                    if (yVelocity < maxJumpHeight) yVelocity += gravity;
                    else yVelocity = maxJumpHeight;
                }

                // Update Y Position
                this.Y += yVelocity;

                // Room Collision
                foreach (var sprite in room.ground.sprites)
                {
                    if (this.CollidesWith(sprite))
                    {
                        // Place Player on Ground
                        if (this.Y <= sprite.GetDestRect().Y)
                        {
                            // Stop Jump
                            this.Y = sprite.GetDestRect().Y - this.Height;
                            yVelocity = 0;
                            jumping = false;

                            // Don't End Collision Until Player Stops Touching Sprite
                            break;
                        }

                        // Stop Movement if Touching Wall
                        if (this.X >= sprite.GetDestRect().X - this.Width && this.X <= sprite.GetDestRect().X + sprite.GetDestRect().Width) blocked = true;
                        else blocked = false;
                    }
                    // Player is not Touching Ground
                    else
                    {
                        // Apply Gravity
                        this.jumping = true;

                        blocked = false;
                    }
                }

                // Move Right
                if (!blocked) this.X += runSpeed;

                // Footstep SFX
                footstepSfxTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (!jumping)
                {
                    if (footstepSfxTimer > framesToPlayFootstepSfx)
                    {
                        // Play Footstep SFX
                        if (!sliding && !blocked) SFX.footsteps.Play();

                        // Reset Timer
                        footstepSfxTimer = 0f;
                    }
                }

                // Slowing Down SFX
                slowDownSfxTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (KeyHold(Keys.Left))
                {
                    if (slowDownSfxTimer > framesToPlaySlowDownSfx)
                    {
                        if (!jumping && !blocked)
                        {
                            // Play SFX
                            SFX.skid.Play();

                            // Create new Dust
                            NewDustParticle();
                        }

                        slowDownSfxTimer = 0f;
                    }

                    slowingDown = true;
                }
                else
                {
                    slowingDown = false;
                }

                // Slow Down
                if (slowingDown && !jumping) // Only works when not jumping
                {
                    runSpeed = slowRunSpeed;
                }
                else
                {
                    runSpeed = defaultRunSpeed;
                }

                // Slide
                if (!cantSlide)
                {
                    if (!sliding && KeyHold(Keys.Down) && !jumping) SFX.slide.Play();

                    if (!jumping && KeyHold(Keys.Down))
                    {
                        // Start Sliding
                        sliding = true;

                        // Set Sliding Timer
                        slideCounter = slideStart;

                        if (this.Width != slidingSize.X && this.Height != slidingSize.Y)
                        {
                            // Update Size
                            this.Width = slidingSize.X;
                            this.Height = slidingSize.Y;
                            this.Y += slidingSize.Y;
                        }
                    }
                }
                else slideCounter = 0;

                if (sliding)
                {
                    // Sliding Timer
                    slideCounter--;

                    // When Sliding is Finished
                    if (slideCounter <= 0)
                    {
                        // Reset Size
                        this.Width = size.X;
                        this.Height = size.Y;
                        this.Y -= slidingSize.Y;

                        // Stop Sliding
                        sliding = false;
                    }
                }

                // Reaching End of Screen
                if (this.X > Global.resWidth)
                {
                    reachedEnd = true;

                    // Prevent Player From Leaving Screen
                    if (reachedEnd) this.X = 0 - this.Width;

                    // Reset Particles
                    dustParticles.Clear();
                }

                // Update Particles
                foreach (DustParticle dust in dustParticles)
                {
                    dust.Update(gameTime);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw Character
            base.Draw(spriteBatch);

            // Draw Particles
            foreach (DustParticle dust in dustParticles)
            {
                dust.Draw(spriteBatch);
            }
        }
    }
}
