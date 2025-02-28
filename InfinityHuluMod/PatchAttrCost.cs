using b1;
using BtlShare;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityHuluMod
{
    [HarmonyPatch(typeof(BGW_EffectTemplateList), "InitAttrCostTemplates")]
    public static class PatchAttrCost
    {
        private static void Postfix(BGW_EffectTemplateList __instance)
        {
            if (__instance != null)
            {
                var type = __instance.GetType();
                var field = type.GetField("AttrCostTemplates", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (field != null)
                {
                    var map = field.GetValue(__instance) as Dictionary<EAttrCostType, BUAttrCostTemplate>;

                    map[EAttrCostType.BloodBottleNum] = new BUACBloodBottleNumCost();

                    Utils.Log("Replace BUACBloodBottleNumCost Class Succ!");
                }
            }
        }

    }
}
