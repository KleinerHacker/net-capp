using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Core;
using net.capp.Type;

namespace net.capp.UI
{
    public abstract class Component : Node
    {
        #region Dependency Properties

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position", typeof(Origin), typeof(Component), new PropertyMetadata(default(Origin)));

        public Origin Position
        {
            get { return (Origin) GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        #endregion

        protected Component()
        {
            PositionProperty.AddOwner(GetType(), new PropertyMetadata(OnInvalidated));
        }

        internal abstract ComponentBuffer CreateBuffer();
    }
}
