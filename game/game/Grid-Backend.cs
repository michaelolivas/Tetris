using System;
using System.Diagnostics;

namespace game
{
    public class Game_Grid
    {
        private int[,] field = new int[18,10]; 


        public Game_Grid()
        {
            //initialize the grid with 0s
            for(int i = 0; i<18; i++)
            {
                for(int j=0; j<10; j++)
                {
                    field[i,j] = 0;
                }
            }
        }
        
        public void Check_Line()
        {
            bool clear = false;
            for (int i = 17; i <= 0; i--)
            {
                int count = 0; //counts to check if the row is full
                for (int j = 0; j < 10; j++)
                {
                    if (field[i,j] == 1) //Checks if there is a peice of the object on that spot
                    {
                        count++;
                        if(count == 10) //if there is a peice of the object for that whole row, clear it
                        {
                            for (int k = 0; j < 10; k++)
                            {
                                field[i,k] = field[i - 1,k]; //deletes by copying the top row
                            }
                            clear = true;
                            //We hae to implemement the score function here!
                            i++;
                        }
                    }
                }
                if (clear && count != 10)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        field[i,j] = field[i - 1,j]; //if a row has been cleared, this pushes each row down.
                    }
                }
            }
        }

        public int [,] Rotate(int [,] block, int row, int column)
        {
            int[,] temp_block = new int[column, row]; //temp block to switch rows and columns
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    temp_block[j,i] = block[i,j]; //copies the content from the original to the new 
                }
            }
            return temp_block;
        }

        //Prints out the grid in the console.
        public void Print_Grid()
        {
            for(int i=0; i<18; i++)
            {
                for(int j=0; j<10; j++)
                {
                    Debug.Write($"{field[i,j]}");
                }
                Debug.WriteLine("");
            }
        }

        public void Falling_Block(int[,] block, int row, int column)
        {
            bool falling = true; 
            int middle = 4;
            for (int i = 0; i < 18; i++)
            {
                //Fisrt Insert in the middle
                if (column == 1)//straight line only
                {
                    if (i == 0) //first block ony 
                    {
                        field[i, middle] = block[row-1,0];
                    }
                    else if (i > 0 && i<4)//inseterts each block one by one.
                    {
                        if (i == 1)
                        {
                            field[i - 1, middle] = block[row - 2, 0];
                            field[i, middle] = block[row - 1, 0];
                        }
                        if (i == 2)
                        {
                            field[i - 2, middle] = block[row - 3, 0];
                            field[i - 1, middle] = block[row - 2, 0];
                            field[i, middle] = block[row - 1, 0];
                        }
                        if (i == 3)
                        {
                            field[i - 3, middle] = block[row - 4, 0];
                            field[i - 2, middle] = block[row - 3, 0];
                            field[i - 1, middle] = block[row - 2, 0];
                            field[i, middle] = block[row - 1, 0];
                        }
                    }
                    else if(i>=4)//Dropping of block by shifting it one and deleting the top.
                    {
                        field[i - 4, middle] = 0;
                        field[i, middle] = block[row - 1, 0];
                    }
                    //checks if there is anywhere else to go
                    if (i < 17 && field[i + 1, middle] == 1)
                        falling = false;
                }
                if(column == 2 && row == 2)//square only 
                {
                    if(i == 0) //bottom row is inserted 
                    {
                        field[i, middle] = block[1, 0];
                        field[i, middle + 1] = block[1, 1];
                    }
                    if(i == 1)//the whole square is now visible on the grid
                    {
                        field[i - 1, middle] = block[0, 0];
                        field[i - 1, middle + 1] = block[0, 1];
                        field[i, middle] = block[1, 0];
                        field[i, middle + 1] = block[1, 1];
                    }
                    if( i > 1)//Shifts the bottom row down and deletes the top row
                    {
                        field[i - 2, middle] = 0;
                        field[i - 2, middle + 1] = 0;
                        field[i - 1, middle] = block[0, 0];
                        field[i - 1, middle + 1] = block[0, 1];
                        field[i, middle] = block[1, 0];
                        field[i, middle + 1] = block[1, 1];
                    }
                    //Checks if the block can continue to fall 
                    if (i < 17 && (field[i + 1, middle] == 1 || field[i + 1, middle + 1] == 1))
                        falling = false;

                }

                if( column == 2 && row == 3)
                {
                    //Insures that when colliding with onother object it'll be able to be combined
                    if (field[i, middle] == 1 && block[1, 1] == 0)
                        block[2, 0] = 1;
                    if (field[i, middle + 1] == 1 && block[1, 2] == 0)
                        block[2, 1] = 1;
                    //Falling object
                    if (i == 0)//last row inserted 
                    {
                        field[i, middle] = block[2, 0];
                        field[i, middle + 1] = block[2, 1];
                    }
                    if (i == 1)// the whole object inserted 
                    {
                        field[i - 1, middle] = block[1, 0];
                        field[i - 1, middle + 1] = block[1, 1];
                        field[i, middle] = block[2, 0];
                        field[i, middle + 1] = block[2, 1];
                    }
                    if(i == 2)
                    {
                        field[i - 2, middle] = block[0, 0]; ;
                        field[i - 2, middle + 1] = block[0, 1];
                        field[i - 1, middle] = block[1, 0];
                        field[i - 1, middle + 1] = block[1, 1];
                        field[i, middle] = block[2, 0];
                        field[i, middle + 1] = block[2, 1];
                    }
                    if( i > 2)
                    {
                        field[i - 3, middle] = 0;
                        field[i - 3, middle + 1] = 0;
                        field[i - 2, middle] = block[0, 0];
                        field[i - 2, middle + 1] = block[0, 1];
                        field[i - 1, middle] = block[1, 0];
                        field[i - 1, middle + 1] = block[1, 1];
                        field[i, middle] = block[2, 0];
                        field[i, middle + 1] = block[2, 1];
                    }
                    if (i < 17 && ((field[i + 1, middle] == 1 && block[2, 0] == 1)
                        || (field[i + 1, middle+1] == 1 && block[2, 1] == 1)))
                    {
                        falling = false;
                    }
                }
                if (column == 3 && row == 2)//for laying down Z T and L 
                {
                    //Insures that when colliding with onother object it'll be able to be combined
                    if (field[i, middle-1] == 1 && block[1, 0] == 0)
                        block[1, 0] = 1;
                    if (field[i, middle] == 1 && block[1, 1] == 0)
                        block[1, 1] = 1;
                    if (field[i, middle + 1] == 1 && block[1, 2] == 0)
                        block[1, 2] = 1;

                    //Falling object
                    if (i == 0)//last row inserted 
                    {
                        field[i, middle - 1] = block[1, 0];
                        field[i, middle] = block[1, 1];
                        field[i, middle + 1] = block[1, 2];
                    }
                    if(i == 1)// the whole object inserted 
                    {
                        field[i-1, middle - 1] = block[0, 0];
                        field[i-1, middle] = block[0, 1];
                        field[i-1, middle + 1] = block[0, 2];
                        field[i, middle - 1] = block[1, 0];
                        field[i, middle] = block[1, 1];
                        field[i, middle + 1] = block[1, 2];
                    }
                    if (i > 1)//keeps moving down while deleting the previous position
                    {
                        field[i - 2, middle - 1] = 0;
                        field[i - 2, middle] = 0;
                        field[i - 2, middle + 1] = 0;
                        field[i - 1, middle - 1] = block[0, 0];
                        field[i - 1, middle] = block[0, 1];
                        field[i - 1, middle + 1] = block[0, 2];
                        field[i, middle - 1] = block[1, 0];
                        field[i, middle] = block[1, 1];
                        field[i, middle + 1] = block[1, 2];
                    }
                    if(i<17 &&((field[i+1, middle -1] ==1 && block[1,0] == 1)
                        || (field[i+1, middle]==1 && block[1,1] == 1)|| (field[i+1, middle +1] ==1&& block[1,2] ==1)))
                    {
                        falling = false;
                    }
                }

                if (column == 4)//straight line only, sideways 
                {
                    if (i == 0)//insertes the whole object
                    {
                        field[i, middle - 1] = block[0, column - 1];
                        field[i, middle] = block[0, column - 2];
                        field[i, middle + 1] = block[0, column - 3];
                        field[i, middle + 2] = block[0, column - 4];
                    }
                    if (i > 0)
                    {
                        //moves down the object and clear the preious position
                        int walker = 1;
                        for (int j = middle-1; j<= middle + 2; j++)
                        {
                            field[i - 1, j] = 0;
                            field[i, j] = block[0, column - walker];
                            walker++;
                        }
                    }
                    //Checks if it can continue falling
                    if (i < 17 && (field[i + 1, middle - 1] == 1 || field[i + 1, middle] == 1 || field[i + 1, middle + 1] == 1 || field[i + 1, middle + 2] == 1))
                    {
                        falling = false;
                    }
                }
                    /*
                       if(rotate){
                            block = Rotate(block, row, column);
                            int temp = row;
                            row = column;
                            column = row;
                       }
                   */
                Print_Grid();
                Debug.WriteLine("");
                if (!falling)
                    break;
            }
        }
    }
}

