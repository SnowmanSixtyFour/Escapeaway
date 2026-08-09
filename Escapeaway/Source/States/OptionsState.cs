using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.States
{
    internal class OptionsState : State
    {
        private Text
            header,
            goBack;

        public OptionsState()
        {
            header = new Text(Global.defaultFont, "Options", new Vector2((Global.resWidth / 2) - 26, 6), Color.White, 1.0f, false);
            goBack = new Text(Global.defaultFont, "Press [X] to Exit", new Vector2((Global.resWidth / 2) - 120, 204), Color.White, 1.0f, false);
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // Switch to Title
            if (KeyPress(Keys.X) || KeyPress(Keys.Escape)) SwitchState(main.title);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            header.Draw(spriteBatch);
            goBack.Draw(spriteBatch);
        }
    }
}
