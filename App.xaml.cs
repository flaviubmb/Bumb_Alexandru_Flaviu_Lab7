using System;
using System.IO;
using BumbAlexandruFlaviuLab7.Data;

namespace BumbAlexandruFlaviuLab7
{
    public partial class App : Application
    {
        static ShoppingListDatabase database;


    public static ShoppingListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "ShoppingList.db3"
                    );
                    database = new ShoppingListDatabase(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
