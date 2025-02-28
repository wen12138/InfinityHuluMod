using ArchiveB1;
using b1;
using BtlShare;
using CommB1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Engine;

namespace InfinityHuluMod
{
    public class BUACBloodBottleNumCost : b1.BUACBloodBottleNumCost
    {

        public override void DoCostAttrValue(AActor Owner, IBUC_AttrContainer AttrContainer, float AttrCostBase, float AttrCostRatio)
        {
            if (Owner == null || AttrContainer == null)
            {
                return;
            }

            int item = this.GetCostValue(AttrContainer, AttrCostBase, AttrCostRatio).Item1;

            if (!InfinityHuluMod.IsInfinityHulu())
            {
                if (AttrContainer.GetFloatValue(EBGUAttrFloat.BloodBottomNum) - (float)item < 0f)
                {
                    BUS_EventCollectionCS.Get(Owner).Evt_DrinkHpBottomFailed.Invoke();
                }
                else
                {
                    BUS_EventCollectionCS.Get(Owner).Evt_IncreaseAttrFloat.Invoke(EBGUAttrFloat.BloodBottomNum, (float)(-(float)item));
                }
            }

            
            if (item > 0)
            {
                BGUFunctionLibraryCS.TriggerGuideNodeFinishEvent(Owner, EGuideNodeFinishType.DrinkBloodBottle);
            }
        }

        public override bool IsAttrValueEnough(IBUC_AttrContainer AttrContainer, float AttrCostBase, float AttrCostRatio)
        {
            return InfinityHuluMod.IsInfinityHulu() || base.IsAttrValueEnough(AttrContainer, AttrCostBase, AttrCostRatio);
        }

    }
}
