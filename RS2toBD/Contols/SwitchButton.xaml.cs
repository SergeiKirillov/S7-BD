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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RS2toBD.Contols
{
    /// <summary>
    /// Логика взаимодействия для SwitchButton.xaml
    /// </summary>
    public partial class SwitchButton : UserControl
    {
        
        Thickness LeftSide = new Thickness(-45, 0, 0, 0);
        Thickness RigthtSide = new Thickness(0, 0, -45, 0);
        SolidColorBrush onSwitch = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        SolidColorBrush offSwitch = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        private bool IsSwitching = false;

        public bool IsSwitching1 { get => IsSwitching; set => IsSwitching = value; }

        public SwitchButton()
        {
            InitializeComponent();
            
            IsSwitching = false;
            RectSwitch.Fill = offSwitch;
            CirclSwitch.Margin = LeftSide;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsSwitching)
            {
                IsSwitching = true;
                RectSwitch.Fill = onSwitch;
                CirclSwitch.Margin = RigthtSide;
            }
            else
            {
                IsSwitching = false;
                RectSwitch.Fill = offSwitch;
                CirclSwitch.Margin = LeftSide;
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsSwitching)
            {
                IsSwitching = true;
                RectSwitch.Fill = onSwitch;
                CirclSwitch.Margin = RigthtSide;
            }
            else
            {
                IsSwitching = false;
                RectSwitch.Fill = offSwitch;
                CirclSwitch.Margin = LeftSide;
            }
        }
    }
}
