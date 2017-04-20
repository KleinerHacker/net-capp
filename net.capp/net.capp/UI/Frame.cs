using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Type;
using net.capp.Util.Extension;

namespace net.capp.UI
{
    public abstract class Frame : BorderedContainer
    {
        #region Dependency Properties

        public static readonly DependencyProperty IsActivatedProperty = DependencyProperty.Register(
            "IsActivated", typeof(bool), typeof(Frame), new PropertyMetadata(false));

        public bool IsActivated
        {
            get { return (bool) GetValue(IsActivatedProperty); }
            set { SetValue(IsActivatedProperty, value); }
        }

        #endregion

        public event EventHandler Activated, Deactivated;

        protected Frame()
        {
            IsActivatedProperty.AddCallback(GetType(), OnInvalidated);
            IsActivatedProperty.AddCallback(GetType(), (o, args) =>
            {
                if (!ReferenceEquals(o, this))
                    return;

                if ((bool) args.NewValue)
                {
                    Activated?.Invoke(this, new EventArgs());
                }
                else
                {
                    Deactivated?.Invoke(this, new EventArgs());
                }
            });
        }
    }
}