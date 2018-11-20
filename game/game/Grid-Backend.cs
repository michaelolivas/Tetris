using System;
using System.Diagnostics;

namespace game
{
    public class Game_Grid
    {
        private int[,] field;


        public Game_Grid()
        {
            int[,] field = new int[18,10];
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
            int[,] temp_block = new int[column, row];
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    temp_block[j,i] = block[i,j];
                }
            }
            return temp_block;
        }

        public void Print_Grid()
        {
            for(int i=0; i<18; i++)
            {
                for(int j=0; j<10; j++)
                {
                    Debug.WriteLine($"", field[i,j]);
                }
            }
        }

        public void Falling_Block(int[,] block, int row, int column)
        {
            bool falling = true;
            while (falling)
            {
                int middle = 4;
                for (int i = 0; i < 18; i++)
                {
                    //Fisrt Insert in the middle
                    if (column == 1)//straight line only
                    {

                        if (i == 0)
                        {
                            field[i, middle] = block[row - 1, 0];

                        }
                        if (i > 0)
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
                        //checks if there is anywhere else to go
                        if (field[i + 1, middle] == 1)
                        {
                            //neeeds to add a small delay in oder to move last minute
                            falling = false;
                            break;
                        }
                        else
                        {
                            field[i - 4, middle] = 0;
                            field[i, middle] = block[row - 1, 0];
                        }
                    }
                    if(column == 2 && row == 2)//square only 
                    {
                        if(i == 0)
                        {
                            field[i, middle] = block[1, 0];
                            field[i, middle + 1] = block[1, 0];
                        }
                        if(i > 0)
                        {
                            field[i - 1, middle] = block[0, 0];
                            field[i - 1, middle + 1] = block[0, 1];
                            field[i, middle] = block[1, 0];
                            field[i, middle + 1] = block[1, 1];
                        }
                    }
                    if (column == 4)//straight line only 
                    {
                        if (i == 0)
                        {
                            field[i, middle - 1] = block[0, column - 1];
                            field[i, middle] = block[0, column - 2];
                            field[i, middle + 1] = block[0, column - 3];
                            field[i, middle + 2] = block[0, column - 4];
                        }
                        if (i > 0)
                        {
                            int walker = 1;
                            for (int j = middle-1; j<= middle + 2; j++)
                            {
                                field[i - 1, j] = 0;
                                field[i, j] = block[0, column - walker];
                                walker++;
                            }
                        }
                        if(field[i,middle -1]==1 && field[i, middle]==1 && field[i, middle +1]==1 && field[i, middle + 2]==1)
                        {
                            falling = false;
                            break;
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
                }
            }
        }
    }
}
