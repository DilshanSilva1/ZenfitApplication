using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Windows.Interop;
using System.Windows.Threading;
using Draft2.View;

namespace Draft2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        bool hidden;
        double panelWidth;
        double mainWidth;
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
            panelWidth=SlidingPanel.Width;
           // mainWidth=ViewPanel.Width;
            
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {

           if (hidden)
            {
                SlidingPanel.Width += 10;
               // ViewPanel.Width -=10;

                if (SlidingPanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
           else
            {
                 SlidingPanel.Width-= 10;
              //  ViewPanel.Width += 10;
                SlidingPanel.HorizontalAlignment= HorizontalAlignment.Right;
                if (SlidingPanel.Width <= 75)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
        private void ControlMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper= new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161,2,0);
        }

        private void ControlMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void fullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }else
            {
                this.WindowState= WindowState.Normal;
            }
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

      

        private void SlidingMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BTNSlidingMenu_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void ProfileWindowBTN_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}