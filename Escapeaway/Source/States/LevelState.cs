using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway;
using Escapeaway.Source.Graphics;
using Escapeaway.Source.States.Level;
using Escapeaway.Source.States.Level.Boss;
using Escapeaway.Source.States.Level.Particles;
using Escapeaway.Source.States.Level.Rooms;

namespace Escapeaway.Source.States
{
    internal class LevelState : State
    {
        RoomLayout roomLayout;
        FirstRoomDisclaimer roomDisclaimer;
        
        FirstRoomDevil firstRoomDevil;
        BackgroundDevil backgroundDevil;
        EndlessFollower follower;

        Player player;
        
        PauseOverlay pauseOverlay;
        HUD hud;

        public bool endless;

        private int defaultLives = 3;

        public int
            currentScreen = 0,
            screenWithBackgroundDevil = 49,
            
            randomScreenColor = 0;
        private Random random;
        private Color screenColor = CustomColor.Red;

        private List<HeatParticle> heatParticles = new List<HeatParticle>();
        private float
            heatParticleTimer = 0f,
            timeBeforeNewHeatParticle = 360f;
        int heatParticleLimit = 14;

        private StaticSprite heatBG;
        private int heatBGHeight = 22;

        public LevelState()
        {
            // Set Variables
            random = new Random();

            // Initialize Level
            roomLayout = new RoomLayout();
            roomDisclaimer = new FirstRoomDisclaimer();

            firstRoomDevil = new FirstRoomDevil();
            backgroundDevil = new BackgroundDevil();
            follower = new EndlessFollower();

            player = new Player(null, new Point(16, 120), Color.White, defaultLives);

            // Background
            heatBG = new StaticSprite(null, new Rectangle(0, Global.resHeight - this.heatBGHeight, Global.resWidth, this.heatBGHeight), CustomColor.LightOrange);

            // HUD
            hud = new HUD();
            pauseOverlay = new PauseOverlay();
        }

        /// <summary>
        /// Generate heat particles for the level background.
        /// </summary>
        private void GenerateHeat()
        {
            heatParticles.Add(new HeatParticle(new Point(random.Next(0, (Global.resWidth - 8)), Global.resHeight)));
        }

        /// <summary>
        /// Resets the level back to a state in which the game hasn't started yet.
        /// </summary>
        public void ResetLevel()
        {
            // Reset Player
            player.Reset();

            // Reset Endless Monster
            if (this.endless) follower.MovePositionBack();

            // Reset Particles
            heatParticles.Clear();
        }

        /// <summary>
        /// Resets the game's level state all the way back to room 1, with a screen value of 0.
        /// </summary>
        public void GoBackToFirstRoom()
        {
            // Reset Screen
            currentScreen = 0;
            roomLayout.GoToRoomOne();

            player.reachedEnd = false;
            SetScreenColor();

            firstRoomDevil.Show();
            backgroundDevil.Reset();
            follower.Reset();

            // Reset Player Values (score, lives, etc)
            player.lives = defaultLives;
            player.score = 0;

            // Show Disclaimer
            roomDisclaimer.visible = true;

            // Reset Room
            ResetLevel();
        }

        private void SetScreenColor()
        {
            SetScreenColor(0);
        }

        private void SetScreenColor(int screenColor)
        {
            if (screenColor == 0) this.screenColor = CustomColor.Red;
            else if (screenColor == 1) this.screenColor = CustomColor.DarkRed;
            else if (screenColor == 2) this.screenColor = CustomColor.Brown;
        }

        public override void OnUpdate(GameTime gameTime, Main main)
        {
            // Set Global Variables
            if (endless != main.endless) endless = main.endless;

            // While Unpaused
            if (!Global.paused)
            {
                // Update Level
                roomLayout.Update(gameTime);
                roomDisclaimer.Update(gameTime, player);

                // Update Enemies
                firstRoomDevil.Update(gameTime);
                if (currentScreen == screenWithBackgroundDevil) backgroundDevil.Update(gameTime);
                
                if (player.moving) // Enemies to only update while player is moving
                {
                    // Endless Follower
                    if (main.endless) follower.Update(gameTime, player, currentScreen);
                }
                // Reset Enemy Positions
                else
                {
                    follower.MovePositionBack();
                }

                player.SetRoom(this.roomLayout);
                player.Update(gameTime);

                // Game Over
                if (player.gameOver)
                {
                    // Go to Game Over Screen
                    SwitchState(main.title);

                    // Reset Game Over Flag
                    player.gameOver = false;
                }

                // Update Particles
                foreach (HeatParticle heatParticle in heatParticles) heatParticle.Update(gameTime);

                // Timer Events
                heatParticleTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (heatParticleTimer > timeBeforeNewHeatParticle)
                {
                    // Remove First Heat Particle
                    if (heatParticles.Count > heatParticleLimit) heatParticles.RemoveAt(0);

                    // Create New Heat Particle
                    GenerateHeat();

                    // Reset Timer
                    heatParticleTimer = 0f;
                }

                // Reset Room
                if (player.reachedEnd)
                {
                    // Randomize Room Layout
                    roomLayout.RandomizeRoom();

                    // Update Current Sceen Count
                    currentScreen++;

                    // Hide Enemies
                    if (currentScreen != 0) firstRoomDevil.Hide();
                    if (main.endless) follower.MovePositionBack();

                    // Add to Score
                    player.score += random.Next(2, 5);

                    // Randomize Screen Colour
                    randomScreenColor = random.Next(0, 3);
                    SetScreenColor(randomScreenColor);

                    // Set Flag to False
                    player.reachedEnd = false;
                }
            }

            // While Paused
            hud.Update(gameTime, player, currentScreen, main);

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
            heatBG.Draw(spriteBatch);
            if (currentScreen == screenWithBackgroundDevil) backgroundDevil.Draw(spriteBatch);
            firstRoomDevil.Draw(spriteBatch);

            // Level
            roomLayout.Draw(spriteBatch);
            player.Draw(spriteBatch);
            if (this.endless) follower.Draw(spriteBatch);

            // Particles
            foreach (HeatParticle heatParticle in heatParticles) heatParticle.Draw(spriteBatch);

            // HUD
            roomDisclaimer.Draw(spriteBatch);
            hud.Draw(spriteBatch);
            pauseOverlay.Draw(spriteBatch);
        }
    }
}
