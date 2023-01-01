using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using KMod;

using Database;
using TUNING;
using static ComplexRecipe;


namespace Fats_not_included.Recipes
{
    class OilyRecipies
    {
        private static void AddRecipe()
        {
            RecipeElement[] input = new RecipeElement[]
            {
                    //new RecipeElement((Tag) "BasicPlantFood", 1f)
                    new RecipeElement(Fats_not_included.Elements.Biooil.OIL_ID, 1f),
                    new RecipeElement(MeatConfig.ID, 1f),
            };

            RecipeElement[] output = new RecipeElement[]
            {
                    new RecipeElement(OilyFoods.PreservedMeatConfig.ID, 2f),
            };

            string recipeID = ComplexRecipeManager.MakeRecipeID(CookingStationConfig.ID, input, output);

            OilyFoods.PreservedMeatConfig.recipe = new ComplexRecipe(recipeID,
                input, output)
            {
                time = FOOD.RECIPES.SMALL_COOK_TIME,
                description = (LocString)"Meat preserved in vegetal Oil",
                nameDisplay = RecipeNameDisplay.Result,
                fabricators = new List<Tag> { (Tag)"CookingStation" },
            };
        }

    }
}
