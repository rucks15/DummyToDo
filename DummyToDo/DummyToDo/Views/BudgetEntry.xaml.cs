using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DummyToDo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DummyToDo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetEntry : ContentPage
    {
        public BudgetsPage budget { get; set; }
        public BudgetEntry()
        {
            InitializeComponent();
            BindingContext = new BudgetEntryViewModel();
        }


    }
}