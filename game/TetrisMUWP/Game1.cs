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
        Texture2D teeBar;
        bool color = true;
        bool flag;
        const int blockSize = 50;
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

            Field = new Game_Grid();

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
            Debug.WriteLine("x:" + xpos);
            Debug.WriteLine("y:" + ypos);
            if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
            {
                if(xpos < boardxpos + 10*blockSize-blockSize)
                xpos += blockSize;
            }
            if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
            {

                if(xpos> boardxpos)
                xpos -= blockSize;
            }
            if (state.IsKeyDown(Keys.Space))
            {
                if (ypos <  17 * blockSize)
                    ypos += 5;
            }
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

            //ypos = row * blockSize;
            // TODO: Add your update logic here
            flag = false;
            KeyboardHandler();
            if (ypos < boardypos + 18 * blockSize - blockSize) {
                ypos += 1;
                if (ypos == 499)
                {
                    flag = true;
                }
            }
            if (ypos == boardypos + 18 * blockSize - blockSize)
            {
                Debug.WriteLine("Collision!");
                Debug.WriteLine("x:" + xpos);
                Debug.WriteLine("y:" + ypos);
                Debug.WriteLine((int)(((xpos - boardxpos) + blockSize) / blockSize) - 1);
                Debug.WriteLine((int)((ypos - boardypos + blockSize) / blockSize) - 2);
                Field.field[(int) ((ypos - boardypos + blockSize) / blockSize)- 1,(int)(((xpos - boardxpos) + blockSize) / blockSize) - 1] = 1;
                ypos = 0;
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
            //shapes();
            //spriteBatch.End();

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int y = 0; y < fieldRow; y++)
            {
                for (int x = 0; x < fieldColumn; x++)
                {
                    Color currColor = Color.Transparent;
                    //Color tintColor = TetronimoColors[Board[x, y]];
                    if (Field.field[y, x] == 1)
                    {
                        currColor = Color.Red;
                    }
                    // Since for the board itself background colors are transparent, we'll go ahead and give this one
                    // a custom color.  This can be omitted if you draw a background image underneath your board

                    spriteBatch.Draw(grass, new Rectangle(boardxpos + x * blockSize, boardypos + y * blockSize, blockSize, blockSize), currColor);
                }
            }
            spriteBatch.Draw(grass, new Rectangle(xpos, ypos, blockSize, blockSize), Color.White);
            spriteBatch.Draw(grass, new Rectangle(xpos, ypos, blockSize, blockSize), Color.White);
            spriteBatch.Draw(grass, new Rectangle(xpos + blockSize, ypos, blockSize, blockSize), Color.White);
            spriteBatch.Draw(grass, new Rectangle(xpos, ypos - blockSize, blockSize, blockSize), Color.White);
            spriteBatch.Draw(grass, new Rectangle(xpos + blockSize, ypos - blockSize, blockSize, blockSize), Color.White);
            spriteBatch.End();
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
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos, blockSize, blockSize), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos + blockSize, ypos, blockSize, blockSize), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + blockSize, blockSize, blockSize), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos + blockSize, ypos + blockSize, blockSize, blockSize), Color.White);
                        spriteBatch.End();
                        break;

                    case 2:
                        Debug.WriteLine("line");
                        spriteBatch.Begin();
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos, blockSize, blockSize), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + blockSize, blockSize, blockSize), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + 50, blockSize, blockSize), Color.White);
                        spriteBatch.Draw(grass, new Rectangle(xpos, ypos + 75, blockSize, blockSize), Color.White);
                        spriteBatch.End();
                        break;
                }
            }

        }

    }
}
