using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Data.Objects;

namespace MusicPlayer.UIComponents.ViewModels
{
    public class ThemeDesignerViewModel
    {
        public ThemeDesignerViewModel() 
        {
            CustomTheme = new Theme()
            {
                WindowAccent = new ThemeColor(),
                WindowContentBackground = new ThemeColor(),
                WindowTitleForeground = new ThemeColor(),
                ListBoxItemForeground = new ThemeColor(),
                CurrentSongTitleForeground = new ThemeColor(),
                CurrentSongArtistForeground = new ThemeColor(),
                MusicControlBackground = new ThemeColor(),
                TitleBarBackground = new ThemeColor(),
            };
        }

        public Theme CustomTheme { get; set; }  
    }
    
    }
