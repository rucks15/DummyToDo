using DummyToDo.Data;
using DummyToDo.Services;
using DummyToDo.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DummyToDo
{
    public partial class App : Application
    {
        static ItemDatabase database;
        static BudgetDatabase bdatabase;

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        public static ItemDatabase Database
        {
            //get meyhod returns value
            get
            {
                if (database == null)
                {
                    database = new ItemDatabase
                        (Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "notes.db3"));
                }
                return database;
            }
        }

        public static BudgetDatabase BDatabase
        {
            get
            {
                if (bdatabase == null)
                {
                    bdatabase = new BudgetDatabase
                        (Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "budget.db3"));
                }
                return bdatabase;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
