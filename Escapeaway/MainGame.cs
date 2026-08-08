// ESCAPEAWAY!
// https://snowman64.itch.io/escapeaway

// Created by Snowman64
// Developed from August 8, 2026 - TBA

// Made for BOSS BASH JAM 4
// https://itch.io/jam/boss-bash-jam-4

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Escapeaway.Source;
using Escapeaway.Source.States;

namespace Escapeaway
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Public Variables
        public static GameTime gameTime;
        public static GraphicsDeviceManager publicGraphics;
        public static GraphicsDevice publicGraphicsDevice;

        // Game State
        private Main game;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set Public Variables
            publicGraphics = this.graphics;
            publicGraphicsDevice = this.GraphicsDevice;

            // Set Window Properties
            this.Window.Title = Global.windowName;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += WindowSizeChanged;

            base.Initialize();
        }

        private void WindowSizeChanged(object sender, EventArgs e)
        {
            game.cam.SetDestRect();
        }

        private void SetWindowSize(int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();

            game.cam.SetDestRect();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Game Assets
            Global.LoadAssets(this.Content);

            // Set Game State
            game = new Main();

            // Set Window Size on Startup
            SetWindowSize(Global.displayWidth, Global.displayHeight);
        }

        protected override void Update(GameTime mainGameTime)
        {
            // Quit Game
            if (Global.quit) Exit();

            // Update Global Variables
            Global.active = this.IsActive;
            gameTime = mainGameTime;

            // Update Game
            game.Update(mainGameTime);

            base.Update(mainGameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            game.cam.Activate();

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullNone
            );
            game.Draw(spriteBatch);
            spriteBatch.End();

            game.cam.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
