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
        private Point
            size = new Point(8, 8),
            smallerSize = new Point(4, 4);
        private int offsetToPlayer = 6; // How far in front of player to spawn

        private int pixelsToMoveBack = 1;

        private float
            timeExisted = 0f,
            timeToExist = 220f; // Frames to exist until deleted
        private bool draw = true;

        public DustParticle(Player player)
        {
            // Set Sprite
            dust = new StaticSprite(null,
                new Rectangle(
                player.X + (player.Width - size.X) + offsetToPlayer,
                player.Y + (player.Height - size.Y),
                size.X,
                size.Y),
                Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // Move Backwards
            dust.SetDestRect(new Rectangle(dust.GetDestRect().X - pixelsToMoveBack, dust.GetDestRect().Y, size.X, size.Y));

            timeExisted += gameTime.ElapsedGameTime.Milliseconds;

            // Turn Small after Half of Time Existed
            if (timeExisted > (timeToExist / 2))
            {
                if (this.size.X > 0) this.size.X--;
                if (this.size.Y > 0) this.size.Y--;
            }

            // After Enough Time, Disappear
            if (timeExisted > timeToExist) draw = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (draw) dust.Draw(spriteBatch);
        }
    }
}
