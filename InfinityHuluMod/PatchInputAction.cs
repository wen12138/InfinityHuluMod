﻿using b1;
using BtlShare;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityHuluMod
{
    //[HarmonyPatch(typeof(BGW_EffectTemplateList), "InitInputActionTemplates")]
    public class PatchInputAction
    {
        private static void Postfix(BGW_EffectTemplateList __instance)
        {
            if (__instance != null)
            {
                var type = __instance.GetType();
                var field = type.GetField("InputActionTemplate", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (field != null)
                {
                    var map = field.GetValue(__instance) as Dictionary<EInputActionType, BUInputActionTemplate>;

                    map[EInputActionType.CastItemSkill] = new BUICastItemSkill();

                    Utils.Log("Replace CastItemSkill Class Succ!");
                }
            }
        }
    }
}
