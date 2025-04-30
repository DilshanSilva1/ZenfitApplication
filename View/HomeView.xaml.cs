using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
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
using Draft2.Model;
using LiveCharts.Wpf;
using LiveCharts;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using static System.Collections.Specialized.BitVector32;


namespace Draft2.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private List<int> ids = new List<int>();
        private int currentIndex = 0; string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";




        public HomeView()
        {
            InitializeComponent();
            LoadCustomerCount();
            LoadSessCount();
            Loadids();
            DisplayBMI();
            DisplayMonthlyIncome();
            var val1List = new ChartValues<double>();
            var val2List = new ChartValues<double>();
            var categories = new List<string>();


;
            string query = "SELECT Name,Height,Weight FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(reader["Name"].ToString());

                    decimal val1 = (decimal)reader["Height"];
                    decimal val2 = (decimal)reader["Weight"];

                    val1List.Add((double)val1);
                    val2List.Add((double)val2);
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title= "Height in CM",
                    Values=val1List,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9800")) 
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Weight in KG",
                Values = val2List,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1800")) 
            });
            BarLabels = categories.ToArray();
            Formatter = value => value.ToString("N");

            DataContext = this;
            int barCount = BarLabels.Length;
            int barWidth = 120;
            mychart.Width = barCount * barWidth;
        }
        public SeriesCollection SeriesCollection { get; private set; }
        public string[] BarLabels { get; private set; }
        public Func<double, string> Formatter { get; private set; }

        private void LoadCustomerCount()
        {
            
            string connectionString ="Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            string query = "SELECT COUNT(ID) FROM Customers";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    CustLabel.Content = $"{count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LoadSessCount()
        {

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            string query = "SELECT COUNT(SessionID) FROM Sessions";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    SessLabel.Content = $"{count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Loadids()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT ID FROM Customers ORDER BY ID", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }
        private void DisplayBMI()
        {
            if (ids.Count == 0) return;

            int id = ids[currentIndex];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Name, Height, Weight FROM Customers WHERE ID = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {   
                    String name=reader.GetString(0);
                    decimal height = reader.GetDecimal(1); // height in cm
                    decimal weight = reader.GetDecimal(2); // weight in kg

                    decimal heightInMeters = height / 100m;
                    decimal bmi = heightInMeters > 0 ? weight / (heightInMeters * heightInMeters) : 0;

                    BMILabel.Content = $"{name}, BMI: {bmi:F2}";

                }
            }
        }
        private void DisplayMonthlyIncome()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            string query = "SELECT SUM(monthly_income) FROM Customers";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    decimal count = (decimal)command.ExecuteScalar();
                    MonthlyLabel.Content = $"{count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayBMI();
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < ids.Count - 1)
            {
                currentIndex++;
                DisplayBMI();
            }
        }
    }
}
