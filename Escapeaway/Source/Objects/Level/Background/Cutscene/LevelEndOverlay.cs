using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.Objects.Level.Background.Cutscene
{
    internal class LevelEndOverlay
    {
        private StaticSprite sprite;

        public bool move = false;
        private int pixelsToMove = 2;

        private int centered;

        public LevelEndOverlay()
        {
            // Set Sprite
            sprite = new StaticSprite(Global.endOverlay, new Rectangle(Global.resWidth, 0, 280, 224), CustomColor.White);

            // Set Destination
            centered = (sprite.GetWidth() - Global.resWidth);
        }

        public void Update(GameTime gameTime)
        {
            // Move Sprite
            if (move)
            {
                // If Sprite is Not Centered (Middle of Screen)
                if (sprite.GetX() >= centered)
                {
                    // Move Sprite to Center
                    sprite.SetX(sprite.GetX() - pixelsToMove);
                }
                else
                {
                    // Center Sprite
                    sprite.SetX(centered);

                    // End Move Event
                    move = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Sprite
            sprite.Draw(spriteBatch);
        }
    }
}
