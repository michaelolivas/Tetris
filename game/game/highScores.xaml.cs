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
using System.Windows.Shapes;

namespace game
{
    /// <summary>
    /// Interaction logic for highScores.xaml
    /// </summary>
    public partial class highScores : Window
    {
        public highScores()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Start goBack = new Start();
            goBack.Show();
            this.Close();
        }
    }
}
