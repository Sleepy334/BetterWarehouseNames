using ColossalFramework.UI;
using HarmonyLib;
using System;

namespace BetterWarehouseNames
{
    [HarmonyPatch]
    public class WarehouseWorldInoPanelPatch
    {
        [HarmonyPatch(typeof(WarehouseWorldInfoPanel), "RefreshDropdownLists")]
        [HarmonyPostfix]
        public static void RefreshDropdownLists(ref UIDropDown ___m_dropdownResource)
        {
            // We add them from the end so Fish mod works
            int iCount = ___m_dropdownResource.items.Length;
            if (iCount > 0)
            {
                ___m_dropdownResource.items[iCount - 5] = "Forest Zones - Lumber";
                ___m_dropdownResource.items[iCount - 4] = "Farm Zones - Food";
                ___m_dropdownResource.items[iCount - 3] = "Ore Zones - Coal";
                ___m_dropdownResource.items[iCount - 2] = "Oil Zones - Petrol";
            }
        }
    }
}
