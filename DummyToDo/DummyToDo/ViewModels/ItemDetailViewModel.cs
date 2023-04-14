using DummyToDo.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DummyToDo.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private Item item;
        private int itemId;
        private string expense;
        private string category;
        private decimal amount;
        private DateTime? date;
        public int Id { get; set; }

        public ItemDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            DeleteCommand = new Command(OnDelete);
        }
        public string Text
        {
            get => expense;
            set => SetProperty(ref expense, value);
        }

        public string Description
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public decimal Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public DateTime? Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public decimal Total
        {
            get => Total;
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public Command SaveCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public async void LoadItemId(int itemId)
        {
            try
            {
                item = await App.Database.GetItemAsync(itemId);
                //var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Expense;
                Description = item.Category;
                Amount = item.Amount;
                Date = item.Date;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        //To Edit & save new details
        private async void OnSave()
        {
            item.Expense = Text;
            item.Category = Description;
            item.Date = DateTime.UtcNow;
            item.Amount = Amount;

            //update the data to database
            await App.Database.SaveItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDelete()
        {
            //save the data to database
            await App.Database.DeleteItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }
    }
}
