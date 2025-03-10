﻿using b1;
using b1.Prediction;
using BtlB1;
using BtlShare;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Engine;
using UnrealEngine.Plugins.EnhancedInput;
using UnrealEngine.Runtime;

namespace InfinityHuluMod
{
    [HarmonyPatch(typeof(b1.BUC_SkillMappingData), "SetResultSkillIDAndMontagePath")]
    public class PatchSkillMappingData
    {
        private const string k_Heyao_Failed_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_Heyao_02.AM_Wukong_Heyao_02";
        private const string k_Heyao_Succ_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_Heyao_01.AM_Wukong_Heyao_01";
        private const string k_Heyao_Fast_Succ_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_heyao_01_fast.AM_Wukong_heyao_01_fast";
        private const string k_Heyao_Fail_Continue_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_Heyao_02_continue.AM_Wukong_Heyao_02_continue";
        private const string k_Heyao_Succ_Continue_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_heyao_01_continue.AM_Wukong_heyao_01_continue";
        private const string k_Heyao_Fast_Fail_Continue_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_heyao_02_fast_continue.AM_Wukong_heyao_02_fast_continue";
        private const string k_Heyao_Fast_Succ_Continue_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_heyao_01_fast_continue.AM_Wukong_heyao_01_fast_continue";

        [HarmonyPostfix]
        private static void Postfix(BUC_SkillMappingData __instance, int MainSkillID, int ResultID, string ResultPath)
        {
            if (__instance != null)
            {
                if (MainSkillID == 10530 && ResultID == 10530 && InfinityHuluMod.IsInfinityHulu())
                {
                    switch (ResultPath)
                    {
                        case k_Heyao_Failed_Montage_Path:
                            string targetPath = InfinityHuluMod.IsFastDrinkHulu() ? k_Heyao_Fast_Succ_Montage_Path : k_Heyao_Succ_Montage_Path;
                            __instance.SetResultSkillIDAndMontagePath(MainSkillID, ResultID, targetPath);
                            break;
                        case k_Heyao_Fail_Continue_Montage_Path:
                            __instance.SetResultSkillIDAndMontagePath(MainSkillID, ResultID, k_Heyao_Succ_Continue_Montage_Path);
                            break;
                        case k_Heyao_Fast_Fail_Continue_Montage_Path:
                            __instance.SetResultSkillIDAndMontagePath(MainSkillID, ResultID, k_Heyao_Fast_Succ_Continue_Montage_Path);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
