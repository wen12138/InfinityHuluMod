using b1;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using b1.ECS;
using b1.EventDelDefine;

namespace InfinityHuluMod
{
    [HarmonyPatch(typeof(BGUPlayerCharacterCS), "InitAllComp")]
    public class PatchCharacterCS
    {
        private static void Postfix(BGUPlayerCharacterCS __instance)
        {
            if (__instance != null)
            {
                //InfinityHuluMod.SelfPoleDrinkComp = __instance.ActorCompContainerCS.RegisterUnitComp<TestPoleDrinkComp>(-2013265920, (EActorCompAlterFlag)0L, (EActorCompRejectFlag)0L, int.MaxValue, 0);
                InfinityHuluMod.SelfPoleDrinkComp = __instance.ActorCompContainerCS.AddComp(new TestPoleDrinkComp(), int.MaxValue, 0);
                #region Remove Comp
                EntityManager entMgr = null;
                {
                    Type worldType = __instance.ActorCompContainerCS.ECSWorld.GetType();
                    var nonPublicFields = worldType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var field in nonPublicFields)
                    {
                        if (field.Name == "EntMgr")
                        {
                            entMgr = field.GetValue(__instance.ActorCompContainerCS.ECSWorld) as EntityManager;
                            break;
                        }
                    }
                }

                var oriComp = entMgr?.GetObject<b1.BUS_PoleDrinkComp>(__instance.ECSEntity);
                InfinityHuluMod.OriPoleDrinkComp = oriComp;
                Utils.Log($"Get OriPoleDrinkComp : {InfinityHuluMod.OriPoleDrinkComp}");
                if (InfinityHuluMod.OriPoleDrinkComp != null)
                {
                    var type = InfinityHuluMod.OriPoleDrinkComp.GetType();
                    var nonPublicFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var field in nonPublicFields)
                    {
                        if (field.Name == "PoleDrinkData")
                        {
                            InfinityHuluMod.PoleDrinkData = field.GetValue(InfinityHuluMod.OriPoleDrinkComp) as BUC_PoleDrinkData;
                            Utils.Log($"Get OriPoleDrinkComp  PoleDrinkData: {InfinityHuluMod.PoleDrinkData}");
                        }
                        else if (field.Name == "AttrContainer")
                        {
                            InfinityHuluMod.AttrContainer = field.GetValue(InfinityHuluMod.OriPoleDrinkComp) as IBUC_AttrContainer;
                            Utils.Log($"Get OriPoleDrinkComp  AttrContainer: {InfinityHuluMod.AttrContainer}");
                        }
                    }

                }

                //if (oriComp != null)
                //{
                //    Type compContainerType = __instance.ActorCompContainerCS.GetType();
                //    int removeCount = 0;

                //    {
                //        var compCSs = compContainerType.GetField("CompCSs", BindingFlags.Instance | BindingFlags.NonPublic);
                //        if (compCSs != null)
                //        {
                //            var compList = compCSs.GetValue(__instance.ActorCompContainerCS) as List<UActorCompBaseCS>;
                //            compList.Remove(oriComp);
                //            removeCount++;
                //            Utils.Log("Remove Origin PoleDrinkComp Form CompCSs");
                //        }
                //    }

                //    {
                //        var compCSs = compContainerType.GetField("CompCSsToBeginPlay", BindingFlags.Instance | BindingFlags.NonPublic);
                //        if (compCSs != null)
                //        {
                //            var compList = compCSs.GetValue(__instance.ActorCompContainerCS) as List<UActorCompBaseCS>;
                //            compList.Remove(oriComp);
                //            removeCount++;
                //            Utils.Log("Remove Origin PoleDrinkComp From CompCSsToBeginPlay");
                //        }
                //    }

                //    if (removeCount == 2)
                //    {
                //        entMgr.RemoveObject(__instance.ECSEntity, oriComp);
                //        Utils.Log("Remove Origin PoleDrinkComp From EntityManager");

                //        if (oriComp.IsNetActive())
                //        {
                //            oriComp.OnNetDeActive();
                //        }

                //        oriComp.OnEndPlay(UnrealEngine.Engine.EEndPlayReason.Destroyed);
                //        InfinityHuluMod.SelfPoleDrinkComp = __instance.ActorCompContainerCS.RegisterUnitComp<TestPoleDrinkComp>(-2013265920, (EActorCompAlterFlag)0L, (EActorCompRejectFlag)0L, int.MaxValue, 0);
                //        //Utils.Log($"Replace PoleDrinkComp Comp Successfully! New Comp TypeIndex: {TypeManager.GetTypeIndex<TestPoleDrinkComp>()}");
                #endregion
            }
            else
                Utils.Log("Replace PoleDrinkComp Comp Failed");
        }
    }

}
