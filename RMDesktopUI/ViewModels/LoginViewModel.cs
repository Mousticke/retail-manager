using Caliburn.Micro;
using RMDesktopUI.Helpers;
using RMDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private string _errorMessage;
        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string UserName
        {
            get { return _userName; }
            set 
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public bool CanLogIn
        {
            get 
            {
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }      
        }

        public async Task LogIn()
        {
            try
            {
                var result = await _apiHelper.AuthenticateAsync(UserName, Password);
                // Capture more information about the user
                await _apiHelper.GetLoggedInUserInfoAsync(result.Access_token);
                // Collapse the error message if any. That means, everything went well.
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
