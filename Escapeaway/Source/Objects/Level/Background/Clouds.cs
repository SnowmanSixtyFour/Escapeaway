using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.Objects.Level.Background
{
    internal class Clouds
    {
        private StaticSprite
            clouds, clouds2,
            heatBG;

        private int
            heatBGHeight = 22;

        public Clouds()
        {
            clouds = new StaticSprite(Global.clouds, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.White * 0.15f, true);

            clouds2 = new StaticSprite(Global.clouds, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.White * 0.25f, true);
            clouds2.xOffset += 14f;
            clouds2.yOffset += 20f;

            heatBG = new StaticSprite(Global.heatBG, new Rectangle(0, Global.resHeight - heatBGHeight, Global.resWidth, heatBGHeight), CustomColor.LightOrange, true);
        }

        public void Update(GameTime gameTime)
        {
            clouds.xOffset += 4.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            clouds.yOffset += -1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            clouds2.xOffset += 12.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            clouds2.yOffset += -5.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            heatBG.xOffset += 2.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            clouds2.Draw(spriteBatch);
            clouds.Draw(spriteBatch);

            heatBG.Draw(spriteBatch);
        }
    }
}
