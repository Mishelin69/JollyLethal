using HarmonyLib;
using UnityEngine;
using System.Linq;

namespace JollyLethal;

[HarmonyPatch(typeof(GrabbableObject))]
internal class GetItemName
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void PrintItemName(GrabbableObject __instance)
    {
        JollyLethal.PluginLogInfoWithPrefix($"Item name {__instance.itemProperties.itemName}");
    }
}
