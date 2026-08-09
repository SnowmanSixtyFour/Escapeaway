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
        // Intro
        private bool moving = false;

        // Properties
        private int ground = 120;

        private int
            // Default Variables
            runSpeed = 0,
            yVelocity = 0,
            currentGravity = 0,

            // Running Speeds
            defaultRunSpeed = 3,
            slowRunSpeed = 2,

            // Jumping
            jumpIncrement = 16,
            maxJumpHeight = 80,
            gravity = 1;
        private bool
            slowingDown = false,
            jumping = false;

        public Player(Texture2D spriteSheet, Point location, Point size, Point sheetSize, Color color) : base(spriteSheet, location, size, sheetSize, color)
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

                if (KeyDown(Keys.Left))
                {
                    slowingDown = true;
                }
                else
                {
                    slowingDown = false;
                }

                // Jump
                if (KeyDown(Keys.Z) && !jumping)
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
                if (this.Y >= ground)
                {
                    this.Y = ground;

                    yVelocity = 0;

                    jumping = false;
                }

                // Slow Down
                if (slowingDown && !jumping)
                {
                    runSpeed = slowRunSpeed;
                }
                else
                {
                    runSpeed = defaultRunSpeed;
                }

                // Prevent Player From Leaving Screen
                if (this.X > Global.resWidth) this.X = 0 - this.Width;
            }
        }
    }
}
