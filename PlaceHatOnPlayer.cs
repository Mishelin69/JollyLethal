using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlaceHatOnPlayerPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void GiveHatToPlayer(PlayerControllerB __instance)
    {
        JollyLethal.PluginLogInfoWithPrefix("Placing santa hat on player");

        var (bonePath, posOffset, rotOffset, scale) = PlaceHatsOnEnemiesPatch.GetEnemyHatConfig("Masked");
        if (bonePath is null)
        {
            return;
        }
        Transform targetBone = PlaceHatsOnEnemiesPatch.GetHatPlaceTransform(__instance.transform, bonePath);
        var hat = PlaceHatsOnEnemiesPatch.SpawnSantaHatOnEnemy(targetBone, posOffset, rotOffset, scale); 
        int losLayer = LayerMask.NameToLayer("LineOfSight");
        SetLayerRecursively(hat.gameObject, losLayer);
    }

    private static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
