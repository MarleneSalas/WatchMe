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
    /// Lógica de interacción para VerInicioView.xaml
    /// </summary>
    public partial class VerInicioView : UserControl
    {
        public VerInicioView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HayResultados.Visibility = Visibility.Collapsed;
            txtBuscar.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NoHayResultados.Visibility=Visibility.Collapsed;
            txtBuscar.Clear();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboGenero.SelectedItem = ComboGenero.Items[1];
        }
    }
}
