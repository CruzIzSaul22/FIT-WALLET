using FITFINAL.Services;
using FITFINAL.ViewModels;

namespace FITFINAL.Views
{
    public partial class HomePage : ContentPage
    {
         public HomePage()
            {
                InitializeComponent();
                BindingContext = new HomeViewModel(DataService.Instance);
            }
        
    }
}