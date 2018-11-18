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

namespace game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Grid bar = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 20;
            one.Width = 20;
            one.Fill = Brushes.Red;
            one.Stroke = Brushes.Black;
            one.Margin = new Thickness(0,-500,0,0);
            GridOne.Children.Add(one);
            

        }
    }
}
