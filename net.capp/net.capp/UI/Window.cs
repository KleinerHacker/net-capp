using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Core;
using net.capp.Type;

namespace net.capp.UI
{
    public class Window : Frame
    {
        public IList<Menu> Menus { get; } = new BindingList<Menu>();

        public Window()
        {
            Border = BorderSets.DoubleLineBorder;
            ForegroundColor = ConsoleColor.Yellow;
            BackgroundColor = ConsoleColor.DarkBlue;

            ((BindingList<Menu>) Menus).ListChanged += (sender, args) => RaiseInvalidated();
        }
    }
}