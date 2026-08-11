using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.States.Intro;

namespace Escapeaway.Source.States
{
    internal class IntroState : State
    {
        private Snowman64 snowman64;

        public IntroState()
        {
            snowman64 = new Snowman64();
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            snowman64.Update(gameTime);

            if (snowman64.gone) SwitchState(main.title);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            snowman64.Draw(spriteBatch);
        }
    }
}
