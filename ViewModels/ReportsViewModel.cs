using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FITFINAL.Services;

namespace FITFINAL.ViewModels
{
    public partial class ReportsViewModel : ObservableObject
    {
        private readonly DataService _data;

        [ObservableProperty] ObservableCollection<KeyValuePair<string, decimal>> expensesByCategory = new();

        public ReportsViewModel(DataService data)
        {
            _data = data;

            _data.DataChanged += Load;

            Load();
        }

        void Load()
        {
            ExpensesByCategory.Clear();

            foreach (var item in _data.GetExpensesByCategory())
                ExpensesByCategory.Add(item);
        }
    }
}