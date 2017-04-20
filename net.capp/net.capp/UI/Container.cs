using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Core;
using net.capp.Type;
using net.capp.Util.Extension;

namespace net.capp.UI
{
    public abstract class Container : Component
    {
        #region Dependency Properties

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size", typeof(Dimension), typeof(Container), new PropertyMetadata(default(Dimension)));

        protected static readonly DependencyProperty MarginProperty = DependencyProperty.Register(
            "Margin", typeof(int), typeof(Container), new PropertyMetadata(0));

        protected int Margin
        {
            get { return (int) GetValue(MarginProperty); }
            set { SetValue(MarginProperty, value); }
        }

        public Dimension Size
        {
            get { return (Dimension) GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        #endregion

        public IList<Component> Children { get; } = new BindingList<Component>();

        protected Container()
        {
            ((BindingList<Component>) Children).ListChanged += (sender, args) => RaiseInvalidated();
            SizeProperty.AddCallback(GetType(), OnInvalidated);
            MarginProperty.AddCallback(GetType(), OnInvalidated);
        }

        internal sealed override ComponentBuffer CreateBuffer()
        {
            var containerBuffer = CreateContainerBuffer();
            foreach (var child in Children)
            {
                containerBuffer.OverlayBuffer(child.CreateBuffer(), child.Position.Left + Margin, child.Position.Top + Margin);
            }

            return containerBuffer;
        }

        internal abstract ContainerBuffer CreateContainerBuffer();
    }
}
