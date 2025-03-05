using b1;
using BtlB1;
using BtlShare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Engine;

namespace InfinityHuluMod
{
    public class BUICastItemSkill : BUInputActionTemplate
    {
        // Token: 0x06011465 RID: 70757 RVA: 0x000AAF74 File Offset: 0x000A9174
        public BUICastItemSkill()
        {
            this.InputActionType = EInputActionType.CastItemSkill;
        }

        // Token: 0x06011466 RID: 70758 RVA: 0x005038D4 File Offset: 0x00501AD4
        protected override bool OnTriggerInputAction(int InputActionID, UnrealEngine.Plugins.EnhancedInput.ETriggerEvent TriggerEvent, ref FInputActionValue Value, b1.Prediction.GSPredictionKey PredictionKey)
        {
            Utils.Log($"BUICastItemSkill OnTriggerInputAction: InputActionID: {InputActionID}, TriggerEvent: {TriggerEvent}, InputActionValue: {Value}, PredictionKey: {PredictionKey}");

            AActor owner = base.GetOwner();
            if (owner == null)
            {
                return false;
            }
            //if (!BGUFuncLibInput.BGUIsCanReceiveBattleInput(owner) || !BGUFuncLibInput.BGUIsCanReceiveBattleInputByActionType(owner, this.InputActionType))
            //{
            //    return false;
            //}
            IBUC_UnitStateData readOnlyData = InfinityHuluMod.SelfPoleDrinkComp?.UnitStateData;
            if (readOnlyData == null || readOnlyData.HasState(EBGUUnitState.JumpMoving))
            {
                return false;
            }

            FUStEnhancedInputActionDesc enhancedInputActionDesc = BGW_GameDB.GetEnhancedInputActionDesc(InputActionID);
            if (enhancedInputActionDesc == null)
            {
                return false;
            }
            if (enhancedInputActionDesc.InputActionParamsInt.Count == 0)
            {
                return false;
            }

            if (InfinityHuluMod.SelfPoleDrinkComp?.SkillInstsData == null)
            {
                return false;
            }
            if (InfinityHuluMod.SelfPoleDrinkComp?.SkillInputAssistData == null)
            {
                return false;
            }

            //BUC_PoleDrinkData readOnlyData2 = InfinityHuluMod.PoleDrinkData;
            //Utils.Log($"PoleDrinkData is Valid: {readOnlyData2 != null}, bPoleDrinkFlag: {readOnlyData2?.bPoleDrinkFlag}");
            //if (readOnlyData2 != null && readOnlyData2.bPoleDrinkFlag)
            //{
            //    //BUS_GSEventCollection bus_GSEventCollection = BUS_EventCollectionCS.Get(owner);
            //    //if (bus_GSEventCollection != null)
            //    //{
            //    //    bus_GSEventCollection.Evt_DoPoleDrink.Invoke(EPoleDrinkType.DrinkHPBottom, 0);
            //    //}
            //    return true;
            //}

            Utils.Log("After All Data Check!!!");

            int skillID = enhancedInputActionDesc.InputActionParamsInt[0];
            BGUCharacterCS bgucharacterCS = owner as BGUCharacterCS;
            if (bgucharacterCS != null)
            {
                IBPC_PlayerTagData readOnlyData3 = InfinityHuluMod.SelfPoleDrinkComp.PlayerTagData;
                if (readOnlyData3 != null && readOnlyData3.HasTag(EBGPPlayerTag.Transforming))
                {
                    FUStPlayerTransUnitConfDesc fustPlayerTransUnitConfDesc = BGW_GameDB.GetFUStPlayerTransUnitConfDesc(bgucharacterCS.GetResID(), 0);
                    if (fustPlayerTransUnitConfDesc == null)
                    {
                        return false;
                    }
                    Utils.Log($"DrinkSkillId: {fustPlayerTransUnitConfDesc.DrinkSkillId} ");
                    if (fustPlayerTransUnitConfDesc.DrinkSkillId <= 0)
                    {
                        return false;
                    }
                    skillID = fustPlayerTransUnitConfDesc.DrinkSkillId;
                }
            }
            BUS_GSEventCollection bus_GSEventCollection2 = BUS_EventCollectionCS.Get(owner);
            if (bus_GSEventCollection2 != null)
            {
                Utils.Log($"Before Evt_InputCastSkill, SkillID: {skillID} ");
                //bus_GSEventCollection2.Evt_InputCastSkill.Invoke(EInputActionType.CastItemSkill, false, skillID, -1);
            }
            return true;
        }
    }
}
