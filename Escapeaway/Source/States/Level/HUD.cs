using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.States.Level
{
    internal class HUD
    {
        private Text lives, room;
        private int y = 6;

        public HUD()
        {
            lives = new Text(Global.defaultFont, "", new Vector2(8, y), Color.White, 1.0f, false);
            room = new Text(Global.defaultFont, "", new Vector2(Global.resWidth - 64, y), Color.White, 1.0f, false);
        }

        public void Update(GameTime gameTime, Player player, int currentScreen)
        {
            lives.setText("LIVES " + player.lives);
            room.setText("ROOM " + currentScreen);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            lives.Draw(spriteBatch);
            room.Draw(spriteBatch);
        }
    }
}
