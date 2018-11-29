using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using game;

namespace game
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private static System.Timers.Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();
            /* Grid bar = new Grid();

             Rectangle one = new Rectangle();
             one.Height = 20;
             one.Width = 20;
             one.Fill = Brushes.Red;
             one.Stroke = Brushes.Black;
             one.Margin = new Thickness(0,-500,0,0);

             GridOne.Children.Add(one);*/
            Grid bar = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 20;
            one.Width = 20;
            one.Fill = Brushes.Red;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 20;
            two.Width = 20;
            two.Fill = Brushes.Red;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(0,-40,0,0);
            bar.Children.Add(one);
            bar.Children.Add(two);
            GridOne.Children.Add(bar);
            //aTimer = new System.Timers.Timer(5000);
            //aTimer.Start();
            bar.Margin = new Thickness(0, 120, 0, 0);
            bar.TranslatePoint(new Point(40,40),bar);
            
            
            //Test leo
            Game_Grid Prime = new Game_Grid();
            Prime.Print_Grid();
            Debug.WriteLine("");
            int[,] Line = new int[4, 1];
            int[,] Flat_Line = new int[1, 4];
            int[,] Square = new int[2, 2] { { 1, 1 }, { 1, 1 } };

            for (int i = 0; i < 4; i++)
            {
                Flat_Line[0, i] = 1;
                Debug.Write(Flat_Line[0, i]);
            }
            for (int i = 0; i< 4; i++)
            {
                Line[i, 0] = 1;
                Debug.WriteLine(Line[i,0]);
            }
            Prime.Falling_Block(Square, 2, 2);
            Prime.Falling_Block(Line, 4, 1);
            Prime.Falling_Block(Line, 4, 1);
            Prime.Falling_Block(Flat_Line, 1, 4);
            Prime.Falling_Block(Square, 2, 2);


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {



            


        }

    }

}
