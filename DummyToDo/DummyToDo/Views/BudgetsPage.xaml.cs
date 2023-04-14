using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DummyToDo.ViewModels;
using DummyToDo.Models;

namespace DummyToDo.Views
{ 
    public partial class BudgetsPage : ContentPage
    {
        BudgetsViewModel _viewModel;
        public BudgetsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new BudgetsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}