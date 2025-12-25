using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

[HarmonyPatch(typeof(EnemyAI))]
internal class FixHatPositionOnEnemyStateChange
{
    [HarmonyPatch("SwitchToBehaviourStateOnLocalClient")]
    [HarmonyPostfix]
    private static void FixEnemyHatPosition(EnemyAI __instance, int stateIndex)
    {
        Transform? hatTransform = RecursiveFindChild(__instance.transform, JollyLethal.JollyHatSpawnedObjName);
        if (hatTransform is null)
        {
            return;
        }

        //Not working
        switch (__instance.enemyType.enemyName)
        {
            case "Jester":
                HandleJesterStateChange(hatTransform, stateIndex);
                break;
            case "Maneater":
                HandleManeaterStateChange(__instance.transform, hatTransform, stateIndex);
                break;
        }
    }

    private static void HandleJesterStateChange(Transform hat, int state)
    {
        JollyLethal.PluginLogInfoWithPrefix("Handling Jesters change of state");
        switch (state)
        {
            case 0:
            case 1: // Boxed / Cranking State
                var (_, posOffset, rotOffset, scale) = EnemyHatConfigs.GetJesterSantaHatConfig();
                PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffset, rotOffset, scale);
                break;

            case 2: // Popped / Chasing State
                var (_, posOffsetPopped, rotOffsetPopped, scalePopped) = EnemyHatConfigs.GetJesterPoppedSantaHatConfig();
                PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffsetPopped, rotOffsetPopped, scalePopped);
                break;
        }
    }

    private static void HandleManeaterStateChange(Transform enemy, Transform hat, int state)
    {
        JollyLethal.PluginLogInfoWithPrefix("Handling Maneaters change of state");
        switch (state)
        {
            case 0: // Baby (Calm/Wandering)
            case 1: // Baby (Crying)
                var (bonePath, posOffset, rotOffset, scale) = EnemyHatConfigs.GetManeaterSantaHatConfig();
                Transform newParent = enemy.Find(bonePath);

                hat.SetParent(newParent, false);
                PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffset, rotOffset, scale);
                break;
            case 2: // ADULT (Monster)
                var (bonePathBig, posOffsetBig, rotOffsetBig, scaleBig) = EnemyHatConfigs.GetManeaterSantaHatConfig();
                Transform newParentBig = enemy.Find(bonePathBig);

                hat.SetParent(newParentBig, false);
                PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffsetBig, rotOffsetBig, scaleBig);
                break;        
        }
    }

    internal static Transform? RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }

            Transform? found = RecursiveFindChild(child, childName);
            if (found is not null)
            {
                return found;
            }
        }
        return null;
    }
}
