using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string GdansksUrl = "https://api.open-meteo.com/v1/forecast?latitude=52.2297&longitude=21.0122&current_weather=true";
        public MainWindow()
        {
            InitializeComponent();

            PokazCzas(null, EventArgs.Empty);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += PokazCzas;
            timer.Start();
        }

        void PokazCzas(object sender, EventArgs e)
        {
            DateTime teraz = DateTime.Now;

            TimeText.Text = teraz.ToString("HH:mm");

            DateText.Text = teraz.ToString(
                "dddd, d MMMM yyyy",
                new CultureInfo("pl-PL")
            );
        }

        private async Task GetWeatherDataAsync()
        {
            string json = await _httpClient.GetStringAsync(GdansksUrl);
            WeatherText.Text = json;
        }
    }
}