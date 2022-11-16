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
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using System.Net.Http;
namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        student Student_Current;
        string studentID;
        string Uri = "";
        RestClient client;
        private System.Windows.Threading.DispatcherTimer timer;
        public MainWindow()
        {
            Uri = "https://ais-rest.conveyor.cloud";
            InitializeComponent();
            client = new RestClient(new Uri(Uri));
            MainFrame.Content = new Auth(this, Uri);
          
        }
        private void List_navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainFrame.Content = SinglPage.getInstance(((ListViewItem)List_navigation.SelectedItem).Name, studentID, Uri);
        }
        public void startTime(string id)
        {
            this.studentID = id;
            MainFrame.Content = null;
            ButtonOpen.IsEnabled = true;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(loadAsync);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        private async void loadAsync(object sender, EventArgs e)
        {
            await Task.Run(() => load());
        }
        private void load()
        {     
            var restrequest = new RestRequest("/Students/details/" + studentID, Method.GET);
            var restresponse = client.Execute(restrequest);
            Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate { txt_balance.Text = Student_Current.Currency.ToString(); 
                                                                                   txt_fio.Text = Student_Current.Im +' '+ Student_Current.Fam + ' ' + Student_Current.Otch; });
        }
    }
}
