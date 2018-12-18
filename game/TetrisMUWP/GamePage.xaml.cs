using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TetrisMUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
		Game1 _game;

        public GamePage()
        {
            this.InitializeComponent();

            // Create the game.
            //var launchArguments = string.Empty;
            //_game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click.Visibility = Visibility.Collapsed;
            grid.Visibility = Visibility.Collapsed;
            pause.Visibility = Visibility.Visible;
            pauseB.Visibility = Visibility.Visible;

            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }

        private void PauseB_Click(object sender, RoutedEventArgs e)
        {
            pauseB.Visibility = Visibility.Collapsed;
            resume.Visibility = Visibility.Visible;
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            pauseB.Visibility = Visibility.Visible;
            resume.Visibility = Visibility.Collapsed;
        }
    }
}
