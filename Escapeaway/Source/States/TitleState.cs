using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.States
{
    internal class TitleState : State
    {
        private StaticSprite logo;
        private int
            logoWidth = 154,
            logoHeight = 27;

        private Text copyright;

        public TitleState()
        {
            logo = new StaticSprite(Global.logo, new Rectangle((Global.resWidth / 2) - (logoWidth / 2), 40, logoWidth, logoHeight), Color.White);

            // Yes, I made the copyright symbol a hashtag. Deal with it
            copyright = new Text(Global.defaultFont, "#Snowman64 2026", new Vector2(2, 212), Color.White, 1.0f, false);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            logo.Draw(spriteBatch);
            copyright.Draw(spriteBatch);
        }
    }
}
