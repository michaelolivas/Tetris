using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        KeyboardState previousState;
        const float SKYRATIO = 2f / 3f;
        float screenWidth;
        float screenHeight;
        Texture2D grass;
        Texture2D teeBar;
        bool color = true;
        bool flag;

        int ypos = 0;
        int xpos = 200;

        const int boardX = 10;
        const int boardY = 18;

        int row = 4;
        int column = 4;
        int[,] test_block;
        int[,] modified_field;
        bool rotate = false;
        bool falling = true;
        bool overflow = false;
        int middle = 4;
        int i = 0;

        List<int[,]> Blocks = new List<int [,]>();
        Color[] Block_Color = {Color.Transparent, Color.Cyan, Color.Purple, Color.Orange, Color.Blue,
                                Color.Red, Color.Green, Color.Yellow};
        int[,] Line = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T = new int[3, 3] { { 0, 0, 0 }, { 2, 2, 2 }, { 0, 2, 0 } };
        int[,] L = new int[3, 3] { { 0, 0, 0 }, { 3, 3, 3 }, { 3, 0, 0 } };
        int[,] Backwards_L = new int[3, 3] { { 0, 0, 0 }, { 4, 4, 4 }, { 0, 0, 4} };
        int[,] Z = new int[3, 3] { { 0, 0, 0 }, { 0, 5, 5 }, { 5, 5, 0 } };
        int[,] Backwards_Z = new int[3, 3] { { 0, 0, 0 }, { 5, 5, 0 }, { 0, 5, 5} };
        int[,] Box = new int[2, 2] { { 6, 6 }, { 6, 6 } };

        Game_Grid Field;

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
            graphics.PreferredBackBufferWidth = 700;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            Field = new Game_Grid();
            Blocks.Add(Line);
            Blocks.Add(T);
            Blocks.Add(L);
            Blocks.Add(Backwards_L);
            Blocks.Add(Z);
            Blocks.Add(Backwards_Z);
            Blocks.Add(Box);
            // test_block = Field.original_block(Line, row, column);
            //modified_field = Field.Solid_Field();


            base.Initialize();
            previousState = Keyboard.GetState();
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

            if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
            {
                xpos += 25;
            }
            if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
                xpos -= 25;
            previousState = state;
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
        /// 
        protected override void Update(GameTime gameTime)
        {
            //Line = Field.original_block(Line, row, column);
            //test_block = Field.original_block(Line, row, column);
            //falling = Field.Falling_Block(Line, row, column, test_block, modified_field, rotate, falling, overflow, middle, i);
            //i++;

            //ypos = row * 25;
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
            //spriteBatch.End();

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for(int y = 0; y< boardY; y++)
            {
                for(int x = 0; x<boardX; x++)
                {
                    Color Field_Color = Block_Color[Field.field[y, x]];



                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
