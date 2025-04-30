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
using System.Data.SqlClient;
using System.Data;

namespace Draft2.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void CustomerView_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
            DataTable dataTable = new DataTable();
          
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);
            conn.Close();
            datagrid.ItemsSource=dataTable.DefaultView;
        }

        
       

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void clearData()
        {
            idTB.Clear();
        }
        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Customers where ID = " + idTB.Text + "", conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                conn.Close();
                clearData();
                LoadGrid();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted"+ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
