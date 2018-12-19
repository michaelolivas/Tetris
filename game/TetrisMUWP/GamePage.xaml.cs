using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TetrisMUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
		Game1 _game;
        //Game1 sc = new Game1();
        //int sc;
        
        //string scoreS = "";
        


        public GamePage()
        {
            this.InitializeComponent();
            //sc = Game1.score;
            //sc += 100;
            //showScore.Text = sc.ToString();
            //showScore.UpdateLayout();
            // Create the game.
            //var launchArguments = string.Empty;
            //_game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click.Visibility = Visibility.Collapsed;
            grid.Visibility = Visibility.Collapsed;
            pause.Visibility = Visibility.Visible;

            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }
       
        public void ShowScore_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            //sc = Game1.score;
            //scoreS = sc.scoreString;
            //sc += 100;
            //showScore.Text = sc.ToString();
        }

        private void ShowScore_TextChanged(object sender, TextChangedEventArgs e)
        {
            //sc += 100;
            //showScore.Text = sc.ToString();
        }
    }

}
