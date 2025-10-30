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
using MusicPlayer.Data.Objects;
using MusicPlayer.UIComponents.ViewModels;
using MusicPlayer.Utility;
using WpfApp3;

namespace MusicPlayer.UIComponents
{
    /// <summary>
    /// Interaction logic for ThemeDesigner.xaml
    /// </summary>
    public partial class ThemeDesigner : Window
    {

        private ThemeDesignerViewModel m_vm;

        public ThemeDesigner()
        {
            InitializeComponent();
            var mainWindowVM = new MainWindowViewModel();
            m_vm = new ThemeDesignerViewModel(mainWindowVM);
            this.DataContext = m_vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemeWriter.Instance.WriteToFile(m_vm.CustomTheme);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e == null || sender == null)
                return;

            TextBox itbQueryTextBox = sender as TextBox;
            string stQueryText = itbQueryTextBox.Text;
            Brush tempBrush = null;

            try
            {
                tempBrush = (SolidColorBrush)(new BrushConverter().ConvertFromString(stQueryText));
            }
            catch (Exception ex)
            {
                itbQueryTextBox.BorderBrush = Brushes.DarkGray;
                return;
            }

            if (itbQueryTextBox.Name == "itbName")
            {
                m_vm.CustomTheme.Name = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowTextForegroundHex")
            {
                m_vm.CustomTheme.WindowTextForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowHeadingForegroundHex")
            {
                m_vm.CustomTheme.WindowHeadingForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowPrimaryAnchorHex")
            {
                m_vm.CustomTheme.WindowPrimaryAnchor.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbSearchBoxBackgroundHex")
            {
                m_vm.CustomTheme.SearchBoxBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowPrimarySliderHex")
            {
                m_vm.CustomTheme.WindowPrimarySlider.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowPrimaryHex")
            {
                m_vm.CustomTheme.WindowPrimary.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowTitleForegroundHex")
            {
                m_vm.CustomTheme.WindowTitleForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowContentBackgroundHex")
            {
                m_vm.CustomTheme.WindowContentBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbComplementGrayHex")
            {
                m_vm.CustomTheme.ComplementGray.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbCurrentSongArtistForegroundHex")
            {
                m_vm.CustomTheme.CurrentSongArtistForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbPlayerControlBackgroundHex")
            {
                m_vm.CustomTheme.PlayerControlBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbTitleBarBackgroundHex")
            {
                m_vm.CustomTheme.TitleBarBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbListBoxHoverHex")
            {
                m_vm.CustomTheme.ListBoxHover.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbListBoxSelectedHex")
            {
                m_vm.CustomTheme.ListBoxSelected.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbLeftToggleHoverHex")
            {
                m_vm.CustomTheme.LeftToggleHover.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbLeftToggleSelectedHex")
            {
                m_vm.CustomTheme.LeftToggleSelected.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbPlayPauseBackgroundHex")
            {
                m_vm.CustomTheme.PlayPauseBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbGenresTextForegroundHex")
            {
                m_vm.CustomTheme.GenresTextForeground.Hex = stQueryText;
            }

            itbQueryTextBox.BorderBrush = Brushes.Gray;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ThemeWriter.Instance.WriteToFile(m_vm.CustomTheme);
            MessageBox.Show("Theme saved!");
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            m_vm.CustomTheme = (Theme)(cbThemes.SelectedItem);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            var m_vm = (ThemeDesignerViewModel)this.DataContext;

            if (cbThemes.SelectedItem is Theme selectedTheme)
            {
                MainWindowViewModel.Instance.CurrentTheme = selectedTheme;
            }
            else
            {
                MainWindowViewModel.Instance.CurrentTheme = m_vm.CustomTheme;
            }

            // Hide Switch

            //var mainWindow = Application.Current.MainWindow as MainWindow;
            //if (mainWindow != null)
            //{
            //    mainWindow.tbSwitchTheme.Visibility = Visibility.Collapsed;
            //}
        }
    }
}
