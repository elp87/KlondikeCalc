using KlondikeCalc.Classes;
using KlondikeCalc.Classes.Dbc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace KlondikeCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SimpleItem> _itemTypes;
        private SimpleItem _selectedItem;

        public MainWindow()
        {
            InitializeComponent();
            _itemTypes = BaseReader.GetItemList();
            ItemsListBox.ItemsSource = _itemTypes;
        }
        
        private void ItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = (SimpleItem)ItemsListBox.SelectedItem;
            Item selected = BaseReader.GetItem(_selectedItem);
            ItemNameLabel.Content = selected.Name;
            DescriptionTextBlock.Text = selected.Description;
            List<Recipe> recipes = BaseReader.GetRecipes(selected);
        }
    }
}
