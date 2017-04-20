using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net.capp.UI
{
    public abstract class LabeledComponent : ColoredComponent
    {
        #region Dependency Properties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(LabeledComponent), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        protected LabeledComponent()
        {
            TextProperty.AddOwner(GetType(), new PropertyMetadata(OnInvalidated));
        }
    }
}
