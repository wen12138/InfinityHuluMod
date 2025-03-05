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
    //[HarmonyPatch(typeof(BGW_EffectTemplateList))]
    //[HarmonyPatch(nameof(BGW_EffectTemplateList.GetInputActionTemplate))]
    public class PatchInputActionTemplate
    {
        //[HarmonyPostfix]
        private static void PostFix(EInputActionType InputActionType, ref BUInputActionTemplate __result)
        {
            Utils.Log($"PostFix BGW_EffectTemplateList GetInputActionTemplate. InputActionType {InputActionType}, __result: {__result}");
        }
    }
}
