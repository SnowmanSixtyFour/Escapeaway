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
        private Text lives, score, room;
        private int y = 6;

        public HUD()
        {
            lives = new Text(Global.defaultFont, "", new Vector2(8, y), CustomColor.White, 1.0f, false);
            score = new Text(Global.defaultFont, "", new Vector2(78, y), CustomColor.White, 1.0f, false);
            room = new Text(Global.defaultFont, "", new Vector2(Global.resWidth - 64, y), CustomColor.White, 1.0f, false);
        }

        public void Update(GameTime gameTime, Player player, int currentScreen, Main main)
        {
            lives.setText("LIVES " + player.lives);
            if (player.score <= Global.maxScore) score.setText("SCORE " + player.score);
            if (player.score < Global.maxScore) score.setColor(CustomColor.White);
            else score.setColor(CustomColor.Yellow);
            if (!main.endless) room.setText("ROOM " + (currentScreen + 1));
            else room.setText("ENDLESS");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            lives.Draw(spriteBatch);
            score.Draw(spriteBatch);
            room.Draw(spriteBatch);
        }
    }
}
