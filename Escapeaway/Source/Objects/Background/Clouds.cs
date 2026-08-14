using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.Objects.Background
{
    internal class Clouds
    {
        private StaticSprite clouds;

        private float cloudScrollSpeed = 3.0f;

        public Clouds()
        {
            clouds = new StaticSprite(Global.clouds, new Rectangle(0, 32, Global.resWidth, Global.resHeight / 3), (Color.White * 0.5f), true);
        }

        public void Update(GameTime gameTime)
        {
            float cloudsOffset = cloudScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            clouds.offset += cloudsOffset;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            clouds.Draw(spriteBatch);
        }
    }
}
