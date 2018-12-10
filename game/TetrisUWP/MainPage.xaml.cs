using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using Newtonsoft.Json;
using Windows.UI.Core;
using System.Windows.Input;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TetrisUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SolidColorBrush emptyBlockColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 177, 177, 173));
        Rectangle[,] uiField;
        Rectangle[,] Line;
        Grid currBlock;
        public Grid bar;
        public Grid block;
        public Grid sLine;
        public Rectangle one;
        bool pauseStatus;
        bool resumeStatus;
        //private bool pageFocus = false;

        public MainPage()
        {
            //Start startWindow = new Start(); //pauseMenu is the name of the pauseMenu.xaml file
            //startWindow.Show();
            InitializeComponent();
            game_page.Focus(FocusState.Programmatic);

            //init line obj
            Line = new Rectangle[4, 4];
            for(int x = 0;x < 4; x++)
            {
                for(int y = 0; y < 4; y++)
                {
                    Line[x,y] = new Rectangle();
                    Line[x, y].Height = 25;
                    Line[x, y].Width = 25;
                    Line[x, y].Fill = emptyBlockColor;
                    Line[x, y].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                    Line[x, y].Margin = new Thickness(0, 0, -50 * y, -50 * x);
                    //GameWin.Children.Add(uiField[i, j]);
                }
            }
            Line[0, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            Line[1, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            Line[2, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            Line[3, 2].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);



            //Initialize Game UI Field
            uiField = new Rectangle[18,10];
            for(int i = 0; i < 18; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    uiField[i, j] = new Rectangle();
                    uiField[i, j].Height = 25;
                    uiField[i, j].Width = 25;
                    uiField[i, j].Fill = emptyBlockColor;
                    uiField[i, j].Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                     uiField[i,j].Margin = new Thickness(0, 0, -50*j, -50*i);
                    GameWin.Children.Add(uiField[i, j]);
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
                    if (uiField[i, j].Fill != emptyBlockColor) //Checks if there is a peice of the object on that spot
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
                                        uiField[z, k].Fill = uiField[z - 1, k].Fill;
                                    }
                                    if (z == 0)
                                    {
                                        uiField[z, k].Fill = emptyBlockColor;
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
        public bool collision(Rectangle[,] block, int row, int column, int row_counter, int i, int middle)
        {
            int walker = 0;
            for (int x = 0; x < column; x++)
            {
                if (uiField[i, middle - 1 + walker].Fill != emptyBlockColor && block[row - 1, x].Fill != emptyBlockColor)
                {
                    return true;
                }
                walker++;
            }
            return false;
        }
        //Might not work
        public void combining(Rectangle[,] block, int row, int column, int row_counter, int i, int middle)
        {
            int walker = 0;
            for (int x = 0; x < column; x++)
            {
                block[row - 1, x] = uiField[i, middle - 1 + walker];
                walker++;
            }
        }
        public Rectangle[,] original_block(Rectangle[,] block, int row, int column)
        {
            Rectangle[,] test_block = new Rectangle[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    test_block[i, j] = block[i, j];
                }
            }
            return test_block;
        }
        public void Falling_Block(Rectangle[,] block, int row, int column)
        {
            Rectangle[,] test_block = original_block(block, row, column);
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
                    if (collision(block, row, column, row_counter, i, middle))
                    {
                        falling = false;
                        break;
                    }
                    if (falling)
                    {
                        walker = 0;
                        for (int x = 0; x < column; x++)
                        {
                            if (uiField[i, middle - 1 + walker].Fill == emptyBlockColor && block[row - 1, x].Fill != emptyBlockColor)
                                uiField[i, middle - 1 + walker] = block[row - 1, x];
                            if (uiField[i, middle - 1 + walker].Fill != emptyBlockColor && block[row - 1, x].Fill == emptyBlockColor)
                                block[row - 1, x].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                            walker++;
                        }
                    }
                }
                if (i > 0)//inseterts each block one by one.
                {
                    row_counter = (i < row) ? i : row - 1;
                    if (collision(block, row, column, row_counter, i, middle))
                    {
                        falling = false;
                        break;
                    }
                    if (falling)
                    {
                        for (int y = 0; y < row_counter + 1; y++)
                        {
                            walker = 0;
                            for (int x = 0; x < column; x++)
                            {
                                if (uiField[i - y, middle - 1 + walker].Fill == emptyBlockColor && block[row - 1 - y, x].Fill != emptyBlockColor)
                                    uiField[i - y, middle - 1 + walker] = block[row - 1 - y, x];
                                if (uiField[i - y, middle - 1 + walker].Fill != emptyBlockColor && block[row - 1 - y, x].Fill == emptyBlockColor)
                                    block[row - 1 - y, x].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                                if (i >= row)
                                {
                                    if (uiField[i - row, middle - 1 + walker].Fill != emptyBlockColor && test_block[row - 1 - y, x].Fill == emptyBlockColor)
                                        uiField[i - row, middle - 1 + walker].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                                    if (uiField[i - row, middle - 1 + walker].Fill != emptyBlockColor && test_block[row - 1 - y, x].Fill != emptyBlockColor)
                                        uiField[i - row, middle - 1 + walker].Fill = emptyBlockColor;
                                }

                                walker++;
                            }
                        }
                    }

                }

                if (i == 17)
                {
                    do
                    {
                        counter = 0;
                        for (int l = 0; l < column; l++)
                        {
                            if (block[row - 1, l].Fill == emptyBlockColor)
                            {
                                counter++;
                                if (counter == column)
                                {
                                    overflow = true;
                                    row--;
                                    break;
                                }
                            }
                            if (block[row - 1, l].Fill != emptyBlockColor)
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
                                uiField[i - y, middle - 1 + walker] = block[row - 1 - y, x];
                                if (i >= row)
                                {
                                    if (uiField[i - row, middle - 1 + walker].Fill != emptyBlockColor && test_block[row - 1 - y, x].Fill == emptyBlockColor)
                                        uiField[i - row, middle - 1 + walker].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                                    if (uiField[i - row, middle - 1 + walker].Fill != emptyBlockColor && test_block[row - 1 - y, x].Fill != emptyBlockColor)
                                        uiField[i - row, middle - 1 + walker].Fill = emptyBlockColor;
                                }
                                walker++;
                            }
                        }
                        row_remainder++;
                    } while (overflow);
                }

                for (int a = 0; a < row; a++)
                {
                    for (int b = 0; b < column; b++)
                    {
                        block[a, b] = test_block[a, b];
                    }
                }
                if (!falling)
                    break;
            }
            Check_Line();
        }
        private void start_game()
        {
            Game_Grid Field = new Game_Grid();

            int[,] Line = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            int[,] Box = new int[2, 2] { { 1, 1 }, { 1, 1 } };
            int[,] L = new int[3, 3] { { 0, 1, 1 }, { 0, 0, 1 }, { 0, 0, 1 } };
            int[,] T = new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };

            Debug.WriteLine("");
            Field.Print_Grid();
            Debug.WriteLine("");
            int[,] currField = Field.field;
            Field.Falling_Block(Line, 4, 4);
            Field.Falling_Block(Box, 2, 2);
            Field.Falling_Block(L, 3, 3);
            Field.Falling_Block(T, 3, 3);
        }
        /*private void PageLostFocus(object sender, RoutedEventArgs e)
        {
            game_page.Focus(FocusState.Programmatic);
        }
        void KeyDowns(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.L)
                Debug.WriteLine("Pressed L");
        }*/
        Grid create_z()
        {
            Grid z = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 25;
            one.Width = 25;
            one.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
            one.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
            two.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            two.Margin = new Thickness(-50, 0, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
            three.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            three.Margin = new Thickness(0, 0, 0, -50);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
            four.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            four.Margin = new Thickness(0, 0, -50, -50);

            z.Children.Add(one);
            z.Children.Add(two);
            z.Children.Add(three);
            z.Children.Add(four);
            return z;
        }
        Grid create_square()
        {
            Grid square = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 25;
            one.Width = 25;
            one.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            one.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            one.Margin = new Thickness(0, 0, 0, 0);

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            two.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            two.Margin = new Thickness(0, 0, -50, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            three.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            three.Margin = new Thickness(0, 0, -50, -50);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            four.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            four.Margin = new Thickness(0, 0, 0, -50);

            square.Children.Add(one);
            square.Children.Add(two);
            square.Children.Add(three);
            square.Children.Add(four);
            return square;
        }
        Grid create_bar()
        {
            Grid bar = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 25;
            one.Width = 25;
            one.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            one.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            two.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            two.Margin = new Thickness(0, -50, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            three.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            three.Margin = new Thickness(0, -100, 0, 0);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            four.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            four.Margin = new Thickness(0, -150, 0, 0);

            bar.Children.Add(one);
            bar.Children.Add(two);
            bar.Children.Add(three);
            bar.Children.Add(four);

            return bar;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(pauseMenu)); //opens pause menu page
            Resume.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Collapsed;
            newGame.Visibility = Visibility.Collapsed;
            Quit.Visibility = Visibility.Visible;
            pauseStatus = true;
            resumeStatus = false;
            
        }

        private async void newGame_Click(object sender, RoutedEventArgs e)
        {
            Pause.Visibility = Visibility.Visible;
            newGame.Visibility = Visibility.Collapsed;
            Resume.Visibility = Visibility.Collapsed;
            Quit.Visibility = Visibility.Collapsed;

            Falling_Block(Line, 4, 4);
            //Task t = new Task(start_game);
            //t.Start();
            
            /*
            bar = create_z();
            GameWin.Children.Add(bar);
            int x = 0;
            
            for (x = 0; x >= -800; x--)
            {

                bar.Margin = new Thickness(0, 0, -50, x);
                bar.Visibility = Visibility.Visible;
                await Task.Delay(1000);
                x += -49;
                while (pauseStatus == true)
                {
                    int y = 1;
                    await Task.Delay(y);
                    y++;
                    if (resumeStatus == true)
                    {
                        break;
                    }
                }
            }
            bool barStatus = true;
            if (barStatus == true)
            {
                block = create_square();
                GameWin.Children.Add(block);
                for (int i = 0; i >= -800; i--)
                {
                    block.Margin = new Thickness(0, 0, -50, i);
                    bar.Visibility = Visibility.Visible;
                    await Task.Delay(1000);
                    i += -49;
                }
            }*/
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            Pause.Visibility = Visibility.Visible;
            newGame.Visibility = Visibility.Collapsed;
            Resume.Visibility = Visibility.Collapsed;
            Quit.Visibility = Visibility.Collapsed;
            pauseStatus = false;
            resumeStatus = true;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Pause.Visibility = Visibility.Visible;
            Quit.Visibility = Visibility.Visible;
            newGame.Visibility = Visibility.Collapsed;
            Resume.Visibility = Visibility.Collapsed;
            pauseStatus = false;
            resumeStatus = true;
            Application.Current.Exit(); //closes the whole app
        }
    }

}
