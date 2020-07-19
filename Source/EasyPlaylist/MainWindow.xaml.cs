using System;
using System.Collections.Generic;
using System.Linq;
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
using Core;

namespace EasyPlaylist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RequestSettings _settings;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool emptyTextbox = false;
            ForeachTextbox(x => emptyTextbox = x.Text == "");

            if (emptyTextbox)
            {
                infoBox.Text = "There are empty fields!";
                return;
            }

            infoBox.Text = "Please wait";

            _settings = new RequestSettings(username.Text, password.Password); //update login info

            PlaylistMaker maker;
            try
            {
                maker = new PlaylistMaker(queryList.Text, playlistName.Text, playlistSummary.Text, _settings);
                maker.CreatePlaylist();
            }
            catch (InvalidLoginInfoException)
            {
                infoBox.Text = "Incorrect username or password";
                return;
            }

            infoBox.Text = "Job done";

            url.Text = maker.PlaylistUrl;
            urlGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ForeachTextbox(x => x.Clear());
            password.Clear();
            infoBox.Text = "";
            url.Clear();
            urlGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ForeachTextbox(Action<TextBox> action)
        {
            int count = VisualTreeHelper.GetChildrenCount(mainGrid);

            for (int i = 0; i < count - 1; i++)
                if (VisualTreeHelper.GetChild(mainGrid, i) is TextBox)
                {
                    TextBox textBox = VisualTreeHelper.GetChild(mainGrid, i) as TextBox;
                    action(textBox);
                }
        }
    }
}
