using KlondikeCalc.Classes.Dbc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlondikeCalc.Classes
{
    public static class BaseReader
    {
        private static string _connectionString;

        #region Methods
        #region Public
        public static List<SimpleItem> GetItemList()
        {
            List<SimpleItem> itemList = new List<SimpleItem>();
            SqlConnection connection = OpenConncetion();
            SqlDataReader reader = GetReader("SELECT id, name FROM dbo.Items", connection);
            while (reader.Read())
            {
                itemList.Add(new SimpleItem() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            CloseConnection(connection);
            return itemList;
        }

        public static List<Recipe> GetRecipes(Item selected)
        {
            List<Recipe> recipes = new List<Recipe>();
            SqlConnection connection = OpenConncetion();
            SqlDataReader reader = GetReader("SELECT * FROM dbo.Recipes WHERE ItemId=" + selected.ID, connection);
            while (reader.Read())
            {
                Recipe newRecipe = new Recipe();
                newRecipe.RecipeItem = selected;
                newRecipe.Count = reader.GetInt32(2);
                //TODO: Здесь читается здание
                int readerpos = 4;
                while (!reader.IsDBNull(readerpos))
                {
                    newRecipe.Ingredients.Add(new RecipeIngredient() { Ingeredient = new Item() { ID = reader.GetInt32(readerpos) }, Count = reader.GetInt32(readerpos + 1)});
                    readerpos += 2;
                }
                recipes.Add(newRecipe);        
            }
            foreach (Recipe rec in recipes)
            {
                foreach (RecipeIngredient ingredient in rec.Ingredients)
                {
                    Item addItem = GetItem(ingredient.Ingeredient.ID);
                    ingredient.Ingeredient = addItem;
                }
            }
            return recipes;
        }       

        public static Item GetItem(int itemId)
        {
            Item item = new Item();
            SqlConnection connection = OpenConncetion();
            SqlDataReader reader = GetReader("SELECT * FROM dbo.Items WHERE id=" + itemId, connection);
            reader.Read();
            item.ID = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            item.Description = reader.GetString(2);
            CloseConnection(connection);
            return item;
        }

        public static Item GetItem(SimpleItem selectedItem)
        {
            return GetItem(selectedItem.Id);
        }
        #endregion

        #region Private
        private static SqlConnection OpenConncetion()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        private static SqlDataReader GetReader(string queryText, SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = queryText;
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        private static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }  
        #endregion
        #endregion

    }
}
