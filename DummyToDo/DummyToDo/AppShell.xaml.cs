using DummyToDo.ViewModels;
using DummyToDo.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DummyToDo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(BudgetsPage), typeof(BudgetsPage));
            Routing.RegisterRoute(nameof(BudgetEntry),typeof(BudgetEntry));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(BudgetDetail), typeof(BudgetDetail));
        }

    }
}
