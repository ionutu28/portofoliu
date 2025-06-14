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

namespace Tetris_
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


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Jocul joc = new Jocul();
            joc.Show();
            Close();
        }


        private void Button_Opt(object sender, RoutedEventArgs e)
        {
            Opt optiuni = new Opt();
            optiuni.Show();
            Close();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);
        }
    }
}
