using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Core;
using net.capp.Type;
using net.capp.Util.Extension;

namespace net.capp.UI
{
    public abstract class BorderedContainer : TitledContainer
    {
        #region Dependency Properties

        public static readonly DependencyProperty BorderProperty = DependencyProperty.Register(
            "Border", typeof(BorderSet), typeof(Frame), new PropertyMetadata(BorderSets.EmptyBorder));

        public BorderSet Border
        {
            get { return (BorderSet) GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }

        #endregion

        protected BorderedContainer()
        {
            BorderProperty.AddCallback(GetType(), OnInvalidated);
            BorderProperty.AddCallback(GetType(), (o, args) =>
            {
                if (!ReferenceEquals(o, this))
                    return;

                Margin = ((BorderSet) args.NewValue).Width;
            });
        }

        internal override ContainerBuffer CreateContainerBuffer()
        {
            var buffer = new ContainerBuffer(Size);
            DrawBorder(buffer);

            if (!string.IsNullOrEmpty(Title))
            {
                DrawTitle(buffer);
            }

            return buffer;
        }

        private void DrawBorder(ContainerBuffer buffer)
        {
            for (var y = 0; y < Size.Height; y++)
            {
                for (var x = 0; x < Size.Width; x++)
                {
                    if (y == 0)
                    {
                        if (x == 0)
                        {
                            buffer[x, y] = new ConsoleElement(Border.TopLeftCorner, ForegroundColor, BackgroundColor);
                        }
                        else if (x == Size.Width - 1)
                        {
                            buffer[x, y] = new ConsoleElement(Border.TopRightCorner, ForegroundColor, BackgroundColor);
                        }
                        else
                        {
                            buffer[x, y] = new ConsoleElement(Border.HorizontalLine, ForegroundColor, BackgroundColor);
                        }
                    }
                    else if (y == Size.Height - 1)
                    {
                        if (x == 0)
                        {
                            buffer[x, y] = new ConsoleElement(Border.BottomLeftCorner, ForegroundColor, BackgroundColor);
                        }
                        else if (x == Size.Width - 1)
                        {
                            buffer[x, y] = new ConsoleElement(Border.BottomRightCorner, ForegroundColor, BackgroundColor);
                        }
                        else
                        {
                            buffer[x, y] = new ConsoleElement(Border.HorizontalLine, ForegroundColor, BackgroundColor);
                        }
                    }
                    else
                    {
                        if (x == 0 || x == Size.Width - 1)
                        {
                            buffer[x, y] = new ConsoleElement(Border.VerticalLine, ForegroundColor, BackgroundColor);
                        }
                        else
                        {
                            buffer[x, y] = new ConsoleElement(' ', ForegroundColor, BackgroundColor);
                        }
                    }
                }
            }
        }

        private void DrawTitle(ContainerBuffer buffer)
        {
            switch (TitleAlignment)
            {
                case TextAlignment.Left:
                    DrawTitleLeft(buffer);
                    break;
                case TextAlignment.Right:
                    DrawTitleRight(buffer);
                    break;
                case TextAlignment.Center:
                    DrawTitleCenter(buffer);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void DrawTitleRight(ContainerBuffer buffer)
        {
            var counter = 0;
            for (var i = Size.Width - 3; i > Math.Max(3, Size.Width - 3 - Title.Length); i++)
            {
                buffer[i, 1] = new ConsoleElement(Title[counter], ForegroundColor, BackgroundColor);
                counter++;
            }
        }

        private void DrawTitleLeft(ContainerBuffer buffer)
        {
            var counter = 0;
            for (var i = 3; i < Math.Min(Size.Width, Title.Length) - 3; i++)
            {
                buffer[i, 1] = new ConsoleElement(Title[counter], ForegroundColor, BackgroundColor);
                counter++;
            }
        }

        private void DrawTitleCenter(ContainerBuffer buffer)
        {
            var start = Math.Max(3, Size.Width / 2 - Title.Length / 2);
            var end = Math.Min(Size.Width - 3, Title.Length);

            var counter = 0;
            for (var i = start; i < end; i++)
            {
                buffer[i, 1] = new ConsoleElement(Title[counter], ForegroundColor, BackgroundColor);
                counter++;
            }
        }
    }
}