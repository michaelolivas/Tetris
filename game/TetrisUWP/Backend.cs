using System;
using System.Diagnostics;

namespace TetrisUWP
{
    public class Game_Grid
    {
        private int[,] field = new int[18, 10];


        public Game_Grid()
        {
            //initialize the grid with 0s
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = 0;
                }
            }
        }

        public void Check_Line()
        {
            for (int i = 17; i >= 0; i--)
            {
                int count = 0; //counts to check if the row is full
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == 1) //Checks if there is a peice of the object on that spot
                    {
                        count++;
                        if (count == 10) //if there is a peice of the object for that whole row, clear it
                        {
                            for (int k = 0; k < 10; k++)
                            {
                                for (int z = i; z >= 0; z--)
                                {
                                    if (z >= 1)
                                    {
                                        field[z, k] = field[z - 1, k];
                                    }

                                    if (z == 0)
                                    {
                                        field[z, k] = 0;
                                    }

                                }
                                Print_Grid();
                                Debug.WriteLine("");
                            }
                            //We hae to implemement the score function here!
                            i++;
                        }
                    }
                }

            }

        }

        public int[,] Rotate_Left(int[,] block, int row, int column)
        {
            int[,] temp_block = new int[row, column]; //temp block to switch rows and columns
            int i = 0;
            int j = 0;
            for (int x = column - 1; x >= 0; x--)
            {
                for (int y = 0; y < row; y++)
                {
                    temp_block[i, j] = block[y, x]; //copies the content from the original to the new 
                    j++;
                }
                j = 0;
                i++;
            }
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < column; j++)
                {
                    Debug.Write($"{temp_block[i, j]}");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("");
            return temp_block;

        }
        public int[,] Rotate_Right(int[,] block, int row, int column)
        {
            int[,] temp_block = new int[row, column]; //temp block to switch rows and columns
            int i = 0;
            int j = 0;
            for (int x = 0; x < column; x++)
            {
                for (int y = row - 1; y >= 0; y--)
                {
                    temp_block[i, j] = block[y, x]; //copies the content from the original to the new 
                    j++;
                }
                j = 0;
                i++;
            }
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < column; j++)
                {
                    Debug.Write($"{temp_block[i, j]}");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("");
            return temp_block;

        }

        //Prints out the grid in the console.
        public void Print_Grid()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Debug.Write($"{field[i, j]}");
                }
                Debug.WriteLine("");
            }
        }


        /*

         */

        public void Falling_Block(int[,] block, int row, int column)
        {
            bool rotate = false;
            bool falling = true;
            bool overflow = false;
            int middle = 4;
            int walker;
            int row_counter = 0;
            int row_remainder = 0;
            int counter = 0;

            for (int i = 0; i < 18; i++)
            {
                //Fisrt Insert in the middle

                    if (i == 0) //first block ony 
                    {
                        walker = 0;
                        for (int x = 0; x < column; x++)
                        {
                            if (field[i, middle - 1 + walker] == 1 && block[row - 1, x] == 1)
                            {
                                falling = false;
                                break;
                            }
                            walker++;
                        }
                        if (falling)
                        {
                            walker = 0;
                            for (int x = 0; x < column; x++)
                            {
                                field[i, middle - 1 + walker] = block[row - 1, x];
                                walker++;
                            }
                        }
                    }
                    if (i > 0)//inseterts each block one by one.
                    {
                        row_counter = (i < row) ? i : row-1;
                        if (falling)
                        {
                            for (int y = 0; y < row_counter+1; y++)
                            {
                                walker = 0;
                                for (int x = 0; x < column; x++)
                                {
                                    field[i - y, middle - 1 + walker] = block[row - 1 - y, x];
                                    if (i >=row)
                                    {
                                        field[i - row, middle - 1 + walker] = 0;
                                    }
                                    walker++;
                                }
                            }
                        }

                    }

                    if (i == 17)
                    {
                        do {
                            counter = 0;
                            for (int l = 0; l < column; l++)
                            {
                                if (block[row - 1, l] == 0)
                                {
                                    counter++;
                                    if (counter == column)
                                    {
                                        overflow = true;
                                        row--;
                                        
                                        break;
                                    }
                                }
                                if (block[row - 1, l] == 1)
                                {
                                    overflow = false;
                                    break;
                                }
                            }
                            for (int y = 0; y < row; y++)
                            {
                                walker = 0;
                                for (int x = 0; x < column; x++)
                                {
                                    field[i - y, middle - 1 + walker] = block[row - 1 - y, x];
                                    if (i >= row)
                                    {
                                        field[i - row, middle - 1 + walker] = 0;
                                    }
                                    walker++;
                                }
                            }
                            row_remainder++;
                        } while (overflow) ;
                    }

                    Print_Grid();
                    Debug.WriteLine("");
                    //if (!falling)
                    // break;
                
                Check_Line();
            }
        }
    }
}
