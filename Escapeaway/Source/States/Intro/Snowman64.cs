using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;
using Escapeaway.Source.Objects;

namespace Escapeaway.Source.States.Intro
{
    internal class Snowman64
    {
        private Character logo;
        private Point size = new Point(81, 10);

        private float
            timeExisted = 0f,
            timeToExist = 1800f;
        public bool gone = false;

        public Snowman64()
        {
            logo = new Character(
                Global.snowman64,
                new Point((Global.resWidth / 2) - (size.X / 2), (Global.resHeight / 2) - (size.Y / 2)),
                new Point(648, 10),
                new Point(size.X, size.Y),
                Color.White);
            logo.CreateAnimation("default", 0, 9);
        }
        
        public void Update(GameTime gameTime)
        {
            // Set Animation
            logo.PlayAnimation("default");
            logo.animSpeed = 180;

            // Update Timer
            timeExisted += gameTime.ElapsedGameTime.Milliseconds;

            // When Timer is Up
            if (timeExisted > timeToExist) gone = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            logo.Draw(spriteBatch);
        }
    }
}
