using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FITFINAL.Services;
using System.Collections.ObjectModel;

namespace FITFINAL.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly DataService _data;

        public RegisterViewModel() : this(DataService.Instance) { }

        public RegisterViewModel(DataService data)
        {
            _data = data;

            Type = "Ingreso";
            Category = null;

            Types = new ObservableCollection<string> { "Ingreso", "Gasto" };
            Categories = new ObservableCollection<string> { "Comida", "Transporte", "Salud", "Servicios", "Otros" };
        }

        [ObservableProperty] string description = string.Empty;

        // ✅ Ahora es string
        [ObservableProperty] string amount = string.Empty;

        [ObservableProperty] string type;
        [ObservableProperty] string category;

        public ObservableCollection<string> Types { get; }
        public ObservableCollection<string> Categories { get; }

        [RelayCommand]
        void Save()
        {
            if (string.IsNullOrWhiteSpace(Description))
                return;

            if (!decimal.TryParse(Amount, out decimal amountValue) || amountValue <= 0)
                return;

            if (Type == "Gasto" && string.IsNullOrWhiteSpace(Category))
                return;

            _data.AddTransaction(Description, amountValue, Type, Category);

            Description = string.Empty;
            Amount = string.Empty;
            Type = "Ingreso";
            Category = null;
        }
    }
}