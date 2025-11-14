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
using MusicPlayer.UIComponents.ViewModels;
using WpfApp3;

namespace MusicPlayer.UIComponents
{
    /// <summary>
    /// Interaction logic for CreatePlaylist.xaml
    /// </summary>
    public partial class PlaylistsWindow : Window
    {
        public MainWindowViewModel m_vm;
        public PlaylistsWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void borderWindowMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                this.DragMove();
        }

        private void bdrClose_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void bdrClose_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void bdrClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bdrMinimize_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void bdrMinimize_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void bdrMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bdrResize_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void bdrResize_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void bdrResize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bdrTitleBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
