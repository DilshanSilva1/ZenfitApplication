using System.Configuration;
using System.Data;
using System.Windows;
using Draft2.View;
using Microsoft.VisualBasic.ApplicationServices;



namespace Draft2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, EventArgs e)
        {
           
            var loginView=new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    if (mainView.IsVisible == true && mainView.IsLoaded)
                    {
                        loginView.Close();
                    }
                }
            };
        }
    }

}
