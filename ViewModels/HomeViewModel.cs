using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FITFINAL.Models;
using FITFINAL.Services;

namespace FITFINAL.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DataService _data;

        [ObservableProperty] decimal totalIncomes;
        [ObservableProperty] decimal totalExpenses;
        [ObservableProperty] decimal balance;
        [ObservableProperty] ObservableCollection<Transaction> transactions = new();

        public HomeViewModel(DataService data)
        {
            _data = data;

            _data.DataChanged += Refresh;

            Refresh();
        }

        public void Refresh()
        {
            TotalIncomes = _data.GetTotalIncomes();
            TotalExpenses = _data.GetTotalExpenses();
            Balance = _data.GetBalance();
            Transactions = new ObservableCollection<Transaction>(_data.GetAllTransactions());
        }
    }
}