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
using WatchMe.Models;
using WatchMe.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ListBoxItem_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            var item = listBoxItem.DataContext;
            var peliculaSeleccionada = listBoxItem.DataContext as Peliculas;

            if (item != null)
            {
                if (DataContext is PeliculasViewModel viewModel)
                {
                    viewModel.VerPeliculaCommand.Execute(peliculaSeleccionada);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txtBuscar.Clear();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            txtBuscar.Clear();
        }
    }
}