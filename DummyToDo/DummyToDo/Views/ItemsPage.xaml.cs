using DummyToDo.Models;
using DummyToDo.ViewModels;
using DummyToDo.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DummyToDo.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        private int BudgetId = (int)Application.Current.Properties["BudgetId"];
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            Budget budget = await App.BDatabase.GetBudgetAsync(BudgetId);
            BudgetAmount.Text = budget.Budget_Amount.ToString();
            StartDate.Text = budget.StartDate.ToString();
            EndDate.Text = budget.EndDate.ToString();
            Balance.Text = budget.Balance.ToString();
        }

       

    }
    }
