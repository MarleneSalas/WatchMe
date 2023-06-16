using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchMe.ViewModels
{
    public class CombinadosViewModel:INotifyPropertyChanged
    {
        public PeliculasViewModel peliculasvm { get; set; }
        public ReseñasViewModel reseñasvm { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
