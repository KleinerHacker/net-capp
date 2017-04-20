using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net.capp.UI
{
    public class Menu : MenuItem
    {
        public IList<MenuItem> Items { get; } = new BindingList<MenuItem>();

        public Menu()
        {
            ((BindingList<MenuItem>) Items).ListChanged += (sender, args) => RaiseInvalidated();
        }
    }
}
