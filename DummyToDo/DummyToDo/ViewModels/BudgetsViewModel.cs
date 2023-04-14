using DummyToDo.Models;
using DummyToDo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Budget = DummyToDo.Models.Budget;

namespace DummyToDo.ViewModels
{

    public class BudgetsViewModel : BaseViewModel
    {

        private Models.Budget _selectedItem;
        public ObservableCollection<Budget> Budgets { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Models.Budget> ItemTapped { get; }

        public BudgetsViewModel()
        {
            Title = "Budget List Page";
            Budgets = new ObservableCollection<Models.Budget>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Models.Budget>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Budgets.Clear();
                //Gets the item from Database
                var budgets = await App.BDatabase.GetBudgetsAsync();
                foreach (var budget in budgets)
                {
                    Budgets.Add(budget);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Models.Budget SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(BudgetEntry)}?{nameof(BudgetEntryViewModel)}");
        }

        async void OnItemSelected(Models.Budget budget)
        {
            if (budget == null)
                return;
            //Application Current property persist any selected budget ID
            Application.Current.Properties["BudgetId"] = budget.Id; 
            // This will go to ItemsDetail page
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?{nameof(ItemsViewModel.BudgetId)}={budget.Id}");
        }
    }
}
