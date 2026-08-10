using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Objects;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.States.Level.PlayerData
{
    internal class DustParticle
    {
        private StaticSprite dust;
        private Point size = new Point(8, 8);

        private int pixelsToMoveBack = 1;

        public DustParticle(Player player)
        {
            // Set Sprite
            dust = new StaticSprite(null,
                new Rectangle(
                player.X + (player.Width - size.X),
                player.Y + (player.Height - size.Y),
                size.X,
                size.Y),
                Color.Gray);
        }

        public void Update(GameTime gameTime)
        {
            // Move Backwards
            dust.SetDestRect(new Rectangle(dust.GetDestRect().X - pixelsToMoveBack, dust.GetDestRect().Y, size.X, size.Y));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            dust.Draw(spriteBatch);
        }
    }
}
