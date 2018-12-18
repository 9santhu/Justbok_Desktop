using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Models
{
    public class Node : INotifyPropertyChanged
    {

        private int _id;
        private string _name;
        private bool _isSelected;

        public int Id { get { return _id; } set { _id = value;RaisePropertyChanged("Id"); } }
        public string Name { get { return _name; } set { _name = value;RaisePropertyChanged("Name"); } }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value;RaisePropertyChanged("IsSelected"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}