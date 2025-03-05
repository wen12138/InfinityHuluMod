using b1;
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
    public class Patche_1
    {
        public static void ApplyPatch(Harmony harmony)
        {
            // 手动获取目标方法的 MethodInfo
            var targetAssembly = Assembly.Load("BtlSvr.Main.dll");
            if (targetAssembly != null)
            {
                //var targetType = AccessTools.TypeByName("b1.BUS_PoleDrinkComp"); // 替换为实际的命名空间和类名
                var targetType = targetAssembly.GetType("b1.BUS_PoleDrinkComp");

                if (targetType != null)
                {
                    //var targetMethod = AccessTools.Method(targetType, "DoPoleDrink", new Type[] { typeof(EPoleDrinkType), typeof(int) });
                    var targetMethod = targetType.GetMethod("DoPoleDrink", BindingFlags.NonPublic | BindingFlags.Instance);

                    var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (targetMethod != null)
                    {
                        // 手动应用补丁
                        foreach (var item in targetMethod.GetParameters())
                        {
                            Utils.Log($"DoPoleDrink Param: {item.ParameterType} {item.Name}");
                        }
                        harmony.Patch(targetMethod, new HarmonyMethod(typeof(Patche_1).GetMethod(nameof(Postfix))));
                    }
                    else
                    {
                        Utils.Log("Can't Patch DoPoleDrink!!!");
                    }
                }
                else
                {
                    Utils.Log("Can't Find b1.BUS_PoleDrinkComp!!!");
                }

            }
            else
            {
                Utils.Log("Can't Load BtlSvr.Main.dll!!!");
            }
        }

        [HarmonyPostfix]
        public static void Postfix(EPoleDrinkType PoleDrinkType, int SkillID, int PosIndex)
        {
            Utils.Log($"Postfix Ori BUS_PoleDrinkComp DoPoleDrink. DrinkType: {PoleDrinkType}, SkillID: {SkillID}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetFixFunctionTemplate))]
    public class Patche_2
    {
        [HarmonyPrefix]
        private static void Prefix(EFixFunctionType FixFunctionType)
        {
            Utils.Log($"Prefix BGW_EffectTemplateList GetFixFunctionTemplate. FixFunctionType: {FixFunctionType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetInteractTypeTemplate))]
    public class Patche_3
    {
        [HarmonyPrefix]
        private static void Prefix(EInteractType InteractType)
        {
            if (InteractType != EInteractType.RebirthPoint)
                Utils.Log($"Prefix BGW_EffectTemplateList GetInteractTypeTemplate. InteractType: {InteractType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetInteractActionTemplate))]
    public class Patche_4
    {
        [HarmonyPrefix]
        private static void Prefix(EInteractAction InteractType)
        {
            Utils.Log($"Prefix BGW_EffectTemplateList GetInteractActionTemplate. InteractType: {InteractType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetMatchingPosTypeTemplate))]
    public class Patche_5
    {
        [HarmonyPostfix]
        private static void Postfix(EMatchingPosType MatchingPosType)
        {
            Utils.Log($"Postfix BGW_EffectTemplateList GetMatchingPosTypeTemplate. MatchingPosType: {MatchingPosType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetAttrCostTemplate))]
    public class Patche_6
    {
        [HarmonyPrefix]
        private static void Prefix(EAttrCostType AttrCostType)
        {
            if (AttrCostType != EAttrCostType.None && AttrCostType != EAttrCostType.Mp)
                Utils.Log($"Prefix BGW_EffectTemplateList GetAttrCostTemplate. AttrCostType: {AttrCostType}");
        }
    }

    //[HarmonyPatch(typeof(BGW_EffectTemplateList), "GetInputActionTemplate")]
    public class Patche_7
    {
        [HarmonyPrefix]
        private static void Prefix(EInputActionType InputActionType)
        {
            Utils.Log($"Prefix BGW_EffectTemplateList GetInputActionTemplate. InputActionType: {InputActionType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetSkillSelectTargetTemplate))]
    public class Patche_8
    {
        [HarmonyPrefix]
        private static void Prefix(ESmartSelectTargetType SelectTargetType, ref BUSkillSelectTargetTemplate __result)
        {
            Utils.Log($"Prefix BGW_EffectTemplateList GetSkillSelectTargetTemplate. SelectTargetType: {SelectTargetType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BGW_EffectTemplateList), nameof(b1.BGW_EffectTemplateList.GetSkillSelectShapeTemplate))]
    public class Patche_9
    {
        [HarmonyPrefix]
        private static void Prefix(ESmartSelectShapeType SelectShapeType, ref BUSkillSelectShapeTemplate __result)
        {
            Utils.Log($"Prefix BGW_EffectTemplateList GetSkillSelectShapeTemplate. SelectShapeType: {SelectShapeType}");
        }
    }


    //[HarmonyPatch(typeof(b1.BUInputActionTemplate), nameof(b1.BUInputActionTemplate.TriggerInputAction))]
    public class Patche_11
    {
        [HarmonyPrefix]
        private static void Prefix(UGameInstance InWorldContext, int InputActionID, ETriggerEvent TriggerEvent, b1.FInputActionValue Value, GSPredictionKey PredictionKey)
        {
            Utils.Log($"Prefix BUInputActionTemplate TriggerInputAction. InputActionID: {InputActionID}, TriggerEvent: {TriggerEvent}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_PlayerInputActionComp), "OnTriggerItemSkillAction_ShortCut")]
    public class Patche_14
    {
        [HarmonyPrefix]
        private static void Prefix(int InputActionID, ETriggerEvent TriggerEvent, EInputActionType InputActionType)
        {
            Utils.Log($"Prefix BUS_PlayerInputActionComp OnTriggerItemSkillAction_ShortCut!!! InputActionID: {InputActionID}, TriggerEvent: {TriggerEvent}, InputActionType: {InputActionType}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_PlayerInputActionComp), "OnInputCastSkill")]
    public class Patche_15
    {
        [HarmonyPrefix]
        private static void Prefix(EInputActionType InputActionType, bool IsRelease, int SkillID, int DescID, int ItemID = -1)
        {
            Utils.Log($"Prefix BUS_PlayerInputActionComp OnInputCastSkill!!! InputActionType: {InputActionType}, IsRelease: {IsRelease}, SkillID: {SkillID}, DescID: {DescID}, ItemID: {ItemID}");
        }
    }
    //[HarmonyPatch(typeof(b1.BUS_PlayerInputActionComp), "TryCastConsumeItemSkill")]
    public class Patche_16
    {
        [HarmonyPrefix]
        private static void Prefix(int ItemID, int PosIndex)
        {
            Utils.Log($"Prefix BUS_PlayerInputActionComp TryCastConsumeItemSkill!!! ItemID: {ItemID}, PosIndex: {PosIndex}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_PlayerInputActionComp), "CheckItemNumInBag")]
    public class Patche_17
    {
        [HarmonyPrefix]
        private static void Prefix(int ItemId, ref int __result)
        {
            Utils.Log($"Prefix BUS_PlayerInputActionComp CheckItemNumInBag!!! ItemId: {ItemId}, __result: {__result}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_PlayerInputActionComp), "DoAttackLogic")]
    public class Patche_18
    {
        [HarmonyPrefix]
        private static void Prefix(EInputActionType InputActionType, bool IsRelease, int DescID = -1)
        {
            Utils.Log($"Prefix BUS_PlayerInputActionComp DoAttackLogic!!! InputActionType: {InputActionType}, IsRelease: {IsRelease}, DescID: {DescID},");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_PlayerInputActionComp), "TriggerItemSkill")]
    public class Patche_19
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            Utils.Log($"Prefix BUS_PlayerInputActionComp TriggerItemSkill!!!");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_SmartCastSkillComp), "AutoSmartCastSkill_Predict")]
    public class Patche_20
    {
        [HarmonyPrefix]
        private static void Prefix(int SkillID, List<int> MappingRuleIDList, EMontageBindReason Reason, ESkillDirection SkillDirection, bool bNeedCheckSkillCanCast, ECastSkillSourceType SourceType, GSPredictionKey PredictionKey)
        {
            Utils.Log($"Prefix BUS_SmartCastSkillComp AutoSmartCastSkill_Predict!!! SkillID: {SkillID}, Reason: {Reason}, SkillDirection: {SkillDirection}, bNeedCheckSkillCanCast: {bNeedCheckSkillCanCast}, SourceType: {SourceType}, PredictionKey: {PredictionKey}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_SmartCastSkillComp), "AutoSmartCastSkill")]
    public class Patche_21
    {
        [HarmonyPrefix]
        private static void Prefix(int SkillID, List<int> MappingRuleIDList, EMontageBindReason Reason, ESkillDirection SkillDirection, bool bNeedCheckSkillCanCast, ECastSkillSourceType SourceType, GSPredictionKey PredictionKey)
        {
            Utils.Log($"Prefix BUS_SmartCastSkillComp AutoSmartCastSkill!!! SkillID: {SkillID}, Reason: {Reason}, SkillDirection: {SkillDirection}, bNeedCheckSkillCanCast: {bNeedCheckSkillCanCast}, SourceType: {SourceType}, PredictionKey: {PredictionKey}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_SmartCastSkillComp), "MoveToCastSkill")]
    public class Patche_22
    {
        [HarmonyPrefix]
        private static void Prefix(int SkillID, EMontageBindReason Reason)
        {
            Utils.Log($"Prefix BUS_SmartCastSkillComp MoveToCastSkill!!! SkillID: {SkillID}, Reason: {Reason}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_SmartCastSkillComp), "StartAttackTracing")]
    public class Patche_23
    {
        [HarmonyPrefix]
        private static void Prefix(int SkillID)
        {
            Utils.Log($"Prefix BUS_SmartCastSkillComp StartAttackTracing!!! SkillID: {SkillID}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_SmartCastSkillComp), "TryCastSkill")]
    public class Patche_24
    {
        [HarmonyPrefix]
        private static void Prefix(int SkillID, EMontageBindReason Reason, ESkillDirection SkillDirection, bool bNeedCehckSkillCanCast, ECastSkillSourceType SourceType)
        {
            Utils.Log($"Prefix BUS_SmartCastSkillComp TryCastSkill!!! SkillID: {SkillID}, Reason: {Reason}, SkillDirection: {SkillDirection}, bNeedCehckSkillCanCast: {bNeedCehckSkillCanCast}, SourceType: {SourceType}");
        }
    }


    [HarmonyPatch(typeof(b1.BUS_SkillInstsCompSvr), "OnUnitCastSkillTry")]
    [HarmonyPatch(new Type[] { typeof(FCastSkillInfo), typeof(GSPredictionKey) })]
    public class Patche_25
    {
        [HarmonyPrefix]
        private static void Prefix(FCastSkillInfo CSI, GSPredictionKey PredictionKey)
        {
            Utils.Log($"Prefix BUS_SkillInstsCompSvr OnUnitCastSkillTry!!! SkillID: {CSI.SkillID}, SkillDirection: {CSI.SkillDirection}, SkillMontageBeginPos: {CSI.SkillMontageBeginPos}, " +
                $"SourceType: {CSI.SourceType}, Reason: {CSI.Reason}, NeedCheckSkillCanCast: {CSI.NeedCheckSkillCanCast},  MontageStartSectionName: {CSI.MontageStartSectionName}");

            //FUStSkillSDesc skillSDesc = BGW_GameDB.GetSkillSDesc(CSI.SkillID, Utils.GetControlledPawn());
            //FUStSkillSMappingDesc skillMapDesc = BGW_GameDB.GetSkillSMappingDesc(CSI.SkillID);

            //Utils.Log($"SkillDesc. {skillSDesc.SkillType}, ResultRull: {skillMapDesc.ResultRull}");
        }
    }

    //[HarmonyPatch(typeof(b1.BUS_SkillMappingComp), "AddMappedSkillInfo")]
    public class Patche_26
    {
        [HarmonyPrefix]
        private static void Prefix(ESkillMappingConditionType SkillMappingConditionType, ref int ChooseIdx, in SkillMappingConfig MappingConfig, in string BattleInfoLog = "")
        {
            Utils.Log($"Prefix BUS_SkillInstsCompSvr AddMappedSkillInfo!!! SkillMappingConditionType: {SkillMappingConditionType}, ChooseIdx: {ChooseIdx}, MontagePosOffset: {BattleInfoLog}, Reason: {BattleInfoLog}");

            //if (MappingConfig.SkillMappingConditionType == ESkillMappingConditionType.Attr)
            //{
            //    var attrFloat = (EBGUAttrFloat)MappingConfig.IntParams[2];
            //    Utils.Log($"attrFloat: {attrFloat}");

            //    if (attrFloat == EBGUAttrFloat.BloodBottomNum && InfinityHuluMod.IsInfinityHulu() && ChooseIdx == 0)
            //    {
            //        Utils.Log("Want ChangeChooseIdx From 0 -> 1");
            //        ChooseIdx = 1;
            //    }
            //}
            //else if (MappingConfig.SkillMappingConditionType == ESkillMappingConditionType.Talent)
            //{
            //    var talentID = MappingConfig.IntParams[0];
            //    Utils.Log($"talentID: {talentID}");

            //    var talentLevel = InfinityHuluMod.SelfPoleDrinkComp.TalentData.GetTalentLevel(talentID);
            //    Utils.Log($"talentLevel: {talentLevel}");
            //}

            //foreach (var skillID in MappingConfig.SkillIDs)
            //{
            //    Utils.Log($"SkillID: {skillID}");
            //}

            //foreach (var sectionName in MappingConfig.SectionNameList)
            //{
            //    Utils.Log($"sectionName: {sectionName}");
            //}

            //foreach (var montagePath in MappingConfig.MontagePaths)
            //{
            //    Utils.Log($"montagePath: {montagePath}");
            //}
        }
    }

    [HarmonyPatch(typeof(b1.BUC_SkillMappingData), "SetResultSkillIDAndMontagePath")]
    public class Patche_27
    {
        private const string k_Heyao_Failed_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_Heyao_02.AM_Wukong_Heyao_02";
        private const string k_Heyao_Succ_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_Heyao_01.AM_Wukong_Heyao_01";
        private const string k_Heyao_Fast_Succ_Montage_Path = "/Game/00Main/Animation/Player/Wukong/AM/Attack/Heyao/AM_Wukong_heyao_01_fast.AM_Wukong_heyao_01_fast";

        [HarmonyPostfix]
        private static void Postfix(BUC_SkillMappingData __instance, int MainSkillID, int ResultID, string ResultPath)
        {
            // 预备在此处进行修改
            Utils.Log($"Postfix BUC_SkillMappingData SetResultSkillIDAndMontagePath!!! MainSkillID: {MainSkillID}, ResultID: {ResultID}, ResultPath: {ResultPath}");

            if (__instance != null)
            {
                if (MainSkillID == 10530 && ResultID == 10530 && ResultPath == k_Heyao_Failed_Montage_Path && InfinityHuluMod.IsInfinityHulu())
                {
                    Utils.Log("Infinity Hulu Must Be Infinity!!!");
                    string targetPath = InfinityHuluMod.IsFastDrinkHulu() ? k_Heyao_Fast_Succ_Montage_Path : k_Heyao_Succ_Montage_Path;

                    Utils.Log($"Try Set BUC_SkillMappingData MainSkillID: {MainSkillID}, ResultID: {ResultID}, ResultPath: {targetPath} Because Current Is InfinityHulu!!!");
                    __instance.SetResultSkillIDAndMontagePath(MainSkillID, ResultID, targetPath);
                }
            }
        }
    }
}
