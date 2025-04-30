using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Draft2.Model;
using Draft2.Repositories;
using FontAwesome.Sharp;

namespace Draft2.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        //FIELDS

        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(_currentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView 
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption 
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon 
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
           
        }
        //commands for user input to control views
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public ICommand ShowInsertViewCommand { get; }

        public ICommand ShowUpdateViewCommand { get; }

        public ICommand ShowDeleteViewCommand { get; }
        public ICommand ShowMailViewCommand { get; }


        public MainViewModel()
        {
            userRepository = new UserRepository();
           
            CurrentUserAccount = new UserAccountModel();
           

            //initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowInsertViewCommand = new ViewModelCommand(ExecuteShowInsertViewCommand);
            ShowUpdateViewCommand = new ViewModelCommand(ExecuteShowUpdateViewCommand);
            ShowDeleteViewCommand = new ViewModelCommand(ExecuteShowDeleteViewCommand);
            ShowMailViewCommand = new ViewModelCommand(ExecuteShowMailViewCommand);

            //default view
            ExecuteShowHomeViewCommand(null);
            LoadCurrentUserData();
        }

        private void ExecuteShowDeleteViewCommand(object obj)
        {
            CurrentChildView = new DeleteViewModel();
            Caption = "Add Session";
            Icon = IconChar.ArrowRightToFile;
        }

        private void ExecuteShowUpdateViewCommand(object obj)
        {
            CurrentChildView = new UpdateViewModel();
            Caption = "Session";
            Icon = IconChar.BookAtlas;
        }

        private void ExecuteShowInsertViewCommand(object obj)
        {
            CurrentChildView = new InsertViewModel();
            Caption = "Add Customer";
            Icon = IconChar.UserEdit;
        }
        private void ExecuteShowMailViewCommand(object obj)
        {
            CurrentChildView = new MailViewModel();
            Caption = "Mail";
            Icon = IconChar.MailBulk;
        }

        //constructors to execute the commands
        private void ExecuteShowCustomerViewCommand(object obj)
        {
           CurrentChildView=new CustomerViewModel();
            Caption = "Customer";
            Icon= IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        { 
                var user= userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
                if (user != null)
                {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $" {user.Name} {user.LastName} ";
                CurrentUserAccount.ProfilePicture = null;

            }
                else
                {
                    CurrentUserAccount.DisplayName = "Invalid User,Not Logged In";
                   Application.Current.Shutdown();
                }

           
        }
    }
}
