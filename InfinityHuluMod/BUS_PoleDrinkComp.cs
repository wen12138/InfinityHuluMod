using b1;
using BtlShare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Engine;
using UnrealEngine.Runtime;

namespace InfinityHuluMod
{
    public class BUS_PoleDrinkComp : b1.UActorCompBaseCS
    {
        private bool ticked = false;

        public override void OnAttach()
        {
            //var del = new b1.EventDelDefine.Del_DoPoleDrink(SelfDoPoleDrink);
            base.BUSEventCollection.Evt_PoleDrinkStateBegin += SelfPoleDrinkStateBegin;
            base.BUSEventCollection.Evt_PoleDrinkStateEnd += SelfPoleDrinkStateEnd;
            base.BUSEventCollection.Evt_DoPoleDrink += SelfDoPoleDrink;
            base.BUSEventCollection.Evt_InputCastSkill += OnInputCastSkill;
            base.BUSEventCollection.Evt_TriggerInputActionImpl += OnTriggerInputActionImpl;
            Utils.Log($"Self BUS_PoleDrinkComp OnAttach! IsNetActive: {IsNetActive()}");
        }

        private void OnInputCastSkill(EInputActionType InputActionType, bool IsRelease, int SkillID, int DescID)
        {
            Utils.Log($"Self OnInputCastSkill! PoleDrinkData: InputActionType:{InputActionType}, IsRelease: {IsRelease}, SkillID: {SkillID}, DescID:{DescID}");
        }

        public void SelfPoleDrinkStateBegin(UAnimMontage DrinkHPBottomSuccessAM, UAnimMontage DrinkHPBottomFailedAM, TMapReadWrite<int, UAnimMontage> UseItemAMMapping)
        {
            
        }

        public void SelfPoleDrinkStateEnd()
        {
            
        }

        private void OnTriggerInputActionImpl(string ActionName, UnrealEngine.Plugins.EnhancedInput.ETriggerEvent TriggerEvent, FInputActionValue Value)
        {
            FUStEnhancedInputActionDesc descByInputActionNameAndTriggerEvent = BGW_GameDB.GetDescByInputActionNameAndTriggerEvent(base.GetActorResID(), ActionName, TriggerEvent);
            if (descByInputActionNameAndTriggerEvent == null)
            {
                return;
            }
            
            Utils.Log($"OnTriggerInputActionImpl ActionName:{ActionName}, InputActionType: {descByInputActionNameAndTriggerEvent.InputActionType}, TriggerEvent: {TriggerEvent}, InputActionValue: {Value}");
        }

        public override void OnTickWithGroup(float DeltaTime, int TickGroup)
        {
            base.OnTickWithGroup(DeltaTime, TickGroup);

            if (!ticked)
            {
                Utils.Log("Self DoPoleDrink Ticked!");
                ticked = true;
            }
        }

        public void SelfDoPoleDrink(EPoleDrinkType Type, int SkillID)
        {
            Utils.Log("Self DoPoleDrink Invoke!");

            //if (UGSE_AnimFuncLib.IsSlotPlayingMontage(Utils.GetBGUPlayerCharacterCS().Mesh.GetAnimInstance(), B1GlobalFNames.AMMatryoshka))
            //{
            //    return;
            //}

            //UAnimMontage uAnimMontage = null;
            //switch (Type)
            //{
            //    case EPoleDrinkType.UseItem:
            //        {
            //            if (InfinityHuluMod.PoleDrinkData.UseItemAMMapping.TryGetValue(SkillID, out var value))
            //            {
            //                uAnimMontage = value;
            //            }

            //            break;
            //        }
            //    case EPoleDrinkType.DrinkHPBottom:
            //        uAnimMontage = InfinityHuluMod.PoleDrinkData.DrinkHPBottomFailedAM;
            //        if (InfinityHuluMod.AttrContainer != null && ((int)InfinityHuluMod.AttrContainer.GetFloatValue(EBGUAttrFloat.BloodBottomNum) > 0 || InfinityHuluMod.IsInfinityHulu()))
            //        {
            //            uAnimMontage = InfinityHuluMod.PoleDrinkData.DrinkHPBottomSuccessAM;
            //        }

            //        break;
            //}

            //if (!(uAnimMontage == null))
            //{
            //    float num = BGUFuncLibAnim.BGUActorTryPlayMontage(Utils.GetBGUPlayerCharacterCS(), uAnimMontage, FName.None);
            //    if (num > 0f)
            //    {
            //        InfinityHuluMod.PoleDrinkData.CurPlayAM = uAnimMontage;
            //        InfinityHuluMod.PoleDrinkData.CurMontageLength = num;
            //        InfinityHuluMod.PoleDrinkData.CurMontageRemainTime = InfinityHuluMod.PoleDrinkData.CurMontageLength;
            //    }
            //}
        }
    }
}
