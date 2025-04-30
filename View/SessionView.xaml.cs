using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Draft2.View
{
    /// <summary>
    /// Interaction logic for UpdateView.xaml
    /// </summary>
    public partial class UpdateView : UserControl
    {
        public UpdateView()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Sessions", conn);
                DataTable dataTable = new DataTable();

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
                conn.Close();
                datagrid.ItemsSource = dataTable.DefaultView;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ClearData()
        {
            idTB.Clear();
        }
        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from Sessions where SessionID = " + idTB.Text + "", conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                conn.Close();
                ClearData();
                LoadGrid();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
