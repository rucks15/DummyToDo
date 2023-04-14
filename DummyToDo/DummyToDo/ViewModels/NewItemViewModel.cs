using DummyToDo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DummyToDo.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string expense;
        private string category;
        private decimal amount;
        private DateTime date;
        private int budget_id = (int)Application.Current.Properties["BudgetId"];

        public NewItemViewModel()
        {
            
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(expense)
                && !String.IsNullOrWhiteSpace(category);
        }

        public string Expense
        {
            get => expense;
            set => SetProperty(ref expense, value);
        }

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public decimal Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public int Budget_Id
        {
            get => budget_id;
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Expense = Expense,
                Category = Category,
                Date = DateTime.UtcNow,
                Amount = Amount,
                Budget_id = Budget_Id,
            };
            Budget budget = await App.BDatabase.GetBudgetAsync(Budget_Id);
            budget.Balance -= Amount;
            //updates Balance to Budgetdatabase
            await App.BDatabase.SaveBudgetAsync(budget);
            //save the new expense item to Itemdatabase
            await App.Database.SaveItemAsync(newItem);
            //await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
