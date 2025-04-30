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
    /// Interaction logic for InsertView.xaml
    /// </summary>
    public partial class InsertView : UserControl
    {
        public InsertView()
        {
            InitializeComponent();
        }
        
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public bool isValid()
        {
            
            if (nameTB.Text == string.Empty)
            {
                MessageBox.Show("Name field is empty");
                return false;
            }
            if (phoneTB.Text == string.Empty)
            {
                MessageBox.Show("phone field is empty");
                return false;
            }
            if (emailTB.Text == string.Empty)
            {
                MessageBox.Show("email field is empty");
                return false;
            }
            if (heightTB.Text == string.Empty)
            {
                MessageBox.Show("height field is empty");
                return false;
            }
            if (weightTB.Text == string.Empty)
            {
                MessageBox.Show("weight field is empty");
                return false;
            }
            if (monthlyTB.Text == string.Empty)
            {
                MessageBox.Show("income field is empty");
                return false;
            }

            return true;
        }
        private void ClearData()
        {
            
            nameTB.Clear();
            phoneTB.Clear();
            emailTB.Clear();
            heightTB.Clear();
            weightTB.Clear();
            monthlyTB.Clear();
            updateTB.Clear();
        }
      

        private void addClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Customers Values (@Name,@Phone,@Email,@Height,@Weight,@DateJoined,@monthly_income)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", nameTB.Text);
                    cmd.Parameters.AddWithValue("@Phone", phoneTB.Text);
                    cmd.Parameters.AddWithValue("@Email", emailTB.Text);
                    cmd.Parameters.AddWithValue("@Height", heightTB.Text);
                    cmd.Parameters.AddWithValue("@Weight", weightTB.Text);
                    cmd.Parameters.AddWithValue("@DateJoined", DateTime.Now);
                    cmd.Parameters.AddWithValue("@monthly_income", monthlyTB.Text);
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

        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();    
            SqlCommand cmd = new SqlCommand("Update Customers set Name = '"+nameTB.Text+"',Phone = '"+phoneTB.Text+"',Email ='"+emailTB.Text+"', Height = '"+heightTB.Text+"',Weight = '"+weightTB.Text+"',monthly_income = '"+monthlyTB.Text+"'WHERE ID ='"+updateTB.Text+"' ",conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated!");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }finally { conn.Close();ClearData(); }  
                
        }

        
    }
}
