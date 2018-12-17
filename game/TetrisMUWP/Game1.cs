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
        KeyboardState previousState;
        public const int fieldRow = 18;
        public const int fieldColumn = 10;
        const float SKYRATIO = 2f / 3f;
        float screenWidth;
        float screenHeight;
        Texture2D grass;
        int ypos = 0;
        int xpos = 200;
        int boardxpos = 100;
        int boardypos = 100;

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

        int[,] Line = new int[4, 4] { { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 } };
        int[,] Box = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        int[,] L = new int[3, 3] { { 0, 1, 1 }, { 0, 0, 1 }, { 0, 0, 1 } };
        int[,] T = new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
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

            //Field = new Game_Grid();

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

            // TODO: use this.Content to load your game content here
        }
        void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
            {
                if(xpos < boardxpos + 10*25-25)
                xpos += 25;
            }
            if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
            {

                if(xpos> boardxpos)
                xpos -= 25;
            }
            if (state.IsKeyDown(Keys.Space))
                if(ypos < 18*25)
                    ypos += 25;
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
            KeyboardHandler();
            if (ypos < boardypos + 18*25 -25)
                ypos += 1;
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
            for (int y = 0; y < fieldRow; y++)
            {
                for (int x = 0; x < fieldColumn; x++)
                {
                    //Color tintColor = TetronimoColors[Board[x, y]];

                    // Since for the board itself background colors are transparent, we'll go ahead and give this one
                    // a custom color.  This can be omitted if you draw a background image underneath your board

                    spriteBatch.Draw(grass, new Rectangle(boardxpos + x * 25, boardypos + y * 25, 25, 25),new Rectangle(0, 0, 32, 32), Color.Black);
                }
            }
            spriteBatch.Draw(grass, new Rectangle(xpos, ypos, 25, 25), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}
