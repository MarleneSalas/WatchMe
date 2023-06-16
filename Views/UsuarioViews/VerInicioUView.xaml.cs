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

namespace WatchMe.Views.UsuarioViews
{
    /// <summary>
    /// Lógica de interacción para VerInicioUView.xaml
    /// </summary>
    public partial class VerInicioUView : UserControl
    {
        public VerInicioUView()
        {
            InitializeComponent();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboGenero.SelectedItem = ComboGenero.Items[1];
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            var item = listBoxItem.DataContext;

            if (item != null)
            {
                if (DataContext is PeliculasViewModel viewModel)
                {
                    viewModel.VerPeliculasCommand.Execute(item);
                }
            }
        }
    }
}
