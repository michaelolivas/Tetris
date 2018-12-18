using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Diagnostics;
//using Newtonsoft.Json;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238


namespace TetrisMUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Start : Page
    {

        public Start()
        {
            this.InitializeComponent();
        }


        private void start_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage)); //open the game window
        }

        public void highScore_Click(object sender, RoutedEventArgs e)
        {
            //click = true;
            this.Frame.Navigate(typeof(highScores)); //open the scores window
            
        }
    }
}
