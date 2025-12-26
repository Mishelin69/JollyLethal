using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

[HarmonyPatch(typeof(GrabbableObject))]
internal class PlaceHatsOnItemsPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void GiveEnemySantaHat(GrabbableObject __instance)
    {
        if (JollyLethal.myCustomAssets is null) 
        {
            JollyLethal.PluginLogErrorWithPrefix("AssetBundle not loaded!!!");
            return;
        }
    }
}
