using System.Collections.ObjectModel;
using FITFINAL.Models;
using FITFINAL.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FITFINAL.ViewModels
{
    public partial class GoalsViewModel : ObservableObject
    {
        private readonly DataService _data;

        [ObservableProperty] ObservableCollection<Goal> goals = new();
        [ObservableProperty] string newGoalName = string.Empty;
        [ObservableProperty] decimal newGoalTarget;
        [ObservableProperty] decimal contributionAmount;
        [ObservableProperty] Goal? selectedGoal;

        public GoalsViewModel(DataService data)
        {
            _data = data;

            _data.DataChanged += Load;

            Load();
        }

        void Load()
        {
            Goals = new ObservableCollection<Goal>(_data.GetGoals());
        }

        [RelayCommand]
        void AddGoal()
        {
            if (string.IsNullOrWhiteSpace(NewGoalName) || NewGoalTarget <= 0)
                return;

            _data.AddGoal(NewGoalName, NewGoalTarget);

            NewGoalName = string.Empty;
            NewGoalTarget = 0;
        }

        [RelayCommand]
        void Contribute()
        {
            if (SelectedGoal is null || ContributionAmount <= 0)
                return;

            _data.AddAmountToGoal(SelectedGoal.Id, ContributionAmount);

            ContributionAmount = 0;
        }
    }
}