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
        // Sprite
        private Character devil;
        private Point
            size = new Point(80, 80),
            sheetSize = new Point(80, 80);

        // Properties
        private Point
            fightPosition = new Point(Global.resWidth - 100, 30);
        private bool
            movedOnscreen = false;

        public Devil()
        {
            devil = new Character(null, new Point(Global.resWidth, fightPosition.Y), size, sheetSize, Color.White);
        }

        public void Update(GameTime gameTime, Player player)
        {
            devil.Update(gameTime);

            // If Player is in Center of Screen
            if (player.centered)
            {
                // Intro to Boss Fight
                if (!movedOnscreen)
                {
                    if (devil.X > fightPosition.X)
                    {
                        devil.X--;
                    }
                    else
                    {
                        movedOnscreen = true;

                        devil.X = fightPosition.X;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            devil.Draw(spriteBatch);
        }
    }
}
