using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net.capp.UI
{
    public abstract class Node : Freezable
    {
        public event EventHandler Invalidated;

        protected sealed override Freezable CreateInstanceCore()
        {
            return (Freezable)GetType().GetConstructor(new System.Type[0]).Invoke(this, new object[0]);
        }

        protected void OnInvalidated(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (!ReferenceEquals(o, this))
                return;

            RaiseInvalidated();
        }

        protected void RaiseInvalidated()
        {
            Invalidated?.Invoke(this, new EventArgs());
        }
    }
}
