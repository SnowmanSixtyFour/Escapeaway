using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Escapeaway.Source.Objects.Level.Projectiles
{
    internal class Bullet
    {
        // Sprite
        private Character bullet;
        private Point
            startLocation = new Point(Global.resWidth, 140),

            size = new Point(16, 16),
            sheetSize = new Point(16, 16);

        // Properties
        public bool hit = false;
        private bool hurts = false;

        private int pixelsToMove = 2;

        public Bullet(bool hurts = false)
        {
            // Set Variables
            this.hurts = hurts;

            // Set Character
            bullet = new Character(null, startLocation, size, sheetSize, Color.White);

            // wip
            if (hurts) bullet.SetColor(Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            bullet.Update(gameTime);

            if (bullet.X > -size.X) bullet.X -= pixelsToMove;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bullet.Draw(spriteBatch);
        }
    }
}
