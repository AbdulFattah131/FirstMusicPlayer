using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using MusicPlayer.Data.Objects;
using MusicPlayer.Utility;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel m_vm;
        public MainWindow()
        {
            InitializeComponent();
            m_vm = new MainWindowViewModel();

            this.DataContext = m_vm;
        }
        private void borderWindowMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
                this.DragMove();
        }

        private void grdClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void grdClose_MouseEnter(object sender, MouseEventArgs e)
        {
            grdClose.Background = (m_vm.CurrentTheme.WindowAccent.Brush);
        }

        private void grdClose_MouseLeave(object sender, MouseEventArgs e)
        {
            grdClose.Background = Brushes.Transparent;
        }

        private void grdMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            grdMinimize.Background = Brushes.DarkGray;
        }

        private void grdMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            grdMinimize.Background = Brushes.Transparent;
        }

        private void grdMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void grdResize_MouseEnter(object sender, MouseEventArgs e)
        {
            grdResize.Background = Brushes.DarkGray;
        }

        private void grdResize_MouseLeave(object sender, MouseEventArgs e)
        {
            grdResize.Background = null;
        }

        private void grdResize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                tbResize.Text = "🗖";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                tbResize.Text = "🗗";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            m_vm.Settings.CurrentThemeName = m_vm.CurrentTheme.Name;

            SettingsWriter.Instance.WriteToFile(m_vm.Settings);
        }
    }
}
