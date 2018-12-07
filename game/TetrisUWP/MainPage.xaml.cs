﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Grid bar;
        public Grid block;
        public Grid sLine;
        public TransformGroup myTransformGroup;
        public Rectangle one;
        bool pauseStatus;
        bool resumeStatus;
        public MainPage()
        {
            //Start startWindow = new Start(); //pauseMenu is the name of the pauseMenu.xaml file
            //startWindow.Show();
            InitializeComponent();

            one = new Rectangle();
            one.Height = 20;
            one.Width = 20;

            //GridOne.Children.Add(one);
            /*
            bar = create_z();
            GameWin.Children.Add(bar);
            bar.Margin = new Thickness(0, 0, -50, 0);
            
            block = create_square();
            GameWin.Children.Add(block);
            block.Margin = new Thickness(0, 0, -200, 0);
            */
            /*
            
            Game_Grid Field = new Game_Grid();

            int[,] Line = new int[4, 4] { { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 } };
            int[,] Box = new int[2, 2] { { 1, 1 }, { 1, 1 } };
            int[,] L = new int[3, 3] { { 0, 1, 1 }, { 0, 0, 1 }, { 0, 0, 1 } };

            Debug.WriteLine("");
            /*for (int i = 0; i < 4; i++)
            {
                Line = Field.Rotate_Right(Line, 4, 4);
            }
            Field.Print_Grid();
            Debug.WriteLine("");
            Field.Falling_Block(Line, 4, 4);
            Field.Falling_Block(Box, 2, 2);
            Field.Falling_Block(L, 3, 3);
            */
        }
        /*private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Debug.WriteLine("You pressed Space");

            }
        }*/
        Grid create_z()
        {
            Grid z = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 25;
            one.Width = 25;
            one.Fill = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            one.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            two.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            two.Margin = new Thickness(-50, 0, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            three.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            three.Margin = new Thickness(0, 0, 0, -50);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = new SolidColorBrush(Windows.UI.Colors.LightGreen);
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

            //
            Game_Grid Field = new Game_Grid();

            int[,] Line = new int[4, 4] { { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 } };
            int[,] Box = new int[2, 2] { { 1, 1 }, { 1, 1 } };
            int[,] L = new int[3, 3] { { 0, 1, 1 }, { 0, 0, 1 }, { 0, 0, 1 } };

            Debug.WriteLine("");
            for (int i = 0; i < 4; i++)
            {
                Line = Field.Rotate_Right(Line, 4, 4);
            }
            Field.Print_Grid();
            Debug.WriteLine("");
            Field.Falling_Block(Line, 4, 4);
            Field.Falling_Block(Box, 2, 2);
            Field.Falling_Block(L, 3, 3);

            //

            /*
            Game_Grid Field = new Game_Grid();
            Field.Print_Grid();

            int[,] Line = new int[4, 1] { { 1 }, { 1 }, { 1 }, { 1 } };
            Field.Falling_Block(Line, 4, 1);
            */
            /*
            bar = create_z();
            GameWin.Children.Add(bar);
            bar.Margin = new Thickness(0, 0, -50, 0);
            
            block = create_square();
            GameWin.Children.Add(block);
            block.Margin = new Thickness(0, 0, -200, 0);
            */

            bar = create_z();
            GameWin.Children.Add(bar);
            /*
            block = create_square();
            GameWin.Children.Add(block);
            */
            int x = 0;
            
            for (x = 0; x >= -800; x--)
            {/*
                Game_Grid Field = new Game_Grid();
                Field.Print_Grid();
              
                int[,] Line = new int[4, 1] { { 1 }, { 1 }, { 1 }, { 1 } };
                sLine = create_bar();
                GameWin.Children.Add(sLine);

                await System.Threading.Tasks.Task.Delay(1000);
                
                Field.Falling_Block(Line, 4, 1);
                */

                bar.Margin = new Thickness(0, 0, -50, x);
                bar.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Delay(1000);
                x += -49;
                while (pauseStatus == true)
                {
                    int y = 1;
                    await System.Threading.Tasks.Task.Delay(y);
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
                    await System.Threading.Tasks.Task.Delay(1000);
                    i += -49;
                }
            }
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
