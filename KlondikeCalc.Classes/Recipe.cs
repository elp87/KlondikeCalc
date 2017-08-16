using System.Collections.Generic;


namespace KlondikeCalc.Classes
{
    public class Recipe
    {
        public Item RecipeItem { get; set; }
        public int Count { get; set; }
        
        public List<RecipeIngredient> Ingredients { get; set; }

        public Recipe()
        {
            Ingredients = new List<RecipeIngredient>();
        }
    }
}
