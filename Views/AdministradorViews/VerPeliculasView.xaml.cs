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

namespace WatchMe.Views.AdministradorViews
{
    /// <summary>
    /// Lógica de interacción para VerPeliculasView.xaml
    /// </summary>
    public partial class VerPeliculasView : UserControl
    {
        public VerPeliculasView()
        {
            InitializeComponent();
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
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
    }
}
