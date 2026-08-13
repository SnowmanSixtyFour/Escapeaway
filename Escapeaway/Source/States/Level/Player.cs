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
            lives = 0,
            score = 0;
        private Point startingPosition;

        // Screen
        public bool
            moving = false,
            gameOver = false;
        private bool
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
            jumping = false, aboveGround = false,
            sliding = false;

        // Timers
        private float
            // Visuals
            flickerTimer = 0f,
            framesUntilFlicker = 90f,

            // Events
            scorePenalizeTimer = 0f, startRunningTimer = 0f,
            framesUntilScorePenalized = 260f, timeUntilMovingAgain = 810f,

            // SFX
            footstepSfxTimer = 0f, slowDownSfxTimer,
            framesToPlayFootstepSfx = 180f, framesToPlaySlowDownSfx = 40f;
        private bool
            flickering = false,
            shouldFlicker = false, countdownRun = false;

        // Particles
        private List<DustParticle> slowParticles = new List<DustParticle>();
        private int dustParticleLimit = 4;

        private List<DustParticle> jumpParticles = new List<DustParticle>();

        public Player(Texture2D spriteSheet, Point location, Color color, int startingLives) : base(spriteSheet, location, size, sheetSize, color)
        {
            this.startingPosition = location;
            this.lives = startingLives;
        }

        public void SetRoom(RoomLayout newRoom)
        {
            this.room = newRoom;
        }

        private void NewSlowDownParticle()
        {
            // Delete Previous Particles
            if (slowParticles.Count > dustParticleLimit) slowParticles.RemoveAt(0);

            // Create New Particle
            slowParticles.Add(new DustParticle(this));
        }

        private void CreateJumpParticles()
        {
            jumpParticles.Add(new DustParticle(this, true));
            jumpParticles.Add(new DustParticle(this, false));
        }

        /// <summary>
        /// Resets the player to its starting state. Good for level resets.
        /// </summary>
        public void Reset()
        {
            ClearParticles();

            // Stop Moving (wait for player input)
            moving = false;

            // Reset Position
            this.X = startingPosition.X;
            this.Y = startingPosition.Y;

            // Reset Values
            cantSlide = true;

            this.Width = size.X;
            this.Height = size.Y;

            countdownRun = false;
            flickering = false;
            shouldFlicker = false;
        }

        private void LostLife()
        {
            // Reset Player
            Reset();

            // Begin Flicker and Auto Run Events
            shouldFlicker = true;
            countdownRun = true;

            if (lives > 0)
            {
                // If score is above 1, cut it in half after death
                if (score > 1) score /= 2;
                // If score is THAT low, set to 0
                else score = 0;
            }
            
            // Take a life
            if (lives > 0) lives--;
            // Game Over
            else
            {
                gameOver = true;
            }
        }

        /// <summary>
        ///  Start moving the player. Useful for respawning or starting the level.
        /// </summary>
        private void StartMoving()
        {
            moving = true;
            cantSlide = false;
        }

        private void ClearParticles()
        {
            slowParticles.Clear();
            jumpParticles.Clear();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Prevent an illegal score
            if (score < 0) score = 0;

            // Prevent a huge gigantic score (from making the HUD look bad!)
            if (score > Global.maxScore) score = Global.maxScore;

            // Intro
            if (!moving)
            {
                // Hide Particles
                ClearParticles();

                // Start Movement
                if (!countdownRun)
                {
                    if (KeyPress(Keys.Right)
                        || ButtonPress(Buttons.LeftThumbstickRight) || ButtonPress(Buttons.DPadRight))
                    {
                        StartMoving();
                    }
                }
            }

            // Flicker Effect after Respawning
            if (shouldFlicker)
            {
                flickerTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (flickerTimer > framesUntilFlicker)
                {
                    flickering = !flickering;

                    flickerTimer = 0f;
                }
            }

            // Timer until Running Again
            if (countdownRun)
            {
                startRunningTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (startRunningTimer > timeUntilMovingAgain)
                {
                    // Stop Timer Flag
                    countdownRun = false;

                    // Stop Flickering
                    shouldFlicker = false;
                    flickering = false; // Failsafe if the flicker effect somehow leaves the player invisible

                    // Start Moving Again
                    StartMoving();

                    // Reset Timer
                    startRunningTimer = 0f;
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
                if (!sliding && !jumping)
                {
                    if (KeyDown(Keys.Z)
                        || ButtonDown(Buttons.B) || ButtonDown(Buttons.A))
                    {
                        yVelocity = -jumpIncrement;

                        jumping = true;
                        aboveGround = true;

                        SFX.jump.Play();
                    }
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
                            // Set Y Position
                            this.Y = sprite.GetDestRect().Y - this.Height;
                            
                            // Create Jump Particles
                            if (jumping && aboveGround)
                            {
                                CreateJumpParticles();
                                aboveGround = false;
                            }
                            
                            // Set Y Velocity
                            yVelocity = 0;

                            // Stop Jump
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

                // Penalize Score while Blocked
                else
                {
                    scorePenalizeTimer += gameTime.ElapsedGameTime.Milliseconds;

                    if (scorePenalizeTimer > framesUntilScorePenalized)
                    {
                        if (score > 0) score--;

                        scorePenalizeTimer = 0f;
                    }
                }

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
                if (KeyDown(Keys.Left)
                    || ButtonDown(Buttons.LeftThumbstickLeft) || ButtonDown(Buttons.DPadLeft))
                {
                    if (slowDownSfxTimer > framesToPlaySlowDownSfx)
                    {
                        if (!jumping && !blocked)
                        {
                            // Play SFX
                            SFX.skid.Play();

                            // Create new Dust
                            NewSlowDownParticle();
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
                    if (!sliding && !jumping)
                    {
                        if (KeyDown(Keys.Down)
                        || ButtonDown(Buttons.DPadDown) || ButtonDown(Buttons.LeftThumbstickDown))
                        {
                            SFX.slide.Play();
                        }
                    }

                    if (!jumping)
                    {
                        if (KeyDown(Keys.Down)
                        || ButtonDown(Buttons.DPadDown) || ButtonDown(Buttons.LeftThumbstickDown))
                        {

                            {
                                // Set Sliding Timer
                                if (!sliding) slideCounter = slideStart;

                                // Start Sliding
                                sliding = true;

                                if (this.Width != slidingSize.X && this.Height != slidingSize.Y)
                                {
                                    // Update Size
                                    this.Width = slidingSize.X;
                                    this.Height = slidingSize.Y;
                                    this.Y += slidingSize.Y;
                                }
                            }
                        }
                    }
                }
                else slideCounter = 0;

                if (sliding)
                {
                    // Sliding Timer
                    if (slideCounter > 1) slideCounter--;
                    else
                    {
                        if (!KeyDown(Keys.Down)
                            && !ButtonDown(Buttons.DPadDown) && !ButtonDown(Buttons.LeftThumbstickDown))
                        {
                            slideCounter = 0;
                        }
                    }

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
                    ClearParticles();
                }

                // Update Particles
                foreach (var slowDust in slowParticles) slowDust.Update(gameTime);
                foreach (var jumpDust in jumpParticles) jumpDust.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw Character
            if (!flickering) base.Draw(spriteBatch);

            // Draw Particles
            foreach (var slowDust in slowParticles) slowDust.Draw(spriteBatch);
            foreach (var jumpDust in jumpParticles) jumpDust.Draw(spriteBatch);
        }
    }
}
