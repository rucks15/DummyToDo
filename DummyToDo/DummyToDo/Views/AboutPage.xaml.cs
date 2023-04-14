using DummyToDo.Models;
using DummyToDo.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DummyToDo.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(BudgetsPage)}");
            //await Navigation.PushModalAsync(new BudgetsPage());
        }
    }
}