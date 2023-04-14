using DummyToDo.Models;
using DummyToDo.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DummyToDo.ViewModels
{
    [QueryProperty(nameof(BudgetId), nameof(BudgetId))]
    public class ItemsViewModel : BaseViewModel
    {
        private Models.Budget budget;
        private int budget_id = (int)Application.Current.Properties["BudgetId"];

        private Item _selectedItem;
        private decimal budget_amount;
        private decimal balance;
        private DateTime startDate;
        private DateTime endDate;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }

        public Command EditBudgetCommand { get; }
        public Command DeleteBudgetCommand { get; }
        public Command<Item> ItemTapped { get; }

        public int BudgetId
        {
            get => budget_id;
            set
            {
                budget_id = value;
            }
        }

        public decimal Budget_Amount
        {
            set => SetProperty(ref budget_amount, value);
            get => budget_amount;
        }

        public decimal Balance
        {
            get => balance;
            set => SetProperty(ref balance, value);
        }

        public DateTime StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }

        public DateTime EndDate
        {
            get => endDate; set => SetProperty(ref endDate, value);
        }

        public ItemsViewModel()
        {
            Title = "Expenses";

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(BudgetId));
            
            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            EditBudgetCommand = new Command(OnEditBudget);

            DeleteBudgetCommand = new Command(OnDeleteBudget);
        }

        async Task ExecuteLoadItemsCommand(int BudgetId)
        {
            IsBusy = true;

            try
            {
                
                
                //Application.Current.Properties["BudgetId"] = BudgetId;

                Items.Clear();
                //Gets the items from Database that matches the foreign key "budget_id"
                var items = await App.Database.GetItemsAsync(BudgetId);
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public Item SelectedItem
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
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemViewModel)}");
           
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnEditBudget()
        {
            budget = await App.BDatabase.GetBudgetAsync(BudgetId);
            budget.Budget_Amount = Budget_Amount;
            budget.StartDate = StartDate;
            budget.EndDate = EndDate;
            budget.Balance = Balance;
            await App.BDatabase.SaveBudgetAsync(budget);
        }

        async void OnDeleteBudget()
        {
            var items = await App.Database.GetItemsAsync(BudgetId);
            foreach (var item in items)
            {
                await App.Database.DeleteItemAsync(item);
            }
            budget = await App.BDatabase.GetBudgetAsync(BudgetId);
            await App.BDatabase.DeleteBudgetAsync(budget);
        }
    }
}