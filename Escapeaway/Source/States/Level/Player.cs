using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Objects;
using Microsoft.Xna.Framework.Input;

namespace Escapeaway.Source.States.Level
{
    internal class Player : Character
    {
        // Game Variables
        public int
            lives = 3,
            score = 0;

        // Screen
        private bool
            moving = false;
        public bool reachedEnd = false;

        // Properties
        private int ground = 120;
        public static Point
            size = new Point(20, 40),
            sheetSize = new Point(20, 40),

            slidingSize = new Point(40, 20);

        private int
            // Default Variables
            runSpeed = 0,
            yVelocity = 0,
            currentGravity = 0,

            // Running Speeds
            defaultRunSpeed = 3,
            slowRunSpeed = 2,

            // Jumping
            jumpIncrement = 12,
            maxJumpHeight = 65,
            gravity = 1,

            // Sliding
            slideCounter = 0,
            slideStart = 30;

        // Conditions
        private bool
            slowingDown = false,
            jumping = false,
            sliding = false;

        public Player(Texture2D spriteSheet, Point location, Color color) : base(spriteSheet, location, size, sheetSize, color)
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Intro
            if (!moving)
            {
                if (KeyPress(Keys.Right)) moving = true;
            }

            // Movement
            if (moving)
            {
                this.X += runSpeed;

                if (KeyHold(Keys.Left))
                {
                    slowingDown = true;
                }
                else
                {
                    slowingDown = false;
                }

                // Jump
                if (!sliding && KeyHold(Keys.Z) && !jumping)
                {
                    yVelocity = -jumpIncrement;

                    jumping = true;
                }

                // Gravity
                if (jumping)
                {
                    if (yVelocity < maxJumpHeight) yVelocity += gravity;
                    else yVelocity = maxJumpHeight;
                }

                // Update Y Position
                this.Y += yVelocity;

                // When Touching Ground
                if (this.Y >= ground && !sliding)
                {
                    this.Y = ground;

                    yVelocity = 0;

                    jumping = false;
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
                }
            }
        }
    }
}
