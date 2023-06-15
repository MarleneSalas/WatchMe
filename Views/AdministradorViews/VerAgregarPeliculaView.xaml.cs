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

namespace WatchMe.Views.AdministradorViews
{
    /// <summary>
    /// Lógica de interacción para VerAgregarPeliculaView.xaml
    /// </summary>
    public partial class VerAgregarPeliculaView : UserControl
    {
        public VerAgregarPeliculaView()
        {
            InitializeComponent();

           
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            combogenero.SelectedItem = combogenero.Items[1];
        }
    }
}
