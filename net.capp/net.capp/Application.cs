using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using net.capp.Core;
using net.capp.Type;
using net.capp.UI;

namespace net.capp
{
    public class Application : Freezable
    {
        public static Application Current { get; private set; } = null;

        #region Dependency Properties

        #region Style

        public static readonly DependencyProperty MenuBarColorProperty = DependencyProperty.Register(
            "MenuBarColor", typeof(ConsoleColor), typeof(Application), new PropertyMetadata(ConsoleColor.DarkGray));

        public static readonly DependencyProperty StatusBarColorProperty = DependencyProperty.Register(
            "StatusBarColor", typeof(ConsoleColor), typeof(Application), new PropertyMetadata(ConsoleColor.DarkGray));

        public static readonly DependencyProperty BackgroundElementProperty = DependencyProperty.Register(
            "BackgroundElement", typeof(ConsoleElement), typeof(Application), new PropertyMetadata(new ConsoleElement('░', ConsoleColor.DarkBlue, ConsoleColor.DarkGray)));

        public ConsoleElement BackgroundElement
        {
            get { return (ConsoleElement) GetValue(BackgroundElementProperty); }
            set { SetValue(BackgroundElementProperty, value); }
        }

        public ConsoleColor StatusBarColor
        {
            get { return (ConsoleColor) GetValue(StatusBarColorProperty); }
            set { SetValue(StatusBarColorProperty, value); }
        }

        public ConsoleColor MenuBarColor
        {
            get { return (ConsoleColor) GetValue(MenuBarColorProperty); }
            set { SetValue(MenuBarColorProperty, value); }
        }

        #endregion

        #region Configuration

        public static readonly DependencyProperty BufferSizeProperty = DependencyProperty.Register(
            "BufferSize", typeof(Dimension), typeof(Application), new PropertyMetadata(new Dimension(80, 25)));

        public static readonly DependencyProperty ConsoleSizeProperty = DependencyProperty.Register(
            "ConsoleSize", typeof(Dimension), typeof(Application), new PropertyMetadata(new Dimension(80, 25)));

        public static readonly DependencyProperty ConsolePositionProperty = DependencyProperty.Register(
            "ConsolePosition", typeof(Origin), typeof(Application), new PropertyMetadata(new Origin(0, 0)));

        public static readonly DependencyProperty ConsoleTitleProperty = DependencyProperty.Register(
            "ConsoleTitle", typeof(string), typeof(Application), new PropertyMetadata("No Title"));

        public string ConsoleTitle
        {
            get { return (string) GetValue(ConsoleTitleProperty); }
            set { SetValue(ConsoleTitleProperty, value); }
        }

        public Origin ConsolePosition
        {
            get { return (Origin) GetValue(ConsolePositionProperty); }
            set { SetValue(ConsolePositionProperty, value); }
        }

        public Dimension ConsoleSize
        {
            get { return (Dimension) GetValue(ConsoleSizeProperty); }
            set { SetValue(ConsoleSizeProperty, value); }
        }

        public Dimension BufferSize
        {
            get { return (Dimension) GetValue(BufferSizeProperty); }
            set { SetValue(BufferSizeProperty, value); }
        }

        #endregion

        #endregion

        internal Renderer Renderer { get; } = new Renderer();

        public Application()
        {
            //Initialize
            Console.CursorVisible = false;
            Console.Title = ConsoleTitle;
            UpdateConsoleConfiguration();

            ConsoleTitleProperty.AddOwner(GetType(), new PropertyMetadata((o, args) =>
            {
                if (!ReferenceEquals(o, this))
                    return;

                Console.Title = (string) args.NewValue;
            }));
            BufferSizeProperty.AddOwner(GetType(), new PropertyMetadata(OnUpdateConsoleConfiguration));
            ConsolePositionProperty.AddOwner(GetType(), new PropertyMetadata(OnUpdateConsoleConfiguration));
            ConsoleSizeProperty.AddOwner(GetType(), new PropertyMetadata(OnUpdateConsoleConfiguration));

            for (var y = 0; y < BufferSize.Height; y++)
            {
                for (var x = 0; x < BufferSize.Width; x++)
                {
                    if (y == 0)
                    {
                        Renderer.Buffer[x, y] = new ConsoleElement(' ', ConsoleColor.White, MenuBarColor);
                    }
                    else if (y == BufferSize.Height - 1)
                    {
                        Renderer.Buffer[x, y] = new ConsoleElement(' ', ConsoleColor.White, StatusBarColor);
                    }
                    else
                    {
                        Renderer.Buffer[x, y] = BackgroundElement;
                    }
                }
            }

            Current = this;
        }

        public void Startup(string[] args)
        {
            Render();
        }

        private void Render()
        {
            var window = new Window {Size = new Dimension(50, 20), Position = new Origin(10, 1), Title = "Test Window"};
            window.Children.Add(new Label {Text = "Hallo Welt", Position = new Origin(1, 1)});

            Renderer.Buffer.OverlayBuffer(window.CreateBuffer(), window.Position.Left, window.Position.Top);
            Renderer.Render();

            Console.ReadLine();
        }

        #region Handler Methods

        private void OnUpdateConsoleConfiguration(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (!ReferenceEquals(o, this))
                return;

            UpdateConsoleConfiguration();
        }

        private void UpdateConsoleConfiguration()
        {
            Console.SetBufferSize(BufferSize.Width, BufferSize.Height);
            Console.SetWindowSize(ConsoleSize.Width, ConsoleSize.Height);
            Console.SetWindowPosition(ConsolePosition.Left, ConsolePosition.Top);

            Renderer.Buffer.Size = BufferSize;
        }

        #endregion

        protected override Freezable CreateInstanceCore()
        {
            return new Application();
        }
    }
}