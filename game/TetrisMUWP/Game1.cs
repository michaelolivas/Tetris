using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using Windows.UI.ViewManagement;

namespace TetrisMUWP
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const float SKYRATIO = 2f / 3f;
        float screenWidth;
        float screenHeight;
        Texture2D grass;
        Texture2D teeBar;
        bool color = true;
        bool flag;
        int ypos = 0;
        int xpos = 200;

        private TimeSpan? lastBulletShot;
        private static readonly TimeSpan ShootInterval = TimeSpan.FromSeconds(100);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenHeight = (float)ApplicationView.GetForCurrentView().VisibleBounds.Height;
            screenWidth = (float)ApplicationView.GetForCurrentView().VisibleBounds.Width;
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
            grass = Content.Load<Texture2D>("grass");
            //teeBar = Content.Load<Texture2D>("grass");

            // TODO: use this.Content to load your game content here
        }
        void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Right))
            {
                Debug.WriteLine("D");
                xpos += 25;
            }
            if (state.IsKeyDown(Keys.Left))
                xpos -= 25;

        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // TODO: Add your update logic here
            flag = false;
            KeyboardHandler();
            if (ypos < 500)
            {
                ypos += 1;
                if (ypos == 499)
                {
                    flag = true;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //int num = new Random().Next(1, 2);
            //spriteBatch.Begin();
            shapes();
            //spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
        private void shapes()
        {
            for (int i = 0; i <= 5; i++)
            {
                int x = new Random().Next(1, 3);
                switch (x)
                {
                    case 1:
                        Debug.WriteLine("box");
                        spriteBatch.Begin();
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos, 25, 25), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos + 25, ypos, 25, 25), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + 25, 25, 25), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos + 25, ypos + 25, 25, 25), Color.White);
                        spriteBatch.End();
                        break;

                    case 2:
                        Debug.WriteLine("line");
                        spriteBatch.Begin();
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos, 25, 25), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + 25, 25, 25), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + 50, 25, 25), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + 75, 25, 25), Color.White);
                        spriteBatch.End();
                        break;
                }
            }

        }

    }
}
