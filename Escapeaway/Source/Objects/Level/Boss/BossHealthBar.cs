using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.Objects.Level.Boss
{
    internal class BossHealthBar
    {
        private StaticSprite outerOutline, healthBar;
        private Text bossName;

        private int newWidth;

        private bool flicker = false;
        private float
            flickerTimer = 0f,
            timeUntilFlicker = 90f;

        /// <summary>
        /// Create a health bar for a boss fight.
        /// </summary>
        /// <param name="name">The name of the boss, displayed above the health bar graphic.</param>
        public BossHealthBar(String name)
        {
            outerOutline = new StaticSprite(null, new Rectangle(26, 197, 205, 11), CustomColor.Black);
            healthBar = new StaticSprite(null, new Rectangle(28, 199, 201, 7), CustomColor.Red);

            bossName = new Text(Global.defaultFont, name, new Vector2(98, 181), CustomColor.White, 1.0f, true);
        }

        public void Update(GameTime gameTime, Boss boss)
        {
            // Calculate Size of Health Bar
            newWidth = (201 * boss.health) / boss.maxHealth;

            // Set Size
            healthBar.SetWidth(newWidth);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            outerOutline.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);

            bossName.Draw(spriteBatch);
        }
    }
}
