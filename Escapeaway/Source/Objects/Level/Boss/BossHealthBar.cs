using Escapeaway.Source.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapeaway.Source.Objects.Level.Boss
{
    internal class BossHealthBar
    {
        private StaticSprite healthBar, outerOutline;

        public BossHealthBar()
        {
            healthBar = new StaticSprite();
            outerOutline = new StaticSprite();
        }

        public void Update(GameTime gameTime, Devil devil)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            outerOutline.Draw(spriteBatch);
        }
    }
}
