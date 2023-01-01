using HarmonyLib;
using KMod;

using Database;
using TUNING;
using static ComplexRecipe;
using System.Collections.Generic;


namespace Fats_not_included
{
    public class Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                Debug.Log("I execute before Db.Initialize!");
            }

            public static void Postfix()
            {
                Debug.Log("I execute after Db.Initialize!");
            }


        }

        [HarmonyPatch(typeof(Assets), "SubstanceListHookup")]
        public class Assets_addElement
        {
            public static void Prefix() { }

            public static void Postfix()
            {
                Fats_not_included.Elements.Biooil.RegisterSubstance();
            }
        }

    }
    public class OilLampInfo : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            //ModUtil.AddBuildingToPlanScreen("Power", Oillamp.OillampOn.ID);
            LocString.CreateLocStringKeys(typeof(OILSTRINGS.STRINGS.ELEMENTS.BIOOIL), null);
            base.OnLoad(harmony);
        }
    }

    [HarmonyPatch(typeof(CookingStationConfig), nameof(CookingStationConfig.ConfigureBuildingTemplate))]
    public static class Patch_CookingStationConfig_ConfigureRecipes
    {
        public static void Postfix()
        {
            Debug.Log("Adding Oily recipies");
            AddRecipe();
        }

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
