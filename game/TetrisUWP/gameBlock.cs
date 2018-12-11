using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TetrisUWP
{
    class gameBlock
    {
        public int num;
        public int x;
        public int y;
        public Rectangle[,] block;
        public gameBlock()
        {
            SolidColorBrush emptyBlockColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 177, 177, 173));
            num = new Random().Next(1, 7);
            Debug.WriteLine(num);
            switch (num)
            {
             //LINE
                case 1: 
                    x = 4;
                    y = 4;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                            //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[0, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    block[2, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    block[3, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    Debug.WriteLine("Line");
                    break;
             //SQUARE
                case 2:
                    x = 2;
                    y = 2;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                           //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[0, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                    block[0, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                    block[1, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                    Debug.WriteLine("Square");
                    break;
             //L
                case 3:
                    x = 3;
                    y = 3;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                            //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[1, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                    block[1, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                    block[2, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                    Debug.WriteLine("L");
                    break;
             //INVERSE L
                case 4:
                    x = 3;
                    y = 3;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                            //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[1, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    block[1, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    block[2, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    Debug.WriteLine("!L");
                    break;
             //T
                case 5:
                    x = 3;
                    y = 3;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                            //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[1, 0].Fill = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
                    block[1, 2].Fill = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
                    block[2, 1].Fill = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
                    Debug.WriteLine("T");
                    break;
             //Z
                case 6:
                    x = 3;
                    y = 3;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                            //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[1, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Pink);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Pink);
                    block[2, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Pink);
                    block[2, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Pink);
                    Debug.WriteLine("Z");
                    break;
             //INVERSE Z
                case 7:
                    x = 3;
                    y = 3;
                    block = new Rectangle[x, y];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            block[i, j] = new Rectangle();
                            block[i, j].Height = 25;
                            block[i, j].Width = 25;
                            block[i, j].Fill = emptyBlockColor;
                            block[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                            //block[i, j].Margin = new Thickness(0, 0, -50 * j, -50 * i);
                        }
                    }
                    block[2, 0].Fill = new SolidColorBrush(Windows.UI.Colors.Purple);
                    block[2, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Purple);
                    block[1, 1].Fill = new SolidColorBrush(Windows.UI.Colors.Purple);
                    block[1, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Purple);
                    Debug.WriteLine("!Z");
                    break;
            }
        }

    }
}
