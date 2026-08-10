using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Escapeaway.Source.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.States.Level.Particles
{
    internal class HeatParticle
    {
        private StaticSprite heat;
        private Point size = new Point(8, 8);

        private int pixelsToMoveUp = 1;

        private float
            timeExisted = 0f,
            timeToExist = 1200f;
        private bool draw = true;

        public HeatParticle(Point location)
        {
            heat = new StaticSprite(null, new Rectangle(location, this.size), CustomColor.LightOrange);
        }

        public void Update(GameTime gameTime)
        {
            heat.SetDestRect(new Rectangle(heat.GetDestRect().X, heat.GetDestRect().Y - pixelsToMoveUp, size.X, size.Y));

            timeExisted += gameTime.ElapsedGameTime.Milliseconds;

            if (timeExisted > timeToExist) draw = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (draw) heat.Draw(spriteBatch);
        }
    }
}
