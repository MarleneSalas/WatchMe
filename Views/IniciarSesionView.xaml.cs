﻿using System;
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
    /// Lógica de interacción para IniciarSesionView.xaml
    /// </summary>
    public partial class IniciarSesionView : UserControl
    {
        public IniciarSesionView()
        {
            InitializeComponent();
        }

        private void pwb_LostFocus(object sender, RoutedEventArgs e)
        {
            txtPassword1.Text = "";
            txtPassword1.Text = pwb.Password;
        }

        private void pwb_Loaded(object sender, RoutedEventArgs e)
        {
            pwb.Clear();
        }
    }
}
