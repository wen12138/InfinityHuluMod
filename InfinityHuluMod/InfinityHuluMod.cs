using b1;
using BtlShare;
using CommB1;
using CSharpModBase;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnrealEngine.Engine;
using UnrealEngine.Runtime;

namespace InfinityHuluMod
{
    public class InfinityHuluMod : ICSharpMod
    {
        public string Name => "InfinityHuluMod";

        public string Version => "0.0.1";

        private Harmony m_Harmony;

        [Serializable]
        public struct HuluIDList
        {
            public int[] Hulus;
        }

        private const int MIN_HULU_ID = 18001;
        private const int MAX_HULU_ID = 18017;

        public static DSRoleData RoleData;
        public static HuluIDList HuluList;

        public void Init()
        {
            Utils.Log("Init Infinity Hulu Mod!!!");
            m_Harmony = new Harmony("InfinityHuluMod.Patch");
            m_Harmony.PatchAll(Assembly.GetExecutingAssembly());

            InitConfig();
        }

        public void DeInit()
        {
            m_Harmony?.UnpatchAll();
            Utils.Log("Uninit Infinity Hulu Mod!!!");
        }

        private void InitConfig()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string configPath = Path.Combine(baseDirectory, "CSharpLoader\\Mods\\InfinityHuluMod\\InfinityHuluConfig.json");
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                HuluList = json.FromJson<HuluIDList>();
                if (HuluList.Hulus != null && HuluList.Hulus.Length > 0)
                {
                    Utils.Log("Init Hulu Config Successfully!");
                    foreach (var hulu in HuluList.Hulus)
                    {
                        Utils.Log($"Infinity Hulu: {hulu}!");
                    }

                }
            }
            else
            {
                Utils.Log("InfinityHuluConfig.json Not Exist! Init Config Failed!");
            }
        }

        private static void TryGetRoleData()
        {
            var gameplayer = GSGBtl.GetLocalPlayerContainer().GamePlayer;
            if (gameplayer == null)
            {
                Utils.Log("New BUACBloodBottleNumCost Get Gameplayer Failed!");
                return;
            }

            Utils.Log("New BUACBloodBottleNumCost Get Gameplayer Successfully!!");
            var type = gameplayer.GetType();
            var field = type.GetField("RootData", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (field != null)
            {
                RoleData = field.GetValue(gameplayer) as DSRoleData;
                if (RoleData != null)
                {
                    Utils.Log("New BUACBloodBottleNumCost Get Roleplayer Data Successfully!!!");
                }
            }
        }

        public static bool IsInfinityHulu()
        {
            if (RoleData == null)
                TryGetRoleData();

            if (RoleData != null && HuluList.Hulus != null && HuluList.Hulus.Length > 0)
            {
                var equipList = RoleData.RoleCs.Actor.Wear.EquipList;
                foreach (var item in equipList.ValueList)
                {
                    if (item.Id >= MIN_HULU_ID && item.Id <= MAX_HULU_ID)
                    {
                        for (int i = 0; i < HuluList.Hulus.Length; i++)
                        {
                            if (HuluList.Hulus[i] == item.Id)
                            {
                                Utils.Log("This is Infinity Hulu!");
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsFastDrinkHulu()
        {
            if (RoleData == null)
                TryGetRoleData();

            if (RoleData != null && HuluList.Hulus != null && HuluList.Hulus.Length > 0)
            {
                var equipList = RoleData.RoleCs.Actor.Wear.EquipList;
                foreach (var item in equipList.ValueList)
                {
                    if (item.Id >= MIN_HULU_ID && item.Id <= MAX_HULU_ID)
                    {
                        if (item.Id == 18013)
                        {
                            Utils.Log("This is Fast Drink Hulu!");
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
