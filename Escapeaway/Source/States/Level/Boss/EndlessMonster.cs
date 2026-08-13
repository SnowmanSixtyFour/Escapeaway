using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Objects;

namespace Escapeaway.Source.States.Level.Boss
{
    internal class EndlessMonster
    {
        private Character monster;
        private int currentScreen = 0;

        private Point
            startingPosition = new Point(0, 0),

            size = new Point(20, 20),
            sheetSize = new Point(20, 20);

        public EndlessMonster()
        {
            monster = new Character(null, startingPosition, sheetSize, size, Color.White);
        }

        /// <summary>
        /// Resets the monster back to its state when endless mode first begins.
        /// </summary>
        public void Reset()
        {
            monster.X = startingPosition.X;
            monster.Y = startingPosition.Y;
        }

        /// <summary>
        /// Moves the monster back a room. Useful for when the player reaches the end of a room.
        /// </summary>
        public void MovePositionBack()
        {
            // If Not Already Behind a Room
            if (monster.X >= -this.size.X)
            {
                // Move Monster Behind a Room
                monster.X -= Global.resWidth;
            }
        }

        public void Update(GameTime gameTime, Player player, int currentScreen)
        {
            // Set Screen Variable
            this.currentScreen = currentScreen;

            // Move Monster
            if (this.currentScreen != 0)
            {
                monster.Update(gameTime);

                // Move Towards Player
                monster.X++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Monster
            if (this.currentScreen != 0) monster.Draw(spriteBatch);
        }
    }
}
