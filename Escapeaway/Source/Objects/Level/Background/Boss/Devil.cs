using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Background.Boss
{
    internal class Devil
    {
        private Character devil;

        public Devil()
        {
        }

        public void Update(GameTime gameTime)
        {
            devil.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            devil.Draw(spriteBatch);
        }
    }
}
