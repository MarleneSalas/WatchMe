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

namespace WatchMe.Views
{
    /// <summary>
    /// Lógica de interacción para IndexAdministradorView.xaml
    /// </summary>
    public partial class IndexAdministradorView : UserControl
    {
        public IndexAdministradorView()
        {
            InitializeComponent();
        }
        private SolidColorBrush selectedButtonBackground = new SolidColorBrush(Color.FromRgb(41, 47, 51));
        private SolidColorBrush selectedButtonForeground = new SolidColorBrush(Colors.White);
        private SolidColorBrush noselectedButtonBackground = new SolidColorBrush(Colors.Transparent);
     
    }
}
