using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using FITFINAL.Models;

namespace FITFINAL.Services
{
    public class DataService
    {
        // ✅ Singleton
        private static DataService? _instance;
        public static DataService Instance => _instance ??= new DataService();

        // ✅ Evento global para actualizar todas las Views
        public event Action? DataChanged;

        private readonly string transactionsPath;
        private readonly string goalsPath;

        private List<Transaction> _transactions = new();
        private List<Goal> _goals = new();

        private int _transactionId = 1;
        private int _goalId = 1;

        // ✅ Constructor privado (solo se crea una instancia)
        private DataService()
        {
            transactionsPath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");
            goalsPath = Path.Combine(FileSystem.AppDataDirectory, "goals.json");

            LoadData();
        }

        private void NotifyChange() => DataChanged?.Invoke();

        // ✅ Cargar JSON
        private void LoadData()
        {
            if (File.Exists(transactionsPath))
            {
                var json = File.ReadAllText(transactionsPath);
                _transactions = JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new();
                if (_transactions.Any())
                    _transactionId = _transactions.Max(t => t.Id) + 1;
            }

            if (File.Exists(goalsPath))
            {
                var json = File.ReadAllText(goalsPath);
                _goals = JsonConvert.DeserializeObject<List<Goal>>(json) ?? new();
                if (_goals.Any())
                    _goalId = _goals.Max(g => g.Id) + 1;
            }
        }

        // ✅ Guardar JSON
        private void SaveData()
        {
            File.WriteAllText(transactionsPath, JsonConvert.SerializeObject(_transactions, Formatting.Indented));
            File.WriteAllText(goalsPath, JsonConvert.SerializeObject(_goals, Formatting.Indented));
        }

        // ============================================================
        // ✅ TRANSACCIONES
        // ============================================================

        public void AddTransaction(string description, decimal amount, string type, string? category = null)
        {
            _transactions.Add(new Transaction
            {
                Id = _transactionId++,
                Description = description,
                Amount = amount,
                Type = type,
                Category = category,
                Date = DateTime.Now
            });

            SaveData();
            NotifyChange();
        }

        public List<Transaction> GetAllTransactions() =>
            _transactions.OrderByDescending(t => t.Date).ToList();

        public decimal GetTotalIncomes() =>
            _transactions.Where(t => t.Type == "Ingreso").Sum(t => t.Amount);

        public decimal GetTotalExpenses() =>
            _transactions.Where(t => t.Type == "Gasto").Sum(t => t.Amount);

        public decimal GetBalance() =>
            GetTotalIncomes() - GetTotalExpenses();

        // ============================================================
        // ✅ METAS
        // ============================================================

        public List<Goal> GetGoals() => _goals.ToList();

        public void AddGoal(string name, decimal target)
        {
            _goals.Add(new Goal
            {
                Id = _goalId++,
                Name = name,
                Target = target,
                Progress = 0
            });

            SaveData();
            NotifyChange();
        }

        public void AddAmountToGoal(int goalId, decimal amount)
        {
            var goal = _goals.FirstOrDefault(g => g.Id == goalId);
            if (goal == null)
                return;

            if (amount <= 0 || amount > GetBalance())
                return;

            goal.Progress += amount;

            // Registrar como gasto
            AddTransaction($"Aporte a meta: {goal.Name}", amount, "Gasto", "Metas");

            SaveData();
            NotifyChange();
        }

        // ✅ Reportes: gastos por categoría
        public Dictionary<string, decimal> GetExpensesByCategory()
        {
            return _transactions
                .Where(t => t.Type == "Gasto" && !string.IsNullOrWhiteSpace(t.Category))
                .GroupBy(t => t.Category!)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
        }
    }
}