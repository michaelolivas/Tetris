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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public Grid bar;
        public TransformGroup myTransformGroup;
        public Rectangle one;
        public MainWindow()
        {
            InitializeComponent();

            one = new Rectangle();
            one.Height = 20;
            one.Width = 20;

            //GridOne.Children.Add(one);
            
            bar = create_z();
            GridOne.Children.Add(bar);
            bar.Margin = new Thickness(-25, 75, 0, 0);


            /////////////
            ScaleTransform myScaleTransform = new ScaleTransform();
            myScaleTransform.ScaleY = 1;
            myScaleTransform.ScaleX = 1;

            RotateTransform myRotateTransform = new RotateTransform();
            myRotateTransform.Angle = 0;

            TranslateTransform myTranslate = new TranslateTransform();
            myTranslate.X = 25;
            myTranslate.Y = -25;

            SkewTransform mySkew = new SkewTransform();
            mySkew.AngleX = 0;
            mySkew.AngleY = 0;

            // Create a TransformGroup to contain the transforms 
            // and add the transforms to it. 
            myTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(myScaleTransform);
            myTransformGroup.Children.Add(myRotateTransform);
            myTransformGroup.Children.Add(myTranslate);
            myTransformGroup.Children.Add(mySkew);
            
        }

        /*protected void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DeepSkyBlue, one);
            //Generates the shape            
        }*/
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Debug.WriteLine("You pressed Space");
                bar.RenderTransform = myTransformGroup;
                
            }
        }
        Grid create_z()
        {
            Grid z = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 25;
            one.Width = 25;
            one.Fill = Brushes.LightGreen;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = Brushes.LightGreen;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(-50, 0, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = Brushes.LightGreen;
            three.Stroke = Brushes.Black;
            three.Margin = new Thickness(0, 0, 0, -50);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = Brushes.LightGreen;
            four.Stroke = Brushes.Black;
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
            one.Fill = Brushes.Yellow;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = Brushes.Yellow;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(0, -50, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = Brushes.Yellow;
            three.Stroke = Brushes.Black;
            three.Margin = new Thickness(0, -50, 50, 0);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = Brushes.Yellow;
            four.Stroke = Brushes.Black;
            four.Margin = new Thickness(0, 0, 50,0);

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
            one.Fill = Brushes.Red;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 25;
            two.Width = 25;
            two.Fill = Brushes.Red;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(0, -50, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 25;
            three.Width = 25;
            three.Fill = Brushes.Red;
            three.Stroke = Brushes.Black;
            three.Margin = new Thickness(0, -100, 0, 0);

            Rectangle four = new Rectangle();
            four.Height = 25;
            four.Width = 25;
            four.Fill = Brushes.Red;
            four.Stroke = Brushes.Black;
            four.Margin = new Thickness(0, -150, 0, 0);

            bar.Children.Add(one);
            bar.Children.Add(two);
            bar.Children.Add(three);
            bar.Children.Add(four);

            return bar;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
