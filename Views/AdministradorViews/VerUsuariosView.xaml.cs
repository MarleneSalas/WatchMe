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
    /// Lógica de interacción para VerUsuariosView.xaml
    /// </summary>
    public partial class VerUsuariosView : UserControl
    {
        public VerUsuariosView()
        {
            InitializeComponent();
        }

        private void ContentPresenter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ContentPresenter contentpresenterItem = sender as ContentPresenter;
            var item = contentpresenterItem.DataContext;

            if (item != null)
            {
                if (DataContext is PeliculasViewModel viewModel)
                {
                    viewModel.VerEliminarUsuarioCommand.Execute(item);
                }
            }
        }
    }
}
