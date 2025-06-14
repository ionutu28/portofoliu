using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tetris_
{
   
    public partial class Opt : Window
    {
        public Opt()
        {

            InitializeComponent();
            on.Visibility = Visibility.Hidden;
            off.Visibility = Visibility.Visible;
        }
        
        private void Button_Home(object sender, RoutedEventArgs e)
        {
            MainWindow meniu = new MainWindow();
            meniu.Show();
            Hide();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        MediaPlayer tet = new MediaPlayer();
        string nume;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                DefaultExt = ".mp3",
                InitialDirectory = @"Tetris_\Tetris_\Meniu\"
            };
            bool? dialogOk = fileDialog.ShowDialog();
            if (dialogOk==true)
            {
                nume = fileDialog.FileName;
                tet.Open(new Uri(nume));
            }
            tet.Play();
            tet.Volume = 0;
            tet.MediaEnded += new EventHandler(gata);
        }

        private void gata(object sender, EventArgs e)
        {
            tet.Position = TimeSpan.Zero;
            tet.Play();
            
        }

        private void Button_Stopsound(object sender, RoutedEventArgs e)
        {
            tet.Stop();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double nr = 0;
            if (IsLoaded)
                nr = sld.Value;
            tet.Volume = nr/5+0.002;
            if(sld.Value==0.00)
            {
                on.Visibility = Visibility.Hidden;
                off.Visibility = Visibility.Visible;
                
            }
            else
            {
                off.Visibility = Visibility.Hidden;
                on.Visibility = Visibility.Visible;
            }
        }
    }
}
