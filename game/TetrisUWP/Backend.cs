using System;
using System.Diagnostics;

namespace TetrisUWP
{
    public class Game_Grid
    {
        public int[,] field = new int[18, 10];

        public int[,] Board()
        {
            //initialize the grid with 0s
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = 0;
                }
            }
            return field;
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
                            }
                            //We hae to implemement the score function here!
                            i++;
                        }
                    }
                }

            }

        }
        //Prints out the grid in the console.
        public int[,] Print_Grid()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Debug.Write($"{field[i, j]}");
                }
                Debug.WriteLine("");
            }
            return field;
        }


        public int[,] Solid_Field()
        {
            int[,] modified_field = new int[18, 10];
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    modified_field[i, j] = field[i, j];
                }
            }
            return modified_field;
        }

        public int[,] original_block(int[,] block, int row, int column)
        {
            int[,] test_block = new int[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    test_block[i, j] = block[i, j];
                }
            }
            return test_block;
        }
        public int [,] Rotate_Left(int[,] block, int [,] test_block, int row, int column)
        {
            int i = 0;
            int j = 0;
            for (int x = column - 1; x >= 0; x--)
            {
                for (int y = 0; y < row; y++)
                {
                    test_block[i, j] = block[y, x]; //copies the content from the original to the new 
                    j++;
                }
                j = 0;
                i++;
            }
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < column; j++)
                {
                    Debug.Write($"{test_block[i, j]}");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("");
            return test_block;
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


        public bool collision(int[,] test_block, int [,] modified_field, int row, int column, int row_counter, int i, int middle)
        {
            int row_walker = row;
            for (int y = 0; y < row_counter+1; y++)
            {
                row_walker--;
                int walker = 0;
                for (int x = 0; x < column; x++)
                {
                    if(middle == 0)
                    {
                        if (modified_field[i - y, middle + walker] == 1 && test_block[row_walker, x] == 1)
                        {
                            return true;
                        }
                    }
                    if (middle>0 && modified_field[i-y, middle - 1 + walker] == 1 && test_block[row_walker, x] == 1)
                    {
                        return true;
                    }
                    walker++;
                } 
            }
            return false;
        }

        bool column_emptyLeft(int[,] block, int column, int middle)
        {
            int count = 0;
            int middle_walker = (middle==0)? 0:1;
            for(int y = 0; y<column; y++)
            {
                if (block[y, middle - middle_walker] == 0)
                {
                    count++;
                    if(count == column - 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void move_left(int[,] block,int[,] modified_field, int column, ref int middle)
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = modified_field[i,j];
                }
            }
            if( middle == 1)
            {
                if(column_emptyLeft(block, column, middle))
                {
                    middle--;
                }
            }
            if(middle> 1)
            {
                middle--;
            }
        }
        public int [,] Falling_Block(int[,] block, int row, int column, int [,] test_block, int[,] modified_field, bool rotate, bool falling, bool overflow, int middle,int i)
        {
            int walker;
            int row_counter = 0;
            int row_remainder = row;
            int counter = 0;
            int middle_walker = 1;

                //Fisrt Insert in the middle
            if (middle == 0)
            {
                middle_walker = 0;
            }
            if (i == 0) //first block ony 
            {
                if (collision(test_block, modified_field, row, column, row_counter, i, middle))
                {
                    return field;
                }
                if (falling)
                {
                    walker = 0;
                    for (int x = 0; x < column; x++)
                    {
                        if (modified_field[i, middle - middle_walker + walker] == 0 && test_block[row - 1, x] == 1)
                        {
                            field[i, middle - middle_walker + walker] = block[row - 1, x];
                        }
                        if (modified_field[i, middle - middle_walker + walker] == 0 && test_block[row - 1, x] == 0)
                        {
                            field[i, middle - middle_walker + walker] = 0;
                        }
                            walker++;
                    }
                }
            }
            if (i > 0)//inseterts each block one by one.
            {
                row_counter = (i < row) ? i : row-1;
                if (collision(test_block, modified_field, row, column, row_counter, i, middle))
                {
                    return field;
                }
                if (falling)
                {
                    for (int y = 0; y < row_counter+1; y++)
                    {
                        walker = 0;
                        for (int x = 0; x < column; x++)
                        {
                            if (modified_field[i - y, middle - middle_walker + walker] == 0 && test_block[row - 1 - y, x] != 0)
                            {
                                field[i - y, middle - middle_walker + walker] = block[row - 1 - y, x];
                            }
                            if (modified_field[i - y, middle - middle_walker + walker] != 0 && test_block[row - 1 - y, x] == 0)
                            {
                                field[i - y, middle - middle_walker + walker] = modified_field[i - y, middle - middle_walker + walker];
                            }
                            if (modified_field[i - y, middle - middle_walker + walker] == 0 && test_block[row - 1 - y, x] == 0)
                            {
                                field[i - y, middle - middle_walker + walker] = 0;
                            }
                            if (i >=row && y == row_counter)
                            {
                                if (modified_field[i - y, middle - middle_walker + walker] == 0 && (test_block[row - 1 - y, x] == 1 || test_block[row - 1 - y, x] == 0))
                                {
                                    field[i - row, middle - middle_walker + walker] = 0;
                                }
                                if (modified_field[i - y, middle - middle_walker + walker] == 1 && test_block[row - 1 - y, x] == 0)
                                {
                                    field[i - row, middle - middle_walker + walker] = modified_field[i - y, middle - middle_walker + walker] ;
                                }
                            }
                            walker++;
                        }
                    }
                }

            }
            /*if (i == 17 && falling)
            {
                Print_Grid();
                Debug.WriteLine("");
                do
                {
                    counter = 0;
                    for (int l = 0; l < column; l++)
                    {
                        if (test_block[row_remainder - 1, l] == 0)
                        {
                            counter++;
                            if (counter == column)
                            {
                                overflow = true;
                                row_remainder--;
                                break;
                            }
                        }
                        if (test_block[row_remainder - 1, l] == 1)
                        {
                            overflow = false;
                            break;
                        }
                    }
                    for (int y = 0; y < row_remainder; y++)
                    {
                        walker = 0;
                        for (int x = 0; x < column; x++)
                        {
                            if (modified_field[i - y, middle - 1 + walker] == 0 && test_block[row - 1 - y, x] == 1)
                            {
                                field[i - y, middle - 1 + walker] = block[row - 1 - y, x];
                            }
                            if (modified_field[i - y, middle - 1 + walker] == 1 && test_block[row - 1 - y, x] == 0)
                            {
                                field[i - y, middle - 1 + walker] = 1;
                            }
                            if (modified_field[i - y, middle - 1 + walker] == 0 && test_block[row - 1 - y, x] == 0)
                            {
                                field[i - y, middle - 1 + walker] = 0;
                            }
                            if (i >= row_remainder)
                            {
                                if (modified_field[i - row_remainder, middle - 1 + walker] == 1 && test_block[row_remainder - 1 - y, x] == 0)
                                {
                                    field[i - row_remainder, middle - 1 + walker] = 1;
                                }
                                if (modified_field[i - row_remainder, middle - 1 + walker] == 0 && test_block[row_remainder - 1 - y, x] == 0)
                                {
                                    field[i - row_remainder, middle - 1 + walker] = 0;
                                }
                            }
                            walker++;
                        }
                    }
                    if (overflow)
                    {
                        Print_Grid();
                        Debug.WriteLine("");
                    }
                } while (overflow) ;
            }*/
            Print_Grid();
            Debug.WriteLine("");
            for (int a = 0; a < row; a++)
            {
                for (int b = 0; b < column; b++)
                {
                    block[a, b] = test_block[a, b];
                }
            }
            return field;
        }
    }
}
