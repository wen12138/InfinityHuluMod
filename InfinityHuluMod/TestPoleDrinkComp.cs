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
    public class TestPoleDrinkComp : UActorCompBaseCS
    {
        public override void OnAttach()
        {
            PoleDrinkData = RequireWritableData<BUC_PoleDrinkData>();
            AttrContainer = RequireReadOnlyData<IBUC_AttrContainer, BUC_AttrContainer>();
            SkillInstsData = RequireReadOnlyData<IBUC_SkillInstsData, BUC_SkillInstsData>();
            SkillInputAssistData = RequireReadOnlyData<IBUC_SkillInputAssistData, BUC_SkillInputAssistData>();
            UnitStateData = RequireReadOnlyData<IBUC_UnitStateData, BUC_UnitStateData>();
            PlayerTagData = RequireReadOnlyData<IBPC_PlayerTagData, BPC_PlayerTagData>();
            //base.BUSEventCollection.Evt_PoleDrinkStateBegin += this.PoleDrinkStateBegin;
            //base.BUSEventCollection.Evt_PoleDrinkStateEnd += this.PoleDrinkStateEnd;
            base.BUSEventCollection.Evt_DoPoleDrink += DoPoleDrink;
            base.BUSEventCollection.Evt_InputCastSkill += OnInputCastSkill;
            Utils.Log($"Self TestPoleDrinkComp OnAttach!");
        }

        private void OnInputCastSkill(EInputActionType InputActionType, bool IsRelease, int SkillID, int DescID)
        {
            Utils.Log($"Self OnInputCastSkill! PoleDrinkData: InputActionType:{InputActionType}, IsRelease: {IsRelease}, SkillID: {SkillID}, DescID:{DescID}");
        }

        // Token: 0x06005296 RID: 21142
        public void PoleDrinkStateBegin(UAnimMontage DrinkHPBottomSuccessAM, UAnimMontage DrinkHPBottomFailedAM, TMapReadWrite<int, UAnimMontage> UseItemAMMapping)
        {
            Utils.Log($"Self TestPoleDrinkComp PoleDrinkStateBegin!");

            this.PoleDrinkData.bPoleDrinkFlag = true;
            this.PoleDrinkData.DrinkHPBottomSuccessAM = DrinkHPBottomSuccessAM;
            this.PoleDrinkData.DrinkHPBottomFailedAM = DrinkHPBottomFailedAM;
            this.PoleDrinkData.UseItemAMMapping.Clear();
            foreach (KeyValuePair<int, UAnimMontage> keyValuePair in UseItemAMMapping)
            {
                this.PoleDrinkData.UseItemAMMapping.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        // Token: 0x06005297 RID: 21143
        public void PoleDrinkStateEnd()
        {
            Utils.Log($"Self TestPoleDrinkComp PoleDrinkStateEnd!");

            if (!this.PoleDrinkData.bPoleDrinkFlag)
            {
                return;
            }
            this.PoleDrinkData.bPoleDrinkFlag = false;
            if (this.PoleDrinkData.CurPlayAM != null)
            {
                this.OwnerAsCharacterCS.StopAnimMontage(this.PoleDrinkData.CurPlayAM);
            }
            base.BUSEventCollection.Evt_SetAnimHumanoidAMMatryoshka.Invoke(0f);
            this.PoleDrinkData.CurMontageLength = 0f;
            this.PoleDrinkData.CurMontageRemainTime = 0f;
        }

        // Token: 0x06005298 RID: 21144
        public void DoPoleDrink(EPoleDrinkType Type, int SkillID)
        {
            Utils.Log($"Self TestPoleDrinkComp DoPoleDrink!");

            if (UGSE_AnimFuncLib.IsSlotPlayingMontage(this.OwnerAsCharacterCS.Mesh.GetAnimInstance(), B1GlobalFNames.AMMatryoshka))
            {
                return;
            }
            UAnimMontage uanimMontage = null;
            if (Type != EPoleDrinkType.DrinkHPBottom)
            {
                UAnimMontage uanimMontage2;
                if (Type == EPoleDrinkType.UseItem && this.PoleDrinkData.UseItemAMMapping.TryGetValue(SkillID, out uanimMontage2))
                {
                    uanimMontage = uanimMontage2;
                }
            }
            else
            {
                uanimMontage = this.PoleDrinkData.DrinkHPBottomFailedAM;
                if (this.AttrContainer != null && (int)this.AttrContainer.GetFloatValue(EBGUAttrFloat.BloodBottomNum) > 0)
                {
                    uanimMontage = this.PoleDrinkData.DrinkHPBottomSuccessAM;
                }
            }
            if (uanimMontage == null)
            {
                return;
            }
            float num = BGUFuncLibAnim.BGUActorTryPlayMontage(this.OwnerAsCharacterCS, uanimMontage, FName.None, EMontageBindReason.Default, 1f, 1f, 0f);
            if (num > 0f)
            {
                this.PoleDrinkData.CurPlayAM = uanimMontage;
                this.PoleDrinkData.CurMontageLength = num;
                this.PoleDrinkData.CurMontageRemainTime = this.PoleDrinkData.CurMontageLength;
            }
        }

        // Token: 0x06005299 RID: 21145
        public override int GetTickGroupMask()
        {
            return 1024;
        }

        // Token: 0x0600529A RID: 21146
        public override void OnTickWithGroup(float DeltaTime, int TickGroup)
        {
            if (InfinityHuluMod.PoleDrinkData.CurMontageRemainTime > 0f)
            {
                Utils.Log($"Ori PoleDrinkData.CurMontageRemainTime: {InfinityHuluMod.PoleDrinkData.CurMontageRemainTime}");
            }

            if (this.PoleDrinkData.CurMontageRemainTime > 0f)
            {
                Utils.Log($"PoleDrinkData.CurMontageRemainTime: {PoleDrinkData.CurMontageRemainTime}");
                this.PoleDrinkData.CurMontageRemainTime -= DeltaTime;
                float p;
                if (this.PoleDrinkData.CurMontageRemainTime <= 0f)
                {
                    p = 0f;
                }
                else if (this.PoleDrinkData.CurMontageRemainTime <= this.PoleDrinkData.BlendOutTime)
                {
                    if (this.PoleDrinkData.BlendOutTime > 0f)
                    {
                        p = this.PoleDrinkData.CurMontageRemainTime / this.PoleDrinkData.BlendOutTime;
                    }
                    else
                    {
                        p = 0f;
                    }
                }
                else if (this.PoleDrinkData.CurMontageLength - this.PoleDrinkData.CurMontageRemainTime <= this.PoleDrinkData.BlendInTime)
                {
                    if (this.PoleDrinkData.BlendInTime > 0f)
                    {
                        p = (this.PoleDrinkData.CurMontageLength - this.PoleDrinkData.CurMontageRemainTime) / this.PoleDrinkData.BlendInTime;
                    }
                    else
                    {
                        p = 1f;
                    }
                }
                else
                {
                    p = 1f;
                }
                base.BUSEventCollection.Evt_SetAnimHumanoidAMMatryoshka.Invoke(p);
            }
        }

        // Token: 0x040040A2 RID: 16546
        public BUC_PoleDrinkData PoleDrinkData;

        // Token: 0x040040A3 RID: 16547
        public IBUC_AttrContainer AttrContainer;
        public IBUC_SkillInstsData SkillInstsData;
        public IBUC_SkillInputAssistData SkillInputAssistData;
        public IBUC_UnitStateData UnitStateData;
        public IBPC_PlayerTagData PlayerTagData;
    }
}
