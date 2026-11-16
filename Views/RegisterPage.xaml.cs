using FITFINAL.Services;
using FITFINAL.ViewModels;

namespace FITFINAL.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(DataService.Instance);
        }

    }
}