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
    internal class OptionsState : State
    {
        public OptionsState()
        {
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);
        }
    }
}
