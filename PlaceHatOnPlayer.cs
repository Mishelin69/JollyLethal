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
        //TODO: fix this code
        return;
        if (Time.frameCount % 60 != 0 || !ShouldPlaceHatOnPlayersHat(__instance))
        {
            return;
        }

        JollyLethal.PluginLogInfoWithPrefix($"Placing santa hat on remote player: {__instance.playerUsername}");

        var (bonePath, posOffset, rotOffset, scale) = PlaceHatsOnEnemiesPatch.GetEnemyHatConfig("Masked");
        if (bonePath is null)
        {
            return;
        }
        Transform targetBone = PlaceHatsOnEnemiesPatch.GetHatPlaceTransform(__instance.transform, bonePath);
        var hat = PlaceHatsOnEnemiesPatch.SpawnSantaHatOnEnemy(targetBone, posOffset, rotOffset, scale);
        int defaultLayer = 0; 
        SetLayerRecursively(hat.gameObject, defaultLayer);   
    }

    private static bool ShouldPlaceHatOnPlayersHat(PlayerControllerB __instance)
    {
        var (bonePath, _, _, _) = PlaceHatsOnEnemiesPatch.GetEnemyHatConfig("Masked");
        return !__instance.IsOwner && !__instance.isPlayerDead &&
                __instance.isPlayerControlled && 
                __instance.transform.Find($"{bonePath}/{JollyLethal.JollyHatSpawnedObjName}") is null;
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
