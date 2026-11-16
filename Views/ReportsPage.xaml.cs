using FITFINAL.Services;
using FITFINAL.ViewModels;

namespace FITFINAL.Views
{
    public partial class ReportsPage : ContentPage
    {
        public ReportsPage()
        {
            InitializeComponent();
            BindingContext = new ReportsViewModel(DataService.Instance);
        }
    }
}