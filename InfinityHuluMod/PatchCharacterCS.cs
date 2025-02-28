using b1;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using b1.ECS;

namespace InfinityHuluMod
{
    [HarmonyPatch(typeof(BGUPlayerCharacterCS), "InitAllComp")]
    public class PatchCharacterCS
    {
        private static void Postfix(BGUPlayerCharacterCS __instance)
        {
            if (__instance != null)
            {
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
                if (oriComp != null)
                {
                    InfinityHuluMod.PoleDrinkData = (BUC_PoleDrinkData)oriComp.CachedOwnerECS.GetDataByChunk(TypeManager.GetTypeIndex<BUC_PoleDrinkData>());
                    InfinityHuluMod.AttrContainer = (BUC_AttrContainer)oriComp.CachedOwnerECS.GetDataByChunk(TypeManager.GetTypeIndex<BUC_AttrContainer>());

                    Utils.Log($"PoleDrinkData: {InfinityHuluMod.PoleDrinkData}");
                    Utils.Log($"AttrContainer: {InfinityHuluMod.AttrContainer}");


                    Type compContainerType = __instance.ActorCompContainerCS.GetType();
                    int removeCount = 0;

                    {
                        var compCSs = compContainerType.GetField("CompCSs", BindingFlags.Instance | BindingFlags.NonPublic);
                        if (compCSs != null)
                        {
                            var compList = compCSs.GetValue(__instance.ActorCompContainerCS) as List<UActorCompBaseCS>;
                            compList.Remove(oriComp);
                            removeCount++;
                            Utils.Log("Remove Origin PoleDrinkComp Form CompCSs");
                        }
                    }

                    {
                        var compCSs = compContainerType.GetField("CompCSsToBeginPlay", BindingFlags.Instance | BindingFlags.NonPublic);
                        if (compCSs != null)
                        {
                            var compList = compCSs.GetValue(__instance.ActorCompContainerCS) as List<UActorCompBaseCS>;
                            compList.Remove(oriComp);
                            removeCount++;
                            Utils.Log("Remove Origin PoleDrinkComp From CompCSsToBeginPlay");
                        }
                    }

                    if (removeCount == 2)
                    {
                        entMgr.RemoveObject(__instance.ECSEntity, oriComp);
                        Utils.Log("Remove Origin PoleDrinkComp From EntityManager");

                        if (oriComp.IsNetActive())
                        {
                            oriComp.OnNetDeActive();
                        }

                        oriComp.OnEndPlay(UnrealEngine.Engine.EEndPlayReason.Destroyed);
                        InfinityHuluMod.PoleDrinkComp = __instance.ActorCompContainerCS.RegisterUnitComp<b1.BUS_PoleDrinkComp>(-2013265920, (EActorCompAlterFlag)0L, (EActorCompRejectFlag)0L, int.MaxValue, 0);

                        //Utils.GetBUS_GSEventCollection().Evt_DoPoleDrink -= InfinityHuluMod.PoleDrinkComp.DoPoleDrink;
                        Utils.GetBUS_GSEventCollection().Evt_DoPoleDrink += InfinityHuluMod.DoPoleDrink;
                        //base.BUSEventCollection.Evt_DoPoleDrink += this.DoPoleDrink;
                        Utils.Log("Replace PoleDrinkComp Comp Successfully!");
                    }


                }
                else
                    Utils.Log("Replace PoleDrinkComp Comp Failed");
            }
        }

    }
}
