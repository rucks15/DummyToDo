using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using DummyToDo.Models;
using Xamarin.Forms;
using DummyToDo.Views;

namespace DummyToDo.ViewModels
{
    internal class BudgetEntryViewModel
    {
        private decimal budgetAmount;
        private DateTime sDate;
        private DateTime eDate;
        private decimal balance;

        public BudgetEntryViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        public decimal BudgetAmount
        {
            get { return budgetAmount; }
            set { budgetAmount = value; }
        }

        public DateTime StartDate
        {
            get { return sDate; }
            set { sDate = value; }
        }

        public DateTime EndDate
        {
            get => eDate;
            set { eDate = value; }
        }

        public decimal BudgetBalance
        {
            get; set;
        }

        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }

        public async void OnSave()
        {
            var budget = new Models.Budget();
            budget.Budget_Amount = BudgetAmount;
            budget.StartDate = StartDate;
            budget.EndDate = EndDate;
            budget.Balance = BudgetAmount;

            await App.BDatabase.SaveBudgetAsync(budget);
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?{nameof(ItemsViewModel.BudgetId)}={budget.Id}");

        }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
