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

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Draft2.View
{
    /// <summary>
    /// Interaction logic for MailView.xaml
    /// </summary>
    public partial class MailView : UserControl
    {
        const string accountSid = "AC750584c0993ad00ccbbd753419e6ab76";
        const string authToken = "5d4269a1a29c09f736df2815cb81f2c1";
        const string fromNumber = "+17653458663";
        public MailView()
        {
            InitializeComponent();
            LoadPhoneNumbers();
        }
        

        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedPhoneNumber = PhoneComboBox.SelectedValue?.ToString();
                if (MessageBox1.Text == string.Empty)
                {
                    MessageBox.Show("Message field is empty");

                }

                if (!string.IsNullOrEmpty(selectedPhoneNumber))
                {
                    TwilioClient.Init(accountSid, authToken);
                    var message = MessageResource.Create(
                        body: MessageBox1.Text,
                        from: new Twilio.Types.PhoneNumber("+17653458663"), 
                        to: new Twilio.Types.PhoneNumber(selectedPhoneNumber)     
                    );
                    MessageBox.Show("Message Sent! SID: " + message.Sid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPhoneNumbers()
        {
          
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dilsh\\MVVMLOGINDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            string query = "SELECT Name,Phone FROM Customers";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        PhoneComboBox.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No phone numbers found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading phone numbers: " + ex.Message);
            }
        }
    }
}
