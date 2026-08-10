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
using Escapeaway.Source.States.Level;
using Escapeaway.Source.States.Level.Particles;

namespace Escapeaway.Source.States
{
    internal class LevelState : State
    {
        Player player;
        PauseOverlay pauseOverlay;
        HUD hud;

        public int
            currentScreen = 0,
            
            randomScreenColor = 0;
        private Random random;
        private Color screenColor = CustomColor.Red;

        private List<HeatParticle> heatParticles = new List<HeatParticle>();
        private float
            heatParticleTimer = 0f,
            timeBeforeNewHeatParticle = 360f;

        public LevelState()
        {
            // Set Variables
            random = new Random();

            // Initialize Level
            player = new Player(null, new Point(0, 120), Color.White);

            // Visuals
            hud = new HUD();
            pauseOverlay = new PauseOverlay();
        }

        private void GenerateHeat()
        {
            heatParticles.Add(new HeatParticle(new Point(random.Next(0, (Global.resWidth - 8)), Global.resHeight)));
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // While Unpaused
            if (!Global.paused)
            {
                // Update Objects
                player.Update(gameTime);

                // Update Particles
                foreach (HeatParticle heatParticle in heatParticles) heatParticle.Update(gameTime);

                // Timer Events
                heatParticleTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (heatParticleTimer > timeBeforeNewHeatParticle)
                {
                    // Remove First Heat Particle
                    if (heatParticles.Count > 0) heatParticles.RemoveAt(0);

                    // Create New Heat Particle
                    GenerateHeat();

                    // Reset Timer
                    heatParticleTimer = 0f;
                }

                // Reset Room
                if (player.reachedEnd)
                {
                    // Update Current Sceen Count
                    currentScreen++;

                    // Randomize Screen Colour
                    randomScreenColor = random.Next(0, 3);
                    if (randomScreenColor == 0) screenColor = CustomColor.Red;
                    else if (randomScreenColor == 1) screenColor = CustomColor.DarkRed;
                    else if (randomScreenColor == 2) screenColor = CustomColor.Brown;

                    // Set Flag to False
                    player.reachedEnd = false;
                }
            }

            // While Paused
            hud.Update(gameTime, player, currentScreen);

            if (Global.paused)
            {
                // Quit to Title
                if (KeyPress(Keys.X)) SwitchState(main.title);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Background
            graphicsDevice.Clear(screenColor);

            // Objects
            player.Draw(spriteBatch);

            // Particles
            foreach (HeatParticle heatParticle in heatParticles) heatParticle.Draw(spriteBatch);

            // HUD
            hud.Draw(spriteBatch);
            pauseOverlay.Draw(spriteBatch);
        }
    }
}
