using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.MaxWidth = SystemParameters.WorkArea.Width
                + SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowResizeBorderThickness.Right;
            this.MaxHeight = SystemParameters.WorkArea.Height
                + SystemParameters.WindowResizeBorderThickness.Top + SystemParameters.WindowResizeBorderThickness.Bottom;

            if (WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new Thickness(7, 7, 1, 1);
            }

        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new Thickness(7, 7, 1, 1);
            }
            else
            {
                this.BorderThickness = new Thickness(0);
            }
        }

        // 支持标题栏拖动
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            Point position = e.GetPosition(WindowTitleBar);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < WindowTitleBar.ActualWidth && position.Y >= 0 && position.Y < WindowTitleBar.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        protected void NavigationBarClick(object sender, RoutedEventArgs e)
        {
            Control dd = (Control)sender;
            string dfd = dd.ToString();
            Pages.Navigate(new Uri("/" + dd.Tag.ToString() + ".xaml", UriKind.Relative));
            e.Handled = true;
        }

        protected void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        protected void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
            e.Handled = true;
        }

        protected void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }
    }

    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowMaximizedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            WindowState state = (WindowState)value;

            return state == WindowState.Maximized ? Visibility.Hidden : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowNormalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            return state != WindowState.Maximized ? Visibility.Hidden : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowNormalStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            switch (state)
            {
                //case WindowState.Minimized:
                //    return Visibility.Visible;
                case WindowState.Maximized:
                    return Visibility.Hidden;
                case WindowState.Normal:
                    return Visibility.Visible;
                default:
                    return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
