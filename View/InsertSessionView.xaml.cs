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
    /// Interaction logic for DeleteView.xaml
    /// </summary>
    public partial class DeleteView : UserControl
    {
        public DeleteView()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public bool isValid()
        {

            if (idTB.Text == string.Empty)
            {
                MessageBox.Show("Name field is empty");
                return false;
            }
            if (weightTB.Text == string.Empty)
            {
                MessageBox.Show("phone field is empty");
                return false;
            }
            if (notesTB.Text == string.Empty)
            {
                MessageBox.Show("email field is empty");
                return false;
            }
           

            return true;
        }
        private void ClearData()
        {

            idTB.Clear();
            weightTB.Clear();
            notesTB.Clear();
           
        }


        private void addClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Sessions Values (@CustomerID,@CurrentWeight,@SessionDate,@Notes)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CustomerID", idTB.Text);
                    cmd.Parameters.AddWithValue("@CurrentWeight", weightTB.Text);
                    cmd.Parameters.AddWithValue("@SessionDate",DateTime.Today);
                    cmd.Parameters.AddWithValue("@Notes", notesTB.Text);

                    conn.Open();
                    
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Updated!");
                    ClearData();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearCust(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }
}
