using System;
using System.Windows;

namespace CustomWpfCalendar
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _calendarView.DateTime = _calendarView.DateTime.AddMonths(-1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _calendarView.DateTime = _calendarView.DateTime.AddMonths(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _calendarView.DateTime = DateTime.Now;
        }
    }
}
