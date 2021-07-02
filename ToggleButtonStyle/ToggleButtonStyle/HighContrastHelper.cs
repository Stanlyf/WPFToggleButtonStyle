using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ToggleButtonStyle
{
    public class HighContrastHelper : DependencyObject
    {
        private HighContrastHelper()
        {
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
        }

        ~HighContrastHelper()
        {
            SystemParameters.StaticPropertyChanged -= SystemParameters_StaticPropertyChanged;
        }

        private static HighContrastHelper instance;
        public static HighContrastHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new HighContrastHelper();
                return instance;
            }
        }

        public void ApplyCurrentTheme()
        {
            if (SystemParameters.HighContrast)
            {
                SolidColorBrush windowbrush = SystemColors.WindowBrush;
                if (windowbrush.Color.R == 255 && windowbrush.Color.G == 255 && windowbrush.Color.B == 255)
                {
                    Instance.IsHighContrastWhite = true;
                    Instance.IsHighContrastBlack = false;
                }
                else if (windowbrush.Color.R == 0 && windowbrush.Color.G == 0 && windowbrush.Color.B == 0)
                {
                    Instance.IsHighContrastWhite = false;
                    Instance.IsHighContrastBlack = true;
                }
                else
                {
                    Instance.IsHighContrastWhite = false;
                    Instance.IsHighContrastBlack = false;
                }
            }
        }

        void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HighContrast")
            {
                ApplyCurrentTheme();
            }
        }

        public static readonly DependencyProperty IsHighContrastWhiteProperty = DependencyProperty.Register(
            "IsHighContrastWhite",
            typeof(bool),
            typeof(HighContrastHelper),
            new PropertyMetadata(false));

        public static readonly DependencyProperty IsHighContrastBlackProperty = DependencyProperty.Register(
            "IsHighContrastBlack",
            typeof(bool),
            typeof(HighContrastHelper),
            new PropertyMetadata(false));

        public bool IsHighContrastWhite
        {
            get { return (bool)GetValue(IsHighContrastWhiteProperty); }
            private set { SetValue(IsHighContrastWhiteProperty, value); }
        }

        public bool IsHighContrastBlack
        {
            get { return (bool)GetValue(IsHighContrastBlackProperty); }
            private set { SetValue(IsHighContrastBlackProperty, value); }
        }
    }
}
