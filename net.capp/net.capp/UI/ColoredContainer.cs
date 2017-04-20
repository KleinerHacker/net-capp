using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net.capp.UI
{
    public abstract class ColoredContainer : Container
    {
        #region Dependency Properties

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            "BackgroundColor", typeof(ConsoleColor), typeof(ColoredContainer), new PropertyMetadata(default(ConsoleColor)));

        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register(
            "ForegroundColor", typeof(ConsoleColor), typeof(ColoredContainer), new PropertyMetadata(default(ConsoleColor)));

        public ConsoleColor ForegroundColor
        {
            get { return (ConsoleColor)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public ConsoleColor BackgroundColor
        {
            get { return (ConsoleColor)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        #endregion

        protected ColoredContainer()
        {
            ForegroundColorProperty.AddOwner(GetType(), new PropertyMetadata(OnInvalidated));
            BackgroundColorProperty.AddOwner(GetType(), new PropertyMetadata(OnInvalidated));
        }
    }
}
