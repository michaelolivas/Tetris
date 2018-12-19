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
        int boardxpos = 100;
        int boardypos = 100;
        bool falling = true;
        const int boardX = 10;
        const int boardY = 18;
        int Position_Period = 300;
        int Period_Counter = 0;
        int score = 0;
        Random rnd = new Random();


        List<int[,]> Blocks = new List<int [,]>();
        Color[] Block_Color = {Color.Transparent, Color.Cyan, Color.Purple, Color.Orange, Color.Blue,
                                Color.Red, Color.Green, Color.Yellow};
        int[,] Line = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T = new int[3, 3] { { 0, 0, 0 }, { 2, 2, 2 }, { 0, 2, 0 } };
        int[,] L = new int[3, 3] { { 0, 0, 0 }, { 3, 3, 3 }, { 3, 0, 0 } };
        int[,] Backwards_L = new int[3, 3] { { 0, 0, 0 }, { 4, 4, 4 }, { 0, 0, 4} };
        int[,] Z = new int[3, 3] { { 0, 0, 0 }, { 0, 5, 5 }, { 5, 5, 0 } };
        int[,] Backwards_Z = new int[3, 3] { { 0, 0, 0 }, { 6, 6, 0 }, { 0, 6, 6} };
        int[,] Box = new int[2, 2] { { 7, 7 }, { 7, 7 } };
        int[,] Rand_Piece = null;

        int [,] Field = new int[boardY, boardX];
        Vector2 FieldLocation =  new Vector2(450,500);
        Vector2 BlockLocation = Vector2.Zero;
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
            //screenHeight = (float)ApplicationView.GetForCurrentView().VisibleBounds.Height;
            //screenWidth = (float)ApplicationView.GetForCurrentView().VisibleBounds.Width;
            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();
            
            this.IsMouseVisible = true;
            Blocks.Add(Line);
            Blocks.Add(T);
            Blocks.Add(L);
            Blocks.Add(Backwards_L);
            Blocks.Add(Z);
            Blocks.Add(Backwards_Z);
            Blocks.Add(Box);
            Rand_Piece = (int[,])Blocks[rnd.Next(0, Blocks.Count)].Clone();

            base.Initialize();
            previousState = Keyboard.GetState();
        }
        public void shiftDown()
        {

        }
        public void shiftLeft()
        {
            Debug.WriteLine("shL");
            int len = Rand_Piece.GetLength(0);

            bool go = false;
            int counter = 0;
            for (int i = 0; i < len; i++)
            {
                if (Rand_Piece[0, i] != 0)
                {
                    counter += 1;
                }
            }
            if (counter == 0)
                go = true;
            if (go)
            {

                for (int row = 0; row < len - 1; row++)
                {
                    for (int col = 0; col < len; col++)
                    {
                        Debug.WriteLine("r:" + row + "col: " + col);
                        Rand_Piece[row, col] = Rand_Piece[row + 1, col];
                        Rand_Piece[row + 1, col] = 0;
                    }
                }
            }
        }
        public void shiftRight()
        {
            int len = Rand_Piece.GetLength(0);
            bool go = false;
            int counter = 0;
            for (int i = 0; i < len; i++)
            {
                if (Rand_Piece[i, 0] != 0)
                {
                    counter += 1;
                }
            }
            if (counter == 0)
                go = true;
            if (go)
            {

                for (int row = len - 1; row > 0; row--)
                {
                    for (int col = 0; col < len; col++)
                    {
                        Rand_Piece[row, col] = Rand_Piece[row - 1, col];
                        Rand_Piece[row - 1, col] = 0;
                    }
                }
            }
            
        }
        public void Check_Line()
        {
            bool clear = false;
            for (int i = fieldRow - 1; i >= 0; i--)
            {
                int count = 0; //counts to check if the row is full
                if (clear)
                    i = 0;
                for (int j = 0; j < fieldColumn; j++)
                {
                    if (Field[i, j] != 0) //Checks if there is a peice of the object on that spot
                    {
                        count++;
                        if (count == fieldColumn) //if there is a peice of the object for that whole row, clear it
                        {
                            for (int k = 0; k < fieldColumn; k++)//clear row
                            {
                                Field[i, k] = 0;
                            }
                            score += 100;
                            for (int w = i-1; w >= 0; w--)//Shift rows Down
                            {
                                for (int c = 0; c < 10; c++) {
                                    Field[w + 1, c] = Field[w, c];
                                }
                            }
                            clear = true;
                            //i = 0;
                            //We hae to implemement the score function here!
                        }
                    }
                }

            }

        }
        public void Rotate_Right()
        {
            int len = Rand_Piece.GetLength(0);
            int[,] temp_block = new int[len, len]; //temp block to switch rows and columns
            int i = 0;
            int j = 0;
            for (int x = 0; x < len; x++)
            {
                for (int y = len - 1; y >= 0; y--)
                {
                    temp_block[i, j] = Rand_Piece[y, x]; //copies the content from the original to the new 
                    j++;
                }
                j = 0;
                i++;
            }
            Rand_Piece = temp_block;

        }
        public bool Collision(int x, int y)
        {
        
            for(int BlockY = 0; BlockY < Rand_Piece.GetLength(0); BlockY++)
            {

                for (int BlockX = 0; BlockX < Rand_Piece.GetLength(0); BlockX++)
                {
                    int next_blockY = y + BlockY;
                    int next_blockX = x + BlockX;
                    if(Rand_Piece[BlockX, BlockY] != 0)
                    {
                        if(next_blockX<0 ||next_blockX > 10)
                        {
                            return true;
                        }
                    }
                    if (next_blockY >= 18 || (Field[next_blockY, next_blockX] != 0 && Rand_Piece[BlockX, BlockY] != 0))
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
                    if(Rand_Piece[x,y] !=0)
                        Field[pasteY, pasteX] = Rand_Piece[x, y];
                }
            }
            for (int i = 0; i < fieldRow; i++)
            {
                for (int j = 0; j < fieldColumn; j++)
                {
                    Debug.Write($"{Field[i, j]}");
                }
                Debug.WriteLine("");
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
            if (state.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            if (state.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))
            {
                Rotate_Right();
            }
            if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
            {
                if (BlockLocation.X == 10-Rand_Piece.GetLength(0))
                {
                    shiftRight();
                }
                else if (BlockLocation.X < 10)
                {
                    Vector2 Next_Position = BlockLocation + new Vector2(1, 0);
                    if (!Collision((int)Next_Position.X, (int)Next_Position.Y))
                        BlockLocation = Next_Position;
                }
            }
            if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
            {
                if(BlockLocation.X == 0)
                {
                    shiftLeft();
                }
                if (BlockLocation.X != 0)
                {
                    Vector2 Next_Position = BlockLocation + new Vector2(-1, 0);
                    if (!Collision((int)Next_Position.X, (int)Next_Position.Y))
                    {
                        BlockLocation = Next_Position;
                        Next_Position.X = -1;
                    }
                }
            }
            /*if (state.IsKeyDown(Keys.Space))
            {
                if (ypos <  17 * blockSize)
                    ypos += 5;
            }*/
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
            Debug.WriteLine(BlockLocation);

            flag = false;

            Period_Counter += gameTime.ElapsedGameTime.Milliseconds;

            KeyboardHandler();
            if (Period_Counter > Position_Period)
            {
                Vector2 NextSpot = BlockLocation + new Vector2(0, 1);
                if (Collision((int)NextSpot.X, (int)NextSpot.Y))
                {
                    Paste((int)BlockLocation.X, (int)BlockLocation.Y);
                    Rand_Piece = (int[,])Blocks[rnd.Next(0, Blocks.Count)].Clone();
                    //generate random pos
                    int len = Rand_Piece.GetLength(0);
                    int ran = rnd.Next(0, fieldColumn - len);
                    Vector2 ranPos = new Vector2(ran, 0);
                    BlockLocation = ranPos;
                }
                else
                {
                        BlockLocation = NextSpot;
                }
                Check_Line();
                Period_Counter = 0;
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
