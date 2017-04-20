using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Type;

namespace net.capp.UI
{
    public abstract class TitledContainer : ColoredContainer
    {
        #region Dependency Properties

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(TitledContainer), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register(
            "TitleAlignment", typeof(TextAlignment), typeof(TitledContainer), new PropertyMetadata(TextAlignment.Center));

        public TextAlignment TitleAlignment
        {
            get { return (TextAlignment) GetValue(TitleAlignmentProperty); }
            set { SetValue(TitleAlignmentProperty, value); }
        }

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion

        protected TitledContainer()
        {
            TitleProperty.AddOwner(GetType(), new PropertyMetadata(OnInvalidated));
            TitleAlignmentProperty.AddOwner(GetType(), new PropertyMetadata(OnInvalidated));
        }
    }
}
