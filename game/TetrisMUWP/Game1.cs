using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Newtonsoft.Json;
using TetrisMUWP.ScoreManager;
using Windows.UI.Xaml.Controls;


namespace TetrisMUWP
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //highScores hsPage = new highScores();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousState;
        public const int fieldRow = 18;
        public const int fieldColumn = 10;
        const float SKYRATIO = 2f / 3f;
        float screenWidth;
        float screenHeight;
        Texture2D grass;
        const int blockSize = 50;
        const int boardX = 10;
        const int boardY = 18;
        int Position_Period = 300;
        int Period_Counter = 0;
        int score = 0;
        bool GameOver = false;
        bool Pause = false;
        int ElapseTime = 0;
        Random rnd = new Random();

        /// <summary>
        /// Initialize blocks and colors used
        /// </summary>
        List<int[,]> Blocks = new List<int [,]>();
        Color[] Block_Color = {Color.Transparent, Color.Cyan, Color.Purple, Color.Orange, Color.Blue,
                                Color.Red, Color.Green, Color.Yellow};
        //All blocks in an array 
        int[,] Line = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T = new int[3, 3] { { 0, 0, 0 }, { 2, 2, 2 }, { 0, 2, 0 } };
        int[,] L = new int[3, 3] { { 0, 0, 0 }, { 3, 3, 3 }, { 3, 0, 0 } };
        int[,] Backwards_L = new int[3, 3] { { 0, 0, 0 }, { 4, 4, 4 }, { 0, 0, 4} };
        int[,] Z = new int[3, 3] { { 0, 0, 0 }, { 0, 5, 5 }, { 5, 5, 0 } };
        int[,] Backwards_Z = new int[3, 3] { { 0, 0, 0 }, { 6, 6, 0 }, { 0, 6, 6} };
        int[,] Box = new int[2, 2] { { 7, 7 }, { 7, 7 } };
        int[,] Rand_Piece = null;

        //Board of the game 

        /// <summary>
        /// Initialize Field and locations
        /// </summary>

        int [,] Field = new int[boardY, boardX];
        Vector2 FieldLocation =  new Vector2(450,500);
        Vector2 BlockLocation = Vector2.Zero;

        public object Window2 { get; private set; }
 

        /// <summary>
        /// Creates instance of game

        /// </summary>
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
            ///Creates empty field
            for(int y=0; y < boardY; y++)
            {
                for(int x= 0; x< boardX; x++)
                {
                    Field[y, x] = 0;
                }
            }
            //Window width and Height
            screenHeight = (float)ApplicationView.GetForCurrentView().VisibleBounds.Height;
            screenWidth = (float)ApplicationView.GetForCurrentView().VisibleBounds.Width;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            
            this.IsMouseVisible = true;
            //Add blocks to array fo blocks
            Blocks.Add(Line);
            Blocks.Add(T);
            Blocks.Add(L);
            Blocks.Add(Backwards_L);
            Blocks.Add(Z);
            Blocks.Add(Backwards_Z);
            Blocks.Add(Box);
            //create random instance of block
            Rand_Piece = (int[,])Blocks[rnd.Next(0, Blocks.Count)].Clone();
            GameOver = false;
            Pause = false;
            base.Initialize();
            previousState = Keyboard.GetState();
        }
        public void shiftDown()
        {
            int len = Rand_Piece.GetLength(0);

            bool go = false;
            int counter = 0;
            ///Check if the lower row is empty, if it is then it can be moved down one more time
            for (int i = 0; i < len; i++)
            {
                if (Rand_Piece[i, len-1] != 0)
                {
                    counter += 1;
                }
            }
            if (counter == 0)
                go = true;
            //If the row is empty shift the rows inside the matrix down
            if (go)
            {

                for (int row = 0; row < len; row++)
                {
                    for (int col = len-1; col > 0; col--)
                    {
                        Rand_Piece[row, col] = Rand_Piece[row, col-1];
                        Rand_Piece[row, col-1] = 0;
                    }
                }
            }
        }
        public void shiftLeft()
        {
            int len = Rand_Piece.GetLength(0);

            bool go = false;
            int counter = 0;
            ///Check if the left col is empty, if it is then it can be moved left one more time
            for (int i = 0; i < len; i++)
            {
                if (Rand_Piece[0, i] != 0)
                {
                    counter += 1;
                }
            }
            if (counter == 0)
                go = true;
            //If the Col is empty shift the rows inside the matrix left
            if (go)
            {

                for (int row = 0; row < len - 1; row++)
                {
                    for (int col = 0; col < len; col++)
                    {
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
            ///Check if the right row is empty, if it is then it can be moved right one more time
            for (int i = 0; i < len; i++)
            {
                if (Rand_Piece[len-1, i] != 0)
                {
                    counter += 1;
                }
            }
            if (counter == 0)
                go = true;
            //If the col is empty shift the rows inside the matrix right
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
        /// <summary>
        /// Function checks if there is a full row and removes it
        /// </summary>
        public void Check_Line()
        {
            bool clear = false;
            //iterate rows
            for (int i = fieldRow - 1; i >= 0; i--)
            {
                int count = 0; //counts to check if the row is full
                if (clear)
                    i = 0;
                //Iterate col
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
                            //every row clear gets 100 points
                            score += 100;
                            Debug.WriteLine("Score:" + score);
                            for (int w = i-1; w >= 0; w--)//Shift rows Down
                            {
                                for (int c = 0; c < 10; c++) {
                                    Field[w + 1, c] = Field[w, c];
                                }
                            }
                            clear = true;
                        }
                    }
                }

            }

        }
        /// <summary>
        /// Function rotates the falling block to the right
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public int [,] Rotate_Right(int [,] block)
        {
            int len = block.GetLength(0);
            int[,] temp_block = new int[len, len]; //temp block to switch rows and columns
            int i = 0;
            int j = 0;
            for (int x = 0; x < len; x++)
            {
                for (int y = len - 1; y >= 0; y--)
                {
                    temp_block[i, j] = block[y, x]; //copies the content from the original to the new 
                    j++;
                }
                j = 0;
                i++;
            }
            return temp_block;

        }
        /// <summary>
        /// Detects collison of blocks and side boundries
        /// </summary>
        /// <param name="block"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Collision(int[,] block, int x, int y)
        {
        
            for(int BlockY = 0; BlockY < block.GetLength(0); BlockY++)
            {

                for (int BlockX = 0; BlockX < block.GetLength(0); BlockX++)
                {
                    //gets the postions of each rectangle of the matrix 
                    int next_blockY = y + BlockY;
                    int next_blockX = x + BlockX;
                    //Only if the block is full 
                    if(Rand_Piece[BlockX, BlockY] != 0)
                    {
                        //checks for side boundries
                        if(next_blockX<0 ||next_blockX > 10)
                        {
                            return true;
                        }
                    }
                    //Compares field and block in order to check if it can continue to continue falling 
                    if (next_blockY >= 18 || (Field[next_blockY, next_blockX] != 0 && block[BlockX, BlockY] != 0))
                    {
                        //if there is space left shift down 
                        if (next_blockY == 18)
                            shiftDown();
                        return true;
                    }
                }
                
            }
            return false;
        }
        /// <summary>
        /// After the blocks have fallen, past it to the field in order 
        /// to use the field for comparisons 
        /// </summary>
        /// <param name="Fieldx"></param>
        /// <param name="Fieldy"></param>
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
            //If paused you can press any buttons 
            if (!Pause)
            {
                if (state.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape))
                {
                    //Exits the application 
                    this.Exit();
                }
                if (state.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))
                {
                    //generates a test rotation block to test for collision 
                    int[,] Orientation = Rotate_Right(Rand_Piece);
                    if (!Collision(Orientation, (int)BlockLocation.X, (int)BlockLocation.Y))
                        Rand_Piece = Orientation;
                }
                if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
                {
                    //shifts id there is an empty space 
                    if (BlockLocation.X == 10 - Rand_Piece.GetLength(0))
                    {
                        shiftRight();
                    }
                    else if (BlockLocation.X < 10)
                    {
                        //Adds and checks the next position 
                        Vector2 Next_Position = BlockLocation + new Vector2(1, 0);
                        if (!Collision(Rand_Piece, (int)Next_Position.X, (int)Next_Position.Y))
                            BlockLocation = Next_Position;
                    }
                }
                if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
                {
                    //shifts if there is an empty space
                    if (BlockLocation.X == 0)
                    {
                        shiftLeft();
                    }
                    if (BlockLocation.X != 0)
                    {
                        //Adds and checks the next position
                        Vector2 Next_Position = BlockLocation + new Vector2(-1, 0);
                        if (!Collision(Rand_Piece, (int)Next_Position.X, (int)Next_Position.Y))
                        {
                            BlockLocation = Next_Position;
                            Next_Position.X = -1;
                        }
                    }
                }
            }
            if (state.IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter))
            {
                //Pauses and resumes game when enter is hit 
                Pause = !Pause;
            }
            //Prevents you from holding down the key 
            previousState = state;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        //public static playerscore newnew = new playerscore();
        /*
        public void updatescore()
        {
     

            hsPage.NewName = "Rigo";
            hsPage.NewScore = score.ToString();
            hsPage.newplayer("Rigo", "100");

        }*/

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            //Debug.WriteLine(BlockLocation);

            if (!Pause)
            {
                //Timer for the game
                Period_Counter += gameTime.ElapsedGameTime.Milliseconds;
                //Was going to be used for save game
                ElapseTime += gameTime.ElapsedGameTime.Milliseconds;
            }
            KeyboardHandler();
            //Timer cycle reset
            if (Period_Counter > Position_Period)
            {
                //Adds 1 to the y position 
                Vector2 NextSpot = BlockLocation + new Vector2(0, 1);
                //Checks if the new possible spot collides with anything 
                if (Collision(Rand_Piece, (int)NextSpot.X, (int)NextSpot.Y))
                {
                    //If block reaches the bottom but still has room to keep moving down, shiftdown 
                    if (BlockLocation.Y == 18 - Rand_Piece.GetLength(0) && !Collision(Rand_Piece, (int)NextSpot.X, (int)NextSpot.Y))
                    {
                        shiftDown();
                    }
                    //Update field with steady block
                    Paste((int)BlockLocation.X, (int)BlockLocation.Y);
                    //Picks the next random piece 
                    Rand_Piece = (int[,])Blocks[rnd.Next(0, Blocks.Count)].Clone();
                    //generate random pos
                    int len = Rand_Piece.GetLength(0);
                    int ran = rnd.Next(0, fieldColumn - len);
                    Vector2 ranPos = new Vector2(ran, 0);
                    BlockLocation = ranPos;
                    //If the grid is full and a block tries to enter on that spot "GameOver"
                    if(Collision(Rand_Piece, (int)BlockLocation.X, (int)BlockLocation.Y))
                    {
                        Debug.WriteLine("Gameover");
                        GameOver = true;
                        Pause = true;

                        //input.Visibility = Visibility.Visible;
                    }
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
        /// This is called when the game should draw itself. If its not gameove 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //If its not gameover then keep drawing.
            if (!GameOver)
            {
                for (int y = 0; y < fieldRow; y++)
                {
                    for (int x = 0; x < fieldColumn; x++)
                    {
                        Color Field_Color = Block_Color[Field[y, x]];
                        if (Field[y, x] == 0)
                        {
                            //sets color to a somewhat transparent color
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
                        //only draw the solid blocks on the matrix not the empty blocks
                        if (Rand_Piece[x, y] != 0)
                            spriteBatch.Draw(grass, new Rectangle((int)FieldLocation.X + ((int)BlockLocation.X + x) * blockSize,
                                                                  (int)FieldLocation.Y + ((int)BlockLocation.Y + y) * blockSize, blockSize, blockSize),
                                                                   Block_Color[Rand_Piece[x, y]]);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}
