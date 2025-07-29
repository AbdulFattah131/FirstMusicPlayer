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

            m_vm = new ThemeDesignerViewModel();
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
                itbQueryTextBox.BorderBrush = Brushes.Red;
                return;
            }

            if (itbQueryTextBox.Name == "itbName")
            {
              m_vm.CustomTheme.Name = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowAccentHex")
            {
              m_vm.CustomTheme.WindowAccent.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowContentBackgroundHex")
            {
                m_vm.CustomTheme.WindowContentBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbWindowTitleForegroundHex")
            {
                m_vm.CustomTheme.WindowTitleForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbListBoxItemForegroundHex")
            {
                m_vm.CustomTheme.ListBoxItemForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbCurrentSongTitleForegroundHex")
            {
                m_vm.CustomTheme.CurrentSongTitleForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbCurrentSongArtistForegroundHex")
            {
                m_vm.CustomTheme.CurrentSongArtistForeground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbMusicControlBackgroundHex")
            {
                m_vm.CustomTheme.MusicControlBackground.Hex = stQueryText;
            }
            if (itbQueryTextBox.Name == "itbTitleBarBackgroundHex")
            {
                m_vm.CustomTheme.TitleBarBackground.Hex = stQueryText;
            }

            itbQueryTextBox.BorderBrush = Brushes.Gray;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ThemeWriter.Instance.WriteToFile(m_vm.CustomTheme);
            MessageBox.Show("Theme saved!");
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_vm.LoadTheme("mytheme.json");
            MessageBox.Show("Theme loaded!");
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}             
