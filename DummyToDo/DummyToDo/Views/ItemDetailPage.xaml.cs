using DummyToDo.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DummyToDo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}