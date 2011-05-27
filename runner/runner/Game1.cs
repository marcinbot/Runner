using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices;
using runner.Platform;
using runner.Object;

namespace runner
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        enum GameMode { MainMenu, GameScreen, GameOver };

        List<PlatformTemplate> platforms;
        List<ObstacleTemplate> obstacles;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameState.random = new Random();

            GameState.player = new Player();
            platforms = new List<PlatformTemplate>();
            obstacles = new List<ObstacleTemplate>();
            
            GameState.debug = "";

            GameState.scrollingSpeed = 7.0f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");

            //load textures
            Textures.dummy = Content.Load<Texture2D>("dummy");
            Textures.building = Content.Load<Texture2D>("building");
            Textures.runner = Content.Load<Texture2D>("runner");
            Textures.obstacles = Content.Load<Texture2D>("obstacles");
            Textures.sky = Content.Load<Texture2D>("sky");
            Textures.clouds = Content.Load<Texture2D>("clouds");

            GameState.player.Initialize(Content.Load<Texture2D>("player"),
            GraphicsDevice.Viewport.TitleSafeArea.X + 100,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height - 150);

            //create first platforms
            platforms.Add(new TunnelPlatform(0, GraphicsDevice.Viewport.TitleSafeArea.Height, 14, 300, Textures.dummy));
            platforms.Add(new NormalPlatform(750, GraphicsDevice.Viewport.TitleSafeArea.Height, 5, 150, Textures.dummy));
            platforms.Add(new NormalPlatform(1050, GraphicsDevice.Viewport.TitleSafeArea.Height, 4, 100, Textures.dummy));
            platforms.Add(new NormalPlatform(1300, GraphicsDevice.Viewport.TitleSafeArea.Height, 8, 90, Textures.dummy));
            obstacles.AddRange(platforms.ElementAt(3).PlaceObstacleOnPlatform(25, 25, 1));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            GameState.player.Update(gameTime, platforms);

            if (GameState.player.isRunning)
            {
                //update scores
                GameState.score += GameState.scrollingSpeed / 50;
                //update platforms
                foreach (PlatformTemplate p in platforms)
                {
                    p.boundingBox.X -= (int)GameState.scrollingSpeed;
                    p.Update();
                }
                //update objects on platforms
                foreach (ObstacleTemplate o in obstacles)
                {
                    o.boundingBox.X -= (int)GameState.scrollingSpeed;
                    o.Update(GameState.player);
                }
            }

            //increase speed
            if (GameState.scrollingSpeed<30)
                GameState.scrollingSpeed += 0.015f;
            GameState.debug = Convert.ToString(GameState.scrollingSpeed);

            if (platforms.Count > 0)
            {
                //delete platforms that are out of sight
                if (platforms.ElementAt(0).boundingBox.Right < 0)
                    platforms.RemoveAt(0);
                //if there are no more platforms to show, generate new ones
                if (platforms.Last().boundingBox.Right < GraphicsDevice.Viewport.TitleSafeArea.Right)
                    PlatformFactory.UpdatePlatformsList(platforms, obstacles, GraphicsDevice.Viewport.TitleSafeArea.Height);
            }

            if (obstacles.Count > 0)
            {
                if (obstacles.ElementAt(0).boundingBox.Right < 0)
                    obstacles.RemoveAt(0);
            }



            if (GameState.player.boundingBox.Top > GraphicsDevice.Viewport.TitleSafeArea.Height)
                this.Exit();
            //GameState.debug = Convert.ToString(GraphicsDevice.Viewport.TitleSafeArea.Height) + " " + Convert.ToString(GraphicsDevice.Viewport.TitleSafeArea.Width);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Textures.sky, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(spriteFont, GameState.debug, new Vector2(0, 0), Color.Black);
            GameState.scoreString = Convert.ToString((int)GameState.score);
            spriteBatch.DrawString(spriteFont, GameState.scoreString, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width - spriteFont.MeasureString(GameState.scoreString).X, 0), Color.Black);

            GameState.player.Draw(spriteBatch);

            foreach (PlatformTemplate p in platforms)
                p.Draw(spriteBatch);

            foreach (ObstacleTemplate o in obstacles)
                o.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
