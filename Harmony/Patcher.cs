using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace BetterWarehouseNames
{
    public static class Patcher {
        public const string HarmonyId = "Sleepy.BetterWarehouseNames";

        private static bool s_patched = false;
        private static int s_iHarmonyPatches = 0;

        public static void PatchAll() 
        {
            if (!s_patched)
            {
#if DEBUG
                Debug.Log("Patching...");
#endif
                s_patched = true;
                var harmony = new Harmony(HarmonyId);

                List<Type> patchList = new List<Type>();

                // Warehouse patch
                //patchList.Add(typeof(WarehouseWorldInoPanelPatch)); 

                // General patches
                patchList.Add(typeof(WarehouseWorldInoPanelPatch));

                // Perform the patching
                PatchAll(patchList);
            }
        }

        public static void PatchAll(List<Type> patchList)
        {
            Debug.Log($"Patching:{patchList.Count} functions");
            s_iHarmonyPatches = patchList.Count;
            var harmony = new Harmony(HarmonyId);

            foreach (var patchType in patchList)
            {
                Patch(harmony, patchType);
            }
        }

        public static void UnpatchAll() {
            if (s_patched)
            {
                var harmony = new Harmony(HarmonyId);
                harmony.UnpatchAll(HarmonyId);
                s_patched = false;
#if DEBUG
                Debug.Log("Unpatching...");
#endif
            }
        }

        public static int GetRequestedPatchCount()
        {
            return s_iHarmonyPatches;
        }

        public static int GetPatchCount()
        {
            var harmony = new Harmony(HarmonyId);
            var methods = harmony.GetPatchedMethods();
            int i = 0;
            foreach (var method in methods)
            {
                var info = Harmony.GetPatchInfo(method);
                if (info.Owners?.Contains(harmony.Id) == true)
                {
#if DEBUG
                    Debug.Log($"Harmony patch method = {method.FullDescription()}");
                    if (info.Prefixes.Count != 0)
                    {
                        Debug.Log("Harmony patch method has PreFix");
                    }
                    if (info.Postfixes.Count != 0)
                    {
                        Debug.Log("Harmony patch method has PostFix");
                    }
#endif
                    i++;
                }
            }

            return i;
        }

        public static void Patch(Harmony harmony, Type patchType)
        {
#if DEBUG
            Debug.Log($"Patch:{patchType}");
#endif
            PatchClassProcessor processor = harmony.CreateClassProcessor(patchType);
            processor.Patch();
        }

        public static void Unpatch(Harmony harmony, Type patchType, string sMethod)
        {
#if DEBUG
            Debug.Log($"Patch:{patchType} Method:{sMethod}");
#endif
            MethodInfo info = AccessTools.Method(patchType, sMethod);
            harmony.Unpatch(info, HarmonyPatchType.All, HarmonyId);
        }
    }
}
