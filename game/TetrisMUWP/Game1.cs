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
        public const int fieldRow = 18;
        public const int fieldColumn = 10;
        const float SKYRATIO = 2f / 3f;
        float screenWidth;
        float screenHeight;
        Texture2D grass;
        bool flag = true;
        const int blockSize = 50;
        int ypos = 0; //Block y
        int xpos = 0; //Block x
        int boardxpos = 0;
        int boardypos = 0;
        bool falling = true;
        const int boardX = 10;
        const int boardY = 18;
        int Position_Period = 300;
        int Period_Counter = 0;
        Random rnd = new Random();


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
        int[,] Rand_Piece = null;

        int [,] Field = new int[boardY, boardX];
        Vector2 FieldLocation = Vector2.Zero;
        Vector2 BlockLocation = Vector2.Zero;
        bool Rotate = false;
        bool Overflow = false;
        int middle = 4;
        int Backend_Falling = 0;
        int[,] test_block; 
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
            for(int y=0; y < boardY; y++)
            {
                for(int x= 0; x< boardX; x++)
                {
                    Field[y, x] = 0;
                }
            }
            
            screenHeight = (float)ApplicationView.GetForCurrentView().VisibleBounds.Height;
            screenWidth = (float)ApplicationView.GetForCurrentView().VisibleBounds.Width;
            Blocks.Add(Line);
            Blocks.Add(T);
            Blocks.Add(L);
            Blocks.Add(Backwards_L);
            Blocks.Add(Z);
            Blocks.Add(Backwards_Z);
            Blocks.Add(Box);
            Rand_Piece = (int[,])Blocks[rnd.Next(1, Blocks.Count)].Clone();

            // test_block = Field.original_block(Line, row, column);
            //modified_field = Field.Solid_Field();


            base.Initialize();
            previousState = Keyboard.GetState();
        }
        public bool Collision(int x, int y)
        {
            for(int BlockY = 0; BlockY < Rand_Piece.GetLength(0); BlockY++)
            {

                for (int BlockX = 0; BlockX < Rand_Piece.GetLength(0); BlockX++)
                {
                    int next_blockY = y + BlockY;
                    int next_blockX = x + BlockX;
                    if(Rand_Piece[BlockY, BlockX] != 0)
                    {
                        if(next_blockX<0 ||next_blockX >= 10)
                        {
                            return true;
                        }
                    }
                    if (next_blockY >=18  || Field[next_blockY, next_blockX] != 0)
                    {
                        return true;
                    } 

                }
                
            }
            return false;
        }
        public void Paste(int Fieldx, int Fieldy)
        {
            for(int y=0; y < Rand_Piece.GetLength(0); y++)
            {
                for(int x=0; x<Rand_Piece.GetLength(0); x++)
                {
                    int pasteX = Fieldx +x;
                    int pasteY = Fieldy+y;
                    if(Rand_Piece[y,x] !=0)
                        Field[pasteY-2, pasteX] = Rand_Piece[y, x];
                }
            }
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
            //Debug.WriteLine("x:" + xpos);
            //Debug.WriteLine("y:" + ypos);
            if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
            {
                if (!Collision((int)BlockLocation.X, (int)BlockLocation.Y))
                    BlockLocation += new Vector2(0,-1);
            }
            if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
            {

                if(!Collision((int)BlockLocation.X, (int)BlockLocation.Y))
                    BlockLocation += new Vector2(0,1);
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
            // TODO: Add your update logic here

            flag = false;

            Period_Counter += gameTime.ElapsedGameTime.Milliseconds;

            KeyboardHandler();
            if (Period_Counter >= Position_Period)
            {
                Vector2 NextSpot = BlockLocation + new Vector2(0, 1);
                if (Collision((int)BlockLocation.X, (int)BlockLocation.Y))
                {
                    Paste((int)BlockLocation.X, (int)BlockLocation.Y);
                    Rand_Piece = (int[,])Blocks[rnd.Next(1, Blocks.Count)].Clone();
                    BlockLocation = Vector2.Zero;
                }
                BlockLocation = NextSpot;
                Period_Counter = 0;
            }
            
            /*if (ypos < boardypos + 18 * blockSize - blockSize - blockSize * Rand_Piece.GetLength(0)) {
                ypos += 1;
                if (ypos == 18*blockSize-blockSize)
                {
                    flag = true;
                }
            }
            if (ypos == boardypos + 18 * blockSize - blockSize-blockSize * Rand_Piece.GetLength(0))
            {
                Debug.WriteLine("Collision!");
                Debug.WriteLine("x:" + xpos);
                Debug.WriteLine("y:" + ypos);
                Debug.WriteLine((int)(((xpos - boardxpos) + blockSize) / blockSize) - 1);
                Debug.WriteLine((int)((ypos - boardypos + blockSize) / blockSize) - 2);
                Field.field[(int) ((ypos - boardypos + blockSize + blockSize*Rand_Piece.GetLength(0)) / blockSize)- 1,(int)(((xpos - boardxpos) + blockSize) / blockSize) - 1] = 1;
                ypos = 0;
            }*/
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int y = 0; y < fieldRow; y++)
            {
                for (int x = 0; x < fieldColumn; x++)
                {
                    Color Field_Color = Block_Color[Field[y, x]];
                    if (Field[y, x] == 0 )
                    {
                        Field_Color = Color.FromNonPremultiplied(50, 50, 50, 50);
                    }
                    // Since for the board itself background colors are transparent, we'll go ahead and give this one
                    // a custom color.  This can be omitted if you draw a background image underneath your board
                    spriteBatch.Draw(grass, new Rectangle((int)FieldLocation.X + x * blockSize, (int)FieldLocation.Y + y * blockSize, blockSize, blockSize), Field_Color);
                }
            }
           
            for (int y = 0; y < Rand_Piece.GetLength(0); y++)
            {
                for (int x = 0; x < Rand_Piece.GetLength(0); x++)
                {
                    if(Rand_Piece[x,y] != 0)
                        spriteBatch.Draw(grass, new Rectangle((int)FieldLocation.X + ((int)BlockLocation.X + x) * blockSize,
                                                              (int)FieldLocation.Y + ((int)BlockLocation.Y + y) * blockSize, blockSize, blockSize),
                                                               Block_Color[Rand_Piece[x, y]]);

                } 
            }
            
            spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}
