using FITFINAL.Services;
using FITFINAL.ViewModels;

namespace FITFINAL.Views
{
    public partial class GoalsPage : ContentPage
    {
        public GoalsPage()
        {
            InitializeComponent();
            BindingContext = new GoalsViewModel(DataService.Instance);
        }

    }
}