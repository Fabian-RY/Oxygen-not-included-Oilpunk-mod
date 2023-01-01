
using System.Collections.Generic;

using UnityEngine;
using HarmonyLib;


namespace Fats_not_included.Elements
{
    class Biooil
    {
        public static readonly Color32 OIL_RED = new Color32(223, 216, 85, 255);
        public static string OIL_ID = "Biooil";
        public static string kanim_name = "liquid_tank_kanim";
        
        public static Dictionary<SimHashes, string> SimHashNameLookup = new Dictionary<SimHashes, string>();
        public static readonly Dictionary<string, object> ReverseSimHashNameLookup = new Dictionary<string, object>();

        public static Substance RegisterSubstance()
        {
            Substance result = ModUtil.CreateSubstance(OIL_ID, 
                Element.State.Liquid, 
                Assets.Anims.Find((anim) => anim.name == kanim_name),
                Assets.instance.substanceTable.liquidMaterial,
                OIL_RED, OIL_RED, OIL_RED);
            SimHashes simHash = (SimHashes)Hash.SDBMLower(OIL_ID);
            SimHashNameLookup.Add(simHash, OIL_ID);
            ReverseSimHashNameLookup.Add(OIL_ID, simHash);
            Assets.instance.substanceTable.GetList().Add(result);
            ElementLoader.FindElementByHash(result.elementID).substance = result;
            return result;
        }
    }
   
}
